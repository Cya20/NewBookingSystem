using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingWeb.API.DAL.Models
{
    public class Booking
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        
        public BookingItemType ItemType { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool ActiveBooking { get; set; }
    }

    public enum BookingItemType
    {
        Apartment,
        Vehicle,
        Show,
        OutdoorActivity
    }
}
