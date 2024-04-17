using Xunit;
using A1_COMP2139.Data;
using A1_COMP2139.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using A1_COMP2139.Controllers;

namespace UnitTest
{
    public class FlightControllerTest
    {
        private FlightController _controller;
        private ApplicationDbContext _context;

        public FlightControllerTest()
        {
            // Mock ApplicationDbContext
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "FlightManagementDb")
                .Options;
            _context = new ApplicationDbContext(dbContextOptions);

            // Seed in-memory database with test data
            var testData = new List<Flight>
    {
        new Flight
        {
            FlightId = 1, FlightNumber = "AC777", Airline = "Test Airline 1",
            Departure = "Test Departure 1", Arrival = "Test Arrival 1",
            DepartureDate = DateTime.Now.AddDays(1), ArrivalDate = DateTime.Now.AddDays(2),
            Price = 500.00, Availability = 1, ImageName= "Test Image 1"
        },
        new Flight
        {
            FlightId = 2, FlightNumber = "LD777", Airline = "Test Airline 2",
            Departure = "Test Departure 2", Arrival = "Test Arrival 2",
            DepartureDate = DateTime.Now.AddDays(3), ArrivalDate = DateTime.Now.AddDays(4),
            Price = 750.00, Availability = 0, ImageName= "Test Image 2"
        },
    };
            _context.Flights.AddRange(testData);
            _context.SaveChanges();

            // Create controller instance with mocked context
            _controller = new FlightController(_context, null);
        }

        [Fact]
        public void Index_ReturnsViewWithFlights()
        {
            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var flights = Assert.IsAssignableFrom<IEnumerable<Flight>>(result.Model);
            Assert.Single(flights); // Expecting 1 flight with availability > 0

            var flight = flights.First();
            Assert.True(flight.Availability > 0);
            Assert.True(flight.Price >= 0.01 && flight.Price <= 10000.00); // Price must be within the valid range
            Assert.True(flight.ArrivalDate > flight.DepartureDate); // Arrival date must be after the departure date
        }


        [Fact]
        public void Details_ReturnsViewWithFlight()
        {
            // Arrange
            int flightId = 1;

            // Act
            var result = _controller.Details(flightId) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var flight = Assert.IsAssignableFrom<Flight>(result.Model);
            Assert.Equal(flightId, flight.FlightId); // Expecting flight with specified flightId
            Assert.True(flight.ArrivalDate > flight.DepartureDate); // Check the date order
            Assert.True(flight.Price >= 0.01 && flight.Price <= 10000.00); // Check the price range
        }

        [Fact]
        public void Details_ReturnsNotFoundForNonexistentFlight()
        {
            // Arrange
            int nonExistentFlightId = 100;

            // Act
            var result = _controller.Details(nonExistentFlightId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

    }
}