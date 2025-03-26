namespace ReservationApi.Models.Shared
{
    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpirationDateRefreshToken { get; set; }
    }
}
