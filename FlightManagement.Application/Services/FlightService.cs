using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagement.Domain.ModelEntities;
using FlightManagement.Infrastructure.Repositories;

namespace FlightManagement.Application.Services
{
    public class FlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _flightRepository.GetAllAsync();
        }

        public async Task<Flight> GetFlightByIdAsync(int id)
        {
            return await _flightRepository.GetByIdAsync(id);
        }

        public async Task<Flight> CreateFlightAsync(Flight flight)
        {
            return await _flightRepository.AddAsync(flight);
        }

        public async Task<bool> UpdateFlightAsync(int id, Flight flight)
        {
            var existingFlight = await _flightRepository.GetByIdAsync(id);
            if (existingFlight == null)
                return false;

            existingFlight.FlightNumber = flight.FlightNumber;
            existingFlight.DepartureDate = flight.DepartureDate;
            existingFlight.DepartureLocation = flight.DepartureLocation;
            existingFlight.ArrivalLocation = flight.ArrivalLocation;
            existingFlight.AircraftType = flight.AircraftType;

            await _flightRepository.UpdateAsync(existingFlight);
            return true;
        }

        public async Task<bool> DeleteFlightAsync(int id)
        {
            var existingFlight = await _flightRepository.GetByIdAsync(id);
            if (existingFlight == null)
                return false;

            await _flightRepository.DeleteAsync(existingFlight);
            return true;
        }
    }
}
