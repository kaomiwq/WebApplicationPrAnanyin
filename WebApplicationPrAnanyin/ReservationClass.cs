using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplicationPrAnanyin.ActionClass.HelperClass.DTO;
using WebApplicationPrAnanyin.ActionClass.Reservation;
using WebApplicationPrAnanyin.Interface;
using WebApplicationPrAnanyin.Models;

namespace WebApplicationPrAnanyin
{
    public class ReservationClass : IReservation
    {
        private readonly AnanyinReservationContext _context;
        public ReservationClass(AnanyinReservationContext context) 
        {
            _context = context;
        }
        public List<ReservationDTO> GetReservations() 
        {
            try
            {
                var reservationsData = _context.Reservations.Select(x => new ReservationDTO()
                {
                    Id = x.Id,
                    TableId = x.TableId,
                    UserId = x.UserId,
                    DateTime = x.DateTime,
                    Status = x.Status
                }
                ).ToList();
                return (List<ReservationDTO>)reservationsData;
            }
            catch 
            {
                Results.BadRequest();
                throw;
            }


        }
        public List<ReservationDTO> GetReservation(int UserId)
        {
            try
            {
                var reservation = _context.Reservations.Find(UserId);

                if (reservation == null)
                {
                    Results.NotFound(new List<string> { "У этого пользователя нет броней" });
                }
                var activeReservationData = _context.Reservations.Where(r => r.UserId == UserId && r.Status == "Активна").Select(x => new ReservationDTO()
                {
                    Id = x.Id,
                    TableId = x.TableId,
                    UserId = x.UserId,
                    DateTime = x.DateTime,
                    Status = x.Status
                }
                ).ToList();
                return (List<ReservationDTO>)activeReservationData;
            }
            catch 
            {
                Results.BadRequest();
                throw;
            }
        }
        public List<string> AddReservation(ReservationCreate reservation)
        {
            try
            {
                var tableData = _context.Tables.FirstOrDefault(x => x.Id == reservation.TableId);
                var user = _context.Users.Find(reservation.UserId);
                var isTableClosed = _context.Tables.Any(t => t.Id == reservation.TableId && t.Status == "close");
                if (isTableClosed) 
                {
                    return new List<string> { "Этот столик закрыт для бронирования" };
                }
                if (user == null) 
                {
                    return new List<string> { "Пользователь не найден" };
                }
                tableData.Status = "close";
                
                
                Reservation createdReservation = new Reservation()
                {
                    TableId = reservation.TableId,
                    UserId = reservation.UserId,
                    DateTime = DateTime.Now,
                    Status = "Активна"
                };

                _context.Tables.Update(tableData);
                _context.Reservations.Add(createdReservation);
                _context.SaveChanges();

                int reservationId = createdReservation.Id;

                Results.Created();
                return [$"Бронь успешно создана id - {reservationId}"];
}
            catch (Exception)
            {
                Results.BadRequest(new List<string> { "Ошибка в выполнении запроса" });
                throw;
            }
        }
        public List<string> UpdateReservation(int Id, ReservationUpdateDTO reservationUpdate)
        {
            try
            {
                var reservationData = _context.Reservations.FirstOrDefault(x => x.Id == Id);
                var currentTableData = _context.Tables.FirstOrDefault(x => x.Id == reservationData.TableId);
                currentTableData.Status = "open";
                var tableData = _context.Tables.FirstOrDefault(x => x.Id == reservationUpdate.TableId);
                var user = _context.Users.Find(reservationUpdate.UserId);
                var isTableClosed = _context.Tables.Any(t => t.Id == reservationUpdate.TableId && t.Status == "close");
                if (isTableClosed)
                {
                    return new List<string> { "Этот столик закрыт для бронирования" };
                }
                if (user == null)
                {
                    return new List<string> { "Пользователь не найден" };
                }
                tableData.Status = "close";

                if (reservationData == null) 
                {
                    Results.NoContent();
                    return [];
                }
                
                reservationData.TableId = reservationUpdate.TableId;
                reservationData.UserId = reservationData.UserId;
                reservationData.DateTime = DateTime.Now;
                reservationData.Status = reservationUpdate.Status;

                _context.Reservations.Update(reservationData);
                _context.SaveChanges();

                Results.Ok();
                return ["Данные брони успешно обновлены"];
            }
            catch 
            {
                Results.BadRequest();
                throw;
            }
        }
        public List<string> DeleteReservation(int Id)
        {
            try
            {
                var reservation = _context.Reservations.Find(Id);

                if (reservation == null) 
                {
                    return new List<string> { "Бронь не найдена" };
                }

                _context.Reservations.Remove(reservation);
                _context.SaveChanges();

                Results.NoContent();
                return ["Бронь успешно удалена"];
            }
            catch
            {
                Results.BadRequest(new List<string> { "Ошибка в выполнении запроса" });
                throw;
            }
        }
    }
}
