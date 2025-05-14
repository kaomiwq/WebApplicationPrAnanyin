namespace WebApplicationPrAnanyin.ActionClass.HelperClass.DTO
{
    public class ReservationUpdateDTO
    {
        public int TableId { get; set; }

        public int UserId { get; set; }

        public string Status { get; set; } = null!;
    }
}
