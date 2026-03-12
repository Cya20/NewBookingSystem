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
            Assert.NotEqual(0, result.Result.Id);
            Assert.Equal(booking.ItemType, result.Result.ItemType);
        }

        [Fact]
        public void GetAll_Should_Return_All_Bookings()
        {
            //Arrange
            var repo = GetDbContext();
            var service = new BookingBusinessLogicService(repo);
            var booking1 = new BookingWeb.API.DAL.Models.Booking
            {
                CustomerName = "Siyanda Lerumo",
                ItemType = BookingItemType.Vehicle,
                ItemName = "Audi RS5",
                StartDate = DateTime.Now.AddDays(5),
                EndDate = DateTime.Now.AddDays(14)
            };
            var booking2 = new BookingWeb.API.DAL.Models.Booking
            {
                CustomerName = "Wandile Lerumo",
                ItemType = BookingItemType.Apartment,
                ItemName = "Beachfront Apartment",
                StartDate = DateTime.Now.AddDays(10),
                EndDate = DateTime.Now.AddDays(20)
            };
            service.CreateBookingAsync(booking1);
            service.CreateBookingAsync(booking2);
            // Act
            var result = service.GetAllBookingsAsync();
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetBookingById_Should_Return_Correct_Booking()
        {
            //Arrange
            var repo = GetDbContext();
            var service = new BookingBusinessLogicService(repo);
            var booking1 = new BookingWeb.API.DAL.Models.Booking
            {
                CustomerName = "Siyanda Lerumo",
                ItemType = BookingItemType.Vehicle,
                ItemName = "Audi RS5",
                StartDate = DateTime.Now.AddDays(5),
                EndDate = DateTime.Now.AddDays(14)
            };
            var booking2 = new BookingWeb.API.DAL.Models.Booking
            {
                CustomerName = "Wandile Lerumo",
                ItemType = BookingItemType.Apartment,
                ItemName = "Beachfront Apartment",
                StartDate = DateTime.Now.AddDays(10),
                EndDate = DateTime.Now.AddDays(20)
            };
            var booking3 = new BookingWeb.API.DAL.Models.Booking
            {
                CustomerName = "Madoda Lerumo",
                ItemType = BookingItemType.OutdoorActivity,
                ItemName = "SkyDiving",
                StartDate = DateTime.Now.AddDays(7),
                EndDate = DateTime.Now.AddDays(18)
            };
            var createdBooking = service.CreateBookingAsync(booking1).Result;
            var createdBooking2 = service.CreateBookingAsync(booking2).Result;
            var createdBooking3 = service.CreateBookingAsync(booking3).Result;
            // Act
            var result = service.GetBookingByIdAsync(createdBooking.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdBooking.Id, result.Result.Id);
        }

        [Fact]
        public void DeleteBooking_Should_Return_True()
        {
            //Arrange
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
            var createdBooking = service.CreateBookingAsync(booking).Result;
            // Act
            var result = service.DeleteBookingAsync(createdBooking.Id);
            // Assert
            Assert.True(result.Result);
        }

        [Fact]
        public void DeleteBooking_Should_Return_False_For_Invalid_Id()
        {
            //Arrange
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
            var createdBooking = service.CreateBookingAsync(booking);
            // Act
            var result = service.DeleteBookingAsync(2);
            // Assert
            Assert.False(result.Result);
        }

        [Fact]
        public void UpdateBooking_Should_Return_True()
        {
            //Arrange
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
            var createdBooking = service.CreateBookingAsync(booking).Result;
            createdBooking.ItemName = "BMW M3";
            createdBooking.StartDate = DateTime.Now.AddDays(6);
            // Act
            var result = service.UpdateBookingAsync(createdBooking);
            // Assert
            Assert.True(result.Result);
        }
    }
}