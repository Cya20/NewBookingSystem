using BookingWeb.API.DAL.Data;
using BookingWeb.API.DAL.IRepository;
using BookingWeb.API.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookingWeb.API.DAL.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        private int _nextId = 1;
        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddBookingAsync(Booking booking)
        {
            try
            {
                booking.Id = _nextId++;
                var newProduct = await _context.Bookings.AddAsync(booking);
                _context.SaveChanges();

                return true;
            }
            catch (SqlException sqlex)
            {
                Log.Error($"SQL Exception occurred while adding a booking: {sqlex.Message}");
                //I would write the logs to Grafana, OpenTelemetry
                return false;
                // Log the exception (sqlex) here
            }
            catch (Exception ex)
            {
                Log.Error($"Exception occurred while adding a booking: {ex.Message}");
                //I would write the logs to Grafana, OpenTelemetry
                return false;
                //throw;
            }

        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            try
            {
                var entity = await _context.Bookings.FindAsync(id);

                if (entity != null)
                {
                    _context.Bookings.Remove(entity);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    throw new ArgumentException("Booking not found!");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Exception occurred while deleting booking: {ex.Message}");
                //I would write the logs to Grafana, OpenTelemetry
                return false;
            }
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            try
            {
                //I need to use use IQueryable for better performance and deferred execution
                var bookings = await _context.Bookings.ToListAsync();

                return bookings;
            }
            catch (Exception ex)
            {
                
                Log.Error($"Exception occurred while retrieving all a booking: {ex.Message}");
                //I would write the logs to Grafana, OpenTelemetry
                throw;
            }
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            try
            {
                var booking = await _context.Bookings.FindAsync(id);

                return booking;
            }
            catch (Exception ex)
            {
                Log.Error($"Exception occurred while retrieving a booking: {ex.Message}");
                //I would write the logs to Grafana, OpenTelemetry
                throw;
            }
        }

        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            try
            {
                var entity = await _context.Bookings.FindAsync(booking.Id);


                if (entity == null)
                    throw new ArgumentException("Booking not found");

                entity.CustomerName = booking.CustomerName;
                entity.ItemName = booking.ItemName;
                entity.ItemType = booking.ItemType;
                entity.StartDate = booking.StartDate;
                entity.EndDate = booking.EndDate;

                _context.Bookings.Update(entity);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Exception occurred while updating booking: {ex.Message}");
                //I would write the logs to Grafana, OpenTelemetry
                return false;
                //throw;
            }
        }
    }
}
