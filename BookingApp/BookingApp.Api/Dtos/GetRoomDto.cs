namespace BookingApp.Api.Dtos
{
    public class GetRoomDto
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }

        public double Surface { get; set; }

        public bool NeedsRepair { get; set; }

    }
}
