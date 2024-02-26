using A1_COMP2139.Data;
using A1_COMP2139.Migrations;
using A1_COMP2139.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using System.IO;



public class BookController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BookFlight(int flightId, string guestEmail, string guestNumber, string receiptNumber)
    {
        var flight = _context.Flights.Find(flightId);
        if (flight != null)
        {
            flight.Availability--;
            var booking = new Book
            {
                FlightId = flight.FlightId,
                GuestEmail = guestEmail,
                GuestNumber = guestNumber,
                ReceiptNumber = receiptNumber
                // Assigning the FlightId from the found flight
            };

            _context.Book.Add(booking);
            _context.SaveChanges();
            return RedirectToAction("ReceiptFlight", new { flightId = flightId, guestEmail = guestEmail, guestNumber = guestNumber, receiptNumber = booking.ReceiptNumber }); // Redirect to the home page or any desired page
        }
        return NotFound();
    }

    public IActionResult ReceiptFlight(int flightId, string guestEmail, string guestNumber, string receiptNumber)
    {
        var flight = _context.Flights.Find(flightId);
        if (flight != null)
        {
            var model = new Book
            {
                Flight = flight,
                GuestEmail = guestEmail,
                GuestNumber = guestNumber,
                ReceiptNumber = receiptNumber
            };
            return View(model); // Pass the Book model to the Receipt view
        }
        return NotFound();
    }




    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BookHotel(int hotelId, string guestEmail, string guestNumber, string receiptNumber)
    {
        var hotel = _context.Hotels.Find(hotelId);
        if (hotel != null)
        {
            hotel.Availability--;
            var booking = new Book
            {
                HotelId = hotel.HotelId,
                GuestEmail = guestEmail,
                GuestNumber = guestNumber,
                ReceiptNumber = receiptNumber// Assigning the FlightId from the found flight
            };

            _context.Book.Add(booking);
            _context.SaveChanges();
            return RedirectToAction("ReceiptHotel", new { hotelId = hotelId, guestEmail = guestEmail, guestNumber = guestNumber, receiptNumber = booking.ReceiptNumber }); // Redirect to the home page or any desired page
        }
        return NotFound();
    }

    public IActionResult ReceiptHotel(int hotelId, string guestEmail, string guestNumber, string receiptNumber)
    {
        var hotel = _context.Hotels.Find(hotelId);
        if (hotel != null)
        {
            var model = new Book
            {
                Hotel = hotel,
                GuestEmail = guestEmail,
                GuestNumber = guestNumber,
                ReceiptNumber = receiptNumber
            };
            return View(model); // Pass the Book model to the Receipt view
        }
        return NotFound();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult BookCar(int carId, string guestEmail, string guestNumber, string receiptNumber)
    {
        var car = _context.Cars.Find(carId);
        if (car != null)
        {
            car.Availability--;
            var booking = new Book
            {
                CarId = car.CarId,
                GuestEmail = guestEmail,
                GuestNumber = guestNumber,
                ReceiptNumber = receiptNumber// Assigning the FlightId from the found flight
            };

            _context.Book.Add(booking);
            _context.SaveChanges();
            return RedirectToAction("ReceiptCar", new { carId = carId, guestEmail = guestEmail, guestNumber = guestNumber, receiptNumber = booking.ReceiptNumber }); // Redirect to the home page or any desired page
        }
        return NotFound();
    }

    public IActionResult ReceiptCar(int carId, string guestEmail, string guestNumber, string receiptNumber)
    {
        var car = _context.Cars.Find(carId);
        if (car != null)
        {
            var model = new Book
            {
                Car = car,
                GuestEmail = guestEmail,
                GuestNumber = guestNumber,
                ReceiptNumber = receiptNumber
            };
            return View(model); // Pass the Book model to the Receipt view
        }
        return NotFound();
    }

}