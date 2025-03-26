namespace ReservationApi.Models.Request
{
    public class FilterRequest
    {
        public DateOnly? CheckIn { get; set; }
        public int? RoomNumber { get; set; }
    }
}
