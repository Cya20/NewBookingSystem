using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookingWeb.API.DAL.Models
{
    public class Booking
    {
        public int Id { get; set; }

        
        public string CustomerName { get; set; }

        
        public BookingItemType ItemType { get; set; }

        
        public string ItemName { get; set; }

        
        public DateTime StartDate { get; set; }

        
        public DateTime EndDate { get; set; }
    }

    public enum BookingItemType
    {
        Apartment,
        Vehicle,
        Show,
        OutdoorActivity
    }
}
