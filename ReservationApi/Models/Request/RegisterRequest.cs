using ReservationApi.Models.Enuns;

namespace ReservationApi.Models.Request
{
    public class RegisterRequest:LoginRequest
    {
        public string Name { get; set; } = string.Empty;
        public RoleEnum Role { get; set; }
    }
}
