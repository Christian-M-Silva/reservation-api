using System.ComponentModel.DataAnnotations;

namespace ReservationApi.Models.Entities
{
    public class ReservationEntity:BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255, ErrorMessage = "Name must have a maximum of 255 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Check-in date is required")]
        public DateOnly CheckIn { get; set; }

        [Required(ErrorMessage = "Check-out date is required")]
        public DateOnly CheckOut { get; set; }

        [Required(ErrorMessage = "Room number is required")]
        public int RoomNumber { get; set; }

        [Required(ErrorMessage = "Client id is required")]
        public Guid ClientId { get; set; }
    }
}
