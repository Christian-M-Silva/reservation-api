namespace ReservationApi.Models.Shared
{
    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; } = string.Empty;
        public DateOnly ExpirationDateRefreshToken { get; set; }
    }
}
