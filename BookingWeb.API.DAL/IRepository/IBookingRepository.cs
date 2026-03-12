using BookingWeb.API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingWeb.API.DAL.IRepository
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int id);
        Task<bool> AddBookingAsync(Booking booking);
        Task<bool> UpdateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int id);
    }
}
