using FlightManagement.Domain.ModelEntities;
using FlightManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.Application.Services
{
    public class SeedService
    {
        private readonly IFlightRepository _flightRepository;

        public SeedService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task SeedDatabase()
        {
            var flights = await _flightRepository.GetAllAsync();
            if (!flights.Any())
            {
                var flightsToAdd = new List<Flight>
                {
                    new Flight { FlightNumber = "ABC123", DepartureDate = DateTime.Now, DepartureLocation = "New York", ArrivalLocation = "Los Angeles", AircraftType = "Boeing 747" },
                    new Flight { FlightNumber = "DEF456", DepartureDate = DateTime.Now.AddDays(1), DepartureLocation = "London", ArrivalLocation = "Paris", AircraftType = "Airbus A320" },
                    new Flight { FlightNumber = "GHI789", DepartureDate = DateTime.Now.AddDays(2), DepartureLocation = "Tokyo", ArrivalLocation = "Sydney", AircraftType = "Boeing 787" },
                    new Flight { FlightNumber = "JKL012", DepartureDate = DateTime.Now.AddDays(3), DepartureLocation = "Dubai", ArrivalLocation = "New York", AircraftType = "Airbus A380" },
                    new Flight { FlightNumber = "MNO345", DepartureDate = DateTime.Now.AddDays(4), DepartureLocation = "Los Angeles", ArrivalLocation = "London", AircraftType = "Boeing 777" },
                    new Flight { FlightNumber = "PQR678", DepartureDate = DateTime.Now.AddDays(5), DepartureLocation = "Paris", ArrivalLocation = "Frankfurt", AircraftType = "Airbus A330" },
                    new Flight { FlightNumber = "STU901", DepartureDate = DateTime.Now.AddDays(6), DepartureLocation = "Sydney", ArrivalLocation = "Tokyo", AircraftType = "Boeing 737" },
                    new Flight { FlightNumber = "VWX234", DepartureDate = DateTime.Now.AddDays(7), DepartureLocation = "New York", ArrivalLocation = "Dubai", AircraftType = "Airbus A350" },
                    new Flight { FlightNumber = "YZA567", DepartureDate = DateTime.Now.AddDays(8), DepartureLocation = "London", ArrivalLocation = "Los Angeles", AircraftType = "Boeing 787" },
                    new Flight { FlightNumber = "BCD890", DepartureDate = DateTime.Now.AddDays(9), DepartureLocation = "Frankfurt", ArrivalLocation = "Paris", AircraftType = "Airbus A320" }
                };

                foreach (var flight in flightsToAdd)
                {
                    await _flightRepository.AddAsync(flight);
                }
            }
        }
    }
}
