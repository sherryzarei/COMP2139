using Xunit;
using A1_COMP2139.Data;
using A1_COMP2139.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using A1_COMP2139.Controllers;

namespace UnitTest
{
    public class CarControllerTest
    {
        private CarController _controller;
        private ApplicationDbContext _context;

        public CarControllerTest()
        {
            // Arrange: Mock ApplicationDbContext with in-memory database
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ProjectManagementDb")
                .Options;
            _context = new ApplicationDbContext(dbContextOptions);

            // Seed in-memory database with test data
            var testData = new List<Car>
            {
                new Car { CarId = 1, RentalCompany = "Test Company 1", Manufacturer ="BMW", Model = "Test Model 1", Availability = 1, ImageName = "test_image1.jpg" },
                new Car { CarId = 2, RentalCompany = "Test Company 2", Manufacturer = "Benz",  Model = "Test Model 2", Availability = 0, ImageName = "test_image2.jpg" },
            };
            _context.Cars.AddRange(testData);
            _context.SaveChanges();

            // Create controller instance with mocked context
            _controller = new CarController(_context, null);
        }

        [Fact]
        public void Index_ReturnsViewWithCars()
        {
            // Act: Call the Index action method
            var result = _controller.Index() as ViewResult;

            // Assert: Verify that the action method returns a ViewResult with a non-null model
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Car>>(result.Model);
            Assert.Equal(1, ((IEnumerable<Car>)result.Model).Count()); // Expecting 1 car with availability > 0
        }

        [Fact]
        public void Details_ReturnsViewWithCar()
        {
            // Arrange: Specify the carId for the Details action method
            int carId = 1;

            // Act: Call the Details action method with the specified carId
            var result = _controller.Details(carId) as ViewResult;

            // Assert: Verify that the action method returns a ViewResult with the correct model
            Assert.NotNull(result);
            Assert.IsAssignableFrom<Car>(result.Model);
            Assert.Equal(carId, ((Car)result.Model).CarId); // Expecting car with specified carId
        }

        [Fact]
        public void Details_ReturnsNotFoundForNonexistentCar()
        {
            // Arrange: Specify a non-existent carId
            int nonExistentCarId = 100;

            // Act: Call the Details action method with the non-existent carId
            var result = _controller.Details(nonExistentCarId) as NotFoundResult;

            // Assert: Verify that the action method returns a NotFoundResult
            Assert.NotNull(result);
        }
    }
}
