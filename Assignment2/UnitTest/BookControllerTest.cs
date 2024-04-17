using Xunit;
using A1_COMP2139.Data;
using A1_COMP2139.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;

namespace UnitTest
{
    public class BookControllerTest
    {
        private Mock<UserManager<ApplicationUser>> MockUserManager()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);

            userManager.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            userManager.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            return userManager;
        }

        [Fact]
        public void BookFlight_DecrementsFlightAvailability()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var userManager = MockUserManager();
                var controller = new BookController(context, userManager.Object);

                var flight = new Flight
                {
                    FlightId = 1,
                    FlightNumber = "AC199",
                    Airline = "Test Airline",
                    Departure = "Test Departure",
                    Arrival = "Test Arrival",
                    ImageName = "test.jpg",
                    Availability = 5
                };
                context.Flights.Add(flight);
                context.SaveChanges();

                // Act
                var result = controller.BookFlight(flightId: 1, guestEmail: "test@test.com", guestNumber: "123456", receiptNumber: "123");

                // Assert
                var bookedFlight = context.Flights.FirstOrDefault(f => f.FlightId == 1);
                Assert.Equal(4, bookedFlight.Availability);
            }
        }

        [Fact]
        public void BookHotel_DecrementsHotelAvailability()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var userManager = MockUserManager();
                var controller = new BookController(context, userManager.Object);

                var hotel = new Hotel
                {
                    HotelId = 1,
                    HotelName = "Test Hotel",
                    Location = "Test Location",
                    ImageName = "test.jpg",
                    Availability = 10
                };
                context.Hotels.Add(hotel);
                context.SaveChanges();

                // Act
                var result = controller.BookHotel(hotelId: 1, guestEmail: "test@test.com", guestNumber: "123456", receiptNumber: "123");

                // Assert
                var bookedHotel = context.Hotels.FirstOrDefault(h => h.HotelId == 1);
                Assert.Equal(9, bookedHotel.Availability);
            }
        }

        [Fact]
        public void BookCar_DecrementsCarAvailability()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var userManager = MockUserManager();
                var controller = new BookController(context, userManager.Object);

                var car = new Car
                {
                    CarId = 1,
                    RentalCompany = "Test Company",
                    Manufacturer = "BMW",
                    Model = "Test Model",
                    ImageName = "test.jpg",
                    Availability = 20
                };
                context.Cars.Add(car);
                context.SaveChanges();

                // Act
                var result = controller.BookCar(carId: 1, guestEmail: "test@test.com", guestNumber: "123456", receiptNumber: "123");

                // Assert
                var bookedCar = context.Cars.FirstOrDefault(c => c.CarId == 1);
                Assert.Equal(19, bookedCar.Availability);
            }
        }
    }
}
