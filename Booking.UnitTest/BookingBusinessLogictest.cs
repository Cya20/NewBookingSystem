using BookingWeb.API.BLL.Services;
using BookingWeb.API.DAL.Data;
using BookingWeb.API.DAL.Models;
using BookingWeb.API.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.UnitTest
{
    public class BookingBusinessLogictest
    {

        private BookingRepository GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BookingTestDb")
                .Options;
            var context = new ApplicationDbContext(options);
            return new BookingRepository(context);
        }


        [Fact]
        public void Create_Booking_Should_Return_Booking()
        {
             // Arrange
            var repo = GetDbContext();
            var service = new BookingBusinessLogicService(repo);
            var booking = new BookingWeb.API.DAL.Models.Booking
            {
                CustomerName = "Siyanda Lerumo",
                ItemType = BookingItemType.Vehicle,
                ItemName = "Audi RS5",
                StartDate = DateTime.Now.AddDays(5),
                EndDate = DateTime.Now.AddDays(14)
            };
            // Act
            var result = service.CreateBookingAsync(booking);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(booking.CustomerName, result.Result.CustomerName);
            Assert.NotEqual(0 , result.Result.Id);
            Assert.Equal(booking.ItemType, result.Result.ItemType);
        }
    }
}
