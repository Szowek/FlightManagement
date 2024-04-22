using Microsoft.AspNetCore.Mvc;
using FlightManagement.Application.Services;
using FlightManagement.Domain.ModelEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightManagement.Main.Controllers
{
    public class FlightController : Controller
    {
        private readonly FlightService _flightService;

        public FlightController(FlightService flightService)
        {
            _flightService = flightService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Flight> flights = await _flightService.GetAllFlightsAsync();
            return View(flights);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _flightService.DeleteFlightAsync(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Flight flight)
        {
            if (ModelState.IsValid)
            {
                await _flightService.CreateFlightAsync(flight);
                return RedirectToAction(nameof(Index));
            }
            return View(flight);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Flight flight)
        {
            if (ModelState.IsValid)
            {
                var result = await _flightService.UpdateFlightAsync(flight.Id, flight);
                if (result)
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "Nie można zaktualizować lotu." });
            }
            return View(flight);
        }
    }
}
