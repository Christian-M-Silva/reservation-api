using System.ComponentModel.DataAnnotations;

namespace ReservationApi.Models.Entities
{
    public class LogEntity : BaseEntity
    {
        [Required(ErrorMessage = "Method is required")]
        public string Method { get; set; } = string.Empty;

        [Required(ErrorMessage = "Route is required")]
        public string Route { get; set; } = string.Empty;

        [Required(ErrorMessage = "Log time is required")]
        public DateTime LogTime { get; set; }
    }
}
