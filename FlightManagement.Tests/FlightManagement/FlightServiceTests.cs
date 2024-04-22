using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlightManagement.Application.Services;
using FlightManagement.Domain.ModelEntities;
using FlightManagement.Infrastructure.Repositories;
using Moq;
using Xunit;

namespace FlightManagement.Tests
{
    public class FlightServiceTests
    {
        [Fact]
        public async Task GetAllFlightsAsync_ReturnsAllFlights()
        {
            var mockRepository = new Mock<IFlightRepository>();
            var testFlights = GetTestFlights();
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(testFlights);

            var flightService = new FlightService(mockRepository.Object);

            var result = await flightService.GetAllFlightsAsync();

            Assert.NotNull(result);
            Assert.Equal(3, result.Count());;
        }

        [Fact]
        public async Task GetFlightByIdAsync_ReturnsFlightWithSpecifiedId()
        {
            int flightId = 1;
            var testFlight = new Flight { Id = flightId, FlightNumber = "ABC123" };
            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(flightId)).ReturnsAsync(testFlight);

            var flightService = new FlightService(mockRepository.Object);

            var result = await flightService.GetFlightByIdAsync(flightId);

            Assert.NotNull(result);
            Assert.Equal(flightId, result.Id);
        }

        private List<Flight> GetTestFlights()
        {
            return new List<Flight>
            {
                new Flight { Id = 1, FlightNumber = "ABC123" },
                new Flight { Id = 2, FlightNumber = "DEF456" },
                new Flight { Id = 3, FlightNumber = "GHI789" }
            };
        }

        [Fact]
        public async Task CreateFlightAsync_CreatesNewFlight()
        {
            var mockRepository = new Mock<IFlightRepository>();
            var newFlight = new Flight { FlightNumber = "XYZ789", DepartureDate = DateTime.Now, DepartureLocation = "Test Location", ArrivalLocation = "Test Destination", AircraftType = "Test Aircraft" };
            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Flight>())).ReturnsAsync(newFlight);

            var flightService = new FlightService(mockRepository.Object);

            var result = await flightService.CreateFlightAsync(newFlight);

            Assert.NotNull(result);
            Assert.Equal(newFlight.FlightNumber, result.FlightNumber);
        }

        [Fact]
        public async Task UpdateFlightAsync_UpdatesExistingFlight()
        {
            int flightId = 1;
            var existingFlight = new Flight { Id = flightId, FlightNumber = "ABC123", DepartureDate = DateTime.Now, DepartureLocation = "Existing Location", ArrivalLocation = "Existing Destination", AircraftType = "Existing Aircraft" };
            var updatedFlight = new Flight { Id = flightId, FlightNumber = "Updated123", DepartureDate = DateTime.Now.AddDays(1), DepartureLocation = "Updated Location", ArrivalLocation = "Updated Destination", AircraftType = "Updated Aircraft" };
            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(flightId)).ReturnsAsync(existingFlight);
            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Flight>())).Returns(Task.CompletedTask);

            var flightService = new FlightService(mockRepository.Object);

            var result = await flightService.UpdateFlightAsync(flightId, updatedFlight);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteFlightAsync_DeletesExistingFlight()
        {
            int flightId = 1;
            var existingFlight = new Flight { Id = flightId, FlightNumber = "ABC123" };
            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(flightId)).ReturnsAsync(existingFlight);
            mockRepository.Setup(repo => repo.DeleteAsync(existingFlight)).Returns(Task.CompletedTask);

            var flightService = new FlightService(mockRepository.Object);

            var result = await flightService.DeleteFlightAsync(flightId);

            Assert.True(result);
        }

        [Fact]
        public async Task GetFlightByIdAsync_ReturnsNullForNonExistentFlight()
        {
            int nonExistentFlightId = 999;
            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(nonExistentFlightId)).ReturnsAsync((Flight)null);

            var flightService = new FlightService(mockRepository.Object);

            var result = await flightService.GetFlightByIdAsync(nonExistentFlightId);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateFlightAsync_ReturnsFalseForNonExistentFlight()
        {
            int nonExistentFlightId = 999;
            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(nonExistentFlightId)).ReturnsAsync((Flight)null);

            var flightService = new FlightService(mockRepository.Object);

            var result = await flightService.UpdateFlightAsync(nonExistentFlightId, new Flight());

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteFlightAsync_ReturnsFalseForNonExistentFlight()
        {
            int nonExistentFlightId = 999;
            var mockRepository = new Mock<IFlightRepository>();
            mockRepository.Setup(repo => repo.GetByIdAsync(nonExistentFlightId)).ReturnsAsync((Flight)null);

            var flightService = new FlightService(mockRepository.Object);

            var result = await flightService.DeleteFlightAsync(nonExistentFlightId);

            Assert.False(result);
        }

    }
}
