namespace ReservationApi.Models.Entities
{
    public class ReservationEntity:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public int RoomNumber { get; set; }
        public Guid ClientId { get; set; }
    }
}
