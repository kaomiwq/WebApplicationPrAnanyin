using System.ComponentModel.DataAnnotations;

namespace WebApplicationPrAnanyin.ActionClass.Reservation
{
    public class ReservationCreate
    {
        [Required]
        public int TableId { get; set; }
        [Required]
        public int UserId { get; set; }
       
       
    }
}
