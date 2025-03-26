using System.ComponentModel.DataAnnotations;

namespace ReservationApi.Models.Entities
{
    public class LogEntity
    {
        [Required(ErrorMessage = "Request type is required")]
        public string RequestType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Route is required")]
        public string Route { get; set; } = string.Empty;

        [Required(ErrorMessage = "Log time is required")]
        public DateTime LogTime { get; set; }
    }
}
