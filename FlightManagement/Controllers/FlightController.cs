using FlightManagement.Application.Services;
using FlightManagement.Domain.ModelEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightManagement.Controllers
{
    public class FlightController : Controller
    {
        private readonly FlightService _flightService;

        public FlightController(FlightService flightService)
        {
            _flightService = flightService;
        }

        // GET: Flight
        public async Task<IActionResult> Index()
        {
            IEnumerable<Flight> flights = await _flightService.GetAllFlightsAsync();
            return View(flights);
        }

        // GET: Flight/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Flight flight = await _flightService.GetFlightByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flight/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flight/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightNumber, DepartureDate, DepartureLocation, ArrivalLocation, AircraftType")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                await _flightService.CreateFlightAsync(flight);
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // GET: Flight/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Flight flight = await _flightService.GetFlightByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        // POST: Flight/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FlightNumber, DepartureDate, DepartureLocation, ArrivalLocation, AircraftType")] Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _flightService.UpdateFlightAsync(id, flight);
                }
                catch (Exception)
                {
                    if (!FlightExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        // GET: Flight/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Flight flight = await _flightService.GetFlightByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flight/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _flightService.DeleteFlightAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
            // You can implement this method based on your requirements
            // For simplicity, I'm assuming that if Flight with the provided id exists, return true
            return true;
        }
    }
}
