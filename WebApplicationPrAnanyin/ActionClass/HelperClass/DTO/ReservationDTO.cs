using WebApplicationPrAnanyin.Models;

namespace WebApplicationPrAnanyin.ActionClass.HelperClass.DTO
{
    public class ReservationDTO
    {
        public int Id { get; set; }

        public int TableId { get; set; }

        public int UserId { get; set; }

        public DateTime DateTime { get; set; }

        public string Status { get; set; } = null!;

    }
}
