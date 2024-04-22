using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.Domain.ModelEntities
{
    public class Flight
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Numer lotu jest wymagany")]
        public string FlightNumber { get; set; }

        [Required(ErrorMessage = "Data odlotu jest wymagana")]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "Miejsce odlotu jest wymagane")]
        public string DepartureLocation { get; set; }

        [Required(ErrorMessage = "Miejsce przylotu jest wymagane")]
        public string ArrivalLocation { get; set; }

        [Required(ErrorMessage = "Typ samolotu jest wymagany")]
        public string AircraftType { get; set; }
    }

}
