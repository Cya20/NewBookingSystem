using BookingWeb.API.DAL.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

var client = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7016")
};

Console.WriteLine("Welcome! Please make your booking below");
Console.Write("");
Console.WriteLine("Enter your name: ");
var name = Console.ReadLine();

string[] itemTypes = Enum.GetNames(typeof(BookingItemType));
Console.WriteLine("Available Types: " + string.Join(", ", itemTypes));
Console.WriteLine(" ");
Console.WriteLine("Enter your Booking Item Type: ");
var itemType = Console.ReadLine();

Console.WriteLine("Enter your Booking Item Name: ");
var itemName = Console.ReadLine();

Console.WriteLine("Enter your Booking Date (yyyy-MM-dd): ");
var bookingDateInput = Console.ReadLine();

var newBooking = new Booking
{
    CustomerName = name,
    ItemType = Enum.TryParse<BookingItemType>(itemType, true, out var parsedItemType) ? parsedItemType : BookingItemType.Apartment, 
    ItemName = itemName,
    StartDate = DateTime.TryParse(bookingDateInput, out var parsedDate) ? parsedDate : DateTime.Now ,
    EndDate = DateTime.TryParse(bookingDateInput, out var parsedEndDate) ? parsedEndDate.AddDays(7) : DateTime.Now.AddDays(7)
};

await CreateBookingAsync(newBooking);

Console.WriteLine("Would you like to update your booking? (Y/N)");
var updateResponse = Console.ReadLine();

if(!string.IsNullOrEmpty(updateResponse) && string.Equals(updateResponse, "Y", StringComparison.OrdinalIgnoreCase))
{
    var bookings = await GetAllBookingsAsync();

    Console.WriteLine("Current Bookings: ");
    foreach (var booking in bookings)
    {
        Console.WriteLine($"ID: {booking.Id}, Customer: {booking.CustomerName}, Item: {booking.ItemName}, Type: {booking.ItemType}, Start: {booking.StartDate.ToShortDateString()}, End: {booking.EndDate.ToShortDateString()}");
    }
    Console.WriteLine("Enter ID of Booking to be updated: ");
    if(int.TryParse(Console.ReadLine(), out var bookingId))
    {
        var bookingToUpdate = await GetBookingByid(bookingId);
        if(bookingToUpdate == null)
        {
            Console.WriteLine("Booking not found. Please try again.");
            return;
        }
        Console.WriteLine("Enter your name: ");
        var updatedName = Console.ReadLine();

        string[] updateItemTypes = Enum.GetNames(typeof(BookingItemType));
        Console.WriteLine("Available Types: " + string.Join(", ", itemTypes));
        Console.WriteLine(" ");
        Console.WriteLine("Enter your Booking Item Type: ");
        var updatedItemType = Console.ReadLine();

        Console.WriteLine("Enter your Booking Item Name: ");
        var updatedItemName = Console.ReadLine();

        Console.WriteLine("Enter your Booking Date (yyyy-MM-dd): ");
        var updatedBookingDateInput = Console.ReadLine();
        var updatedBooking = new Booking
        {
            Id = bookingId,
            CustomerName = updatedName,
            ItemType = Enum.TryParse<BookingItemType>(updatedItemType, true, out var updatedParsedItemType) ? parsedItemType : BookingItemType.Apartment,
            ItemName = itemName,
            StartDate = DateTime.TryParse(updatedBookingDateInput, out var updatedParsedDate) ? updatedParsedDate : DateTime.Now,
            EndDate = DateTime.TryParse(updatedBookingDateInput, out var updatedParsedEndDate) ? updatedParsedEndDate.AddDays(7) : DateTime.Now.AddDays(7)
        };
        await UpdateBookingAsync(updatedBooking);
    }
    else
    {
        Console.WriteLine("Invalid ID. Please try again.");
        return;
    }

   
}


async Task<Booking> CreateBookingAsync(Booking booking)
 {
	try
	{
        var response = await client.PostAsJsonAsync("api/Booking/CreateBooking", booking);

        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var createdBooking = JsonConvert.DeserializeObject<Booking>(responseContent);
            Console.WriteLine($"Booking created successfully! Thank you very much {createdBooking.CustomerName} your booking ID is: {createdBooking.Id}");
            return createdBooking;
        }
        else
        {
            Console.WriteLine("Failed to create booking. Please try again.");
            var errorMessage = JsonConvert.DeserializeObject<Booking>(responseContent);
            return errorMessage;
        }
    }
	catch (Exception ex)
	{
		throw;
	}
 }

async Task<bool> UpdateBookingAsync(Booking booking)
{
    try
    {
        var response = await client.PutAsJsonAsync("api/Booking/UpdateBooking", booking);

        var responseContent = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"{booking.CustomerName} your booking has been successfully updated");
            return true;
        }
        else
        {
            Console.WriteLine("Failed to create booking. Please try again.");
            return false;
        }
    }
    catch (Exception ex)
    {
        throw;
    }
}

async Task<IEnumerable<Booking>> GetAllBookingsAsync()
{
    try
    {
        var response = await client.GetAsync("api/Booking/GetAllBookings");
        var responseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var bookings = JsonConvert.DeserializeObject<List<Booking>>(responseContent);
            Console.WriteLine("Current Bookings:");
            foreach (var booking in bookings)
            {
                Console.WriteLine($"ID: {booking.Id}, Customer: {booking.CustomerName}, Item: {booking.ItemName}, Type: {booking.ItemType}, Start: {booking.StartDate.ToShortDateString()}, End: {booking.EndDate.ToShortDateString()}");
            }
            return bookings;
           
        }
        else
        {
            Console.WriteLine("Failed to retrieve bookings. Please try again.");
            
            return null;
            
        }
    }
    catch (Exception ex)
    {
        throw;
    }
}

async Task<Booking> GetBookingByid(int id)
{
    try
    {
        var response = await client.GetAsync($"api/Booking/GetBookingById/{id}");
        var responseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var booking = JsonConvert.DeserializeObject<Booking>(responseContent);
            Console.WriteLine($"ID: {booking.Id}, Customer: {booking.CustomerName}, Item: {booking.ItemName}, Type: {booking.ItemType}, Start: {booking.StartDate.ToShortDateString()}, End: {booking.EndDate.ToShortDateString()}");
            return booking;
           
        }
        else
        {
            Console.WriteLine("Failed to retrieve booking. Please try again.");
            var errorMessage = JsonConvert.DeserializeObject<Booking>(responseContent);
            return errorMessage;
        }
    }
    catch (Exception ex)
    {
        throw;
    }
}






