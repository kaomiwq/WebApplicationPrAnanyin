using Microsoft.Identity.Client;
using WebApplicationPrAnanyin.ActionClass.HelperClass.DTO;
using WebApplicationPrAnanyin.ActionClass.Reservation;
using WebApplicationPrAnanyin.Models;

namespace WebApplicationPrAnanyin.Interface
{
    public interface IReservation
    {
        public List<ReservationDTO> GetReservations();
        public List<ReservationDTO> GetReservation(int UserId);
        public List<string> AddReservation(ReservationCreate reservation);
        public List<string> UpdateReservation(int UserId, ReservationUpdateDTO reservation);
        public List<string> DeleteReservation(int Id);
    }
}
