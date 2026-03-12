using BookingWeb.API.BLL.Interfaces;
using BookingWeb.API.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingBusinessLogic _bookingBusinessLogic;

        public BookingController(IBookingBusinessLogic bookingBusinessLogic)
        {
            _bookingBusinessLogic = bookingBusinessLogic;
        }

        [HttpGet("GetAllBookings")]
        public async Task<IActionResult> GetAllBookingsAsync()
        {
            var bookings = await _bookingBusinessLogic.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("GetBookingById/{id}")]
        public async Task<IActionResult> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingBusinessLogic.GetBookingByIdAsync(id);
           
            return Ok(booking);
        }

        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBookingAsync(Booking booking)
        {
            var createdBooking = await _bookingBusinessLogic.CreateBookingAsync(booking);
            return Ok(createdBooking);
        }

        [HttpPut("UpdateBooking")]
        public async Task<IActionResult> UpdateBookingAsync([FromBody] Booking booking)
        {
            var result = await _bookingBusinessLogic.UpdateBookingAsync(booking);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update booking");
            }
            return Ok();
        }
    }
}
