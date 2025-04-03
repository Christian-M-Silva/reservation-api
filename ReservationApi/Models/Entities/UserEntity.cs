using ReservationApi.Models.Enuns;
using System.ComponentModel.DataAnnotations;

namespace ReservationApi.Models.Entities
{
    public class UserEntity:BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255, ErrorMessage = "Name must have a maximum of 255 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(255, ErrorMessage = "Password must have a maximum of 255 characters")]
        [MinLength(8, ErrorMessage = "Password must have a minimun of 8 characters")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-mail is required")]
        [EmailAddress(ErrorMessage ="E-mail format invalid")]
        [MaxLength(256, ErrorMessage = "E-mail must have a maximum of 256 characters")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required")]
        public RoleEnum Role { get; set; }

        public string? RefreshToken { get; set; }

        public DateOnly? ExpirationDateRefreshToken { get; set; }
    }
}
