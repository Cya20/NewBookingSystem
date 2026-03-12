using BookingWeb.API.BLL.Helpers;
using BookingWeb.API.BLL.Interfaces;
using BookingWeb.API.DAL.IRepository;
using BookingWeb.API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Serilog;

namespace BookingWeb.API.BLL.Services
{
    public class BookingBusinessLogicService : IBookingBusinessLogic
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingBusinessLogicService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            try
            {
                if (booking == null)
                    throw new CustomException(HttpStatusCode.BadRequest, "Booking cannot be null");

                if(booking.StartDate > booking.EndDate)
                    throw new CustomException(HttpStatusCode.BadRequest, "Start date cannot be after end date");

                var result = await _bookingRepository.AddBookingAsync(booking);

                if (!result)
                    throw new CustomException(HttpStatusCode.InternalServerError, "Failed to add booking");

                return booking;
            }
            catch (Exception ex)
            {
                Log.Error($"CreateBookingAsync|Exception| {ex.Message}");
                throw;
            }
        }


        public async Task<bool> DeleteBookingAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(HttpStatusCode.BadRequest, "Invalid product ID");

                var result = await _bookingRepository.DeleteBookingAsync(id);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error($"DeleteBookingAsync|Exception| {ex.Message}");
                throw;
            }
        }


        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            try
            {
                var  bookings = await _bookingRepository.GetAllBookingsAsync();

                if (bookings == null || !bookings.Any())
                    throw new CustomException(HttpStatusCode.NotFound, "No bookings found");

                return bookings;
            }
            catch (Exception ex)
            {
                Log.Error($"GetAllBookingsAsync|Exception| {ex.Message}");
                throw;
            }
        }


        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException(HttpStatusCode.BadRequest, "Invalid Booking ID");

                var booking = await _bookingRepository.GetBookingByIdAsync(id);

                if (booking == null)
                    throw new CustomException(HttpStatusCode.NotFound, "Booking not found");

                return booking;
            }
            catch (Exception ex)
            {
                Log.Error($"GetBookingByIdAsync|Exception| {ex.Message}");
                throw;
            }
        }


        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            try
            {
                if (booking == null)
                    throw new CustomException(HttpStatusCode.BadRequest, "Booking cannot be null");

                if (booking.StartDate > booking.EndDate)
                    throw new CustomException(HttpStatusCode.BadRequest, "Start date cannot be after end date");

                var result = await _bookingRepository.UpdateBookingAsync(booking);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error($"UpdateBookingAsync|Exception| {ex.Message}");
                throw;
            }
        }
    }
}
