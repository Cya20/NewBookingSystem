using BookingWeb.API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingWeb.API.BLL.Interfaces
{
    public interface IBookingBusinessLogic
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int id);
        Task<bool> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int id);
        Task<Booking> CreateBookingAsync(Booking booking);
    }
}
