using System;
using A1_COMP2139.Controllers;
using A1_COMP2139.Data;
using A1_COMP2139.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace UnitTest
{
	public class HotelControllerTest
	{
        private HotelController _controller;
        private ApplicationDbContext _context;

        public HotelControllerTest()
        {
            // Arrange: Mock ApplicationDbContext with in-memory database
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ProjectManagementDb")
                .Options;
            _context = new ApplicationDbContext(dbContextOptions);

            // Seed in-memory database with test data
            var testData = new List<Hotel>
            {
                new Hotel { HotelId = 1, HotelName = "Test Hotel 1", Location = "Test Location 1", Availability = 1, ImageName = "Test Image 1", Price = 100 },
                new Hotel { HotelId = 2, HotelName = "Test Hotel 2", Location = "Test Location 2", Availability = 0, ImageName = "Test Image 2", Price = 120 },
            };
            _context.Hotels.AddRange(testData);
            _context.SaveChanges();

            // Create controller instance with mocked context
            _controller = new HotelController(_context, null);
        }

        [Fact]
        public void Index_ReturnsViewWithHotels()
        {
            // Act: Call the Index action method
            var result = _controller.Index() as ViewResult;

            // Assert: Verify that the action method returns a ViewResult with a non-null model
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Hotel>>(result.Model);
            Assert.Equal(1, ((IEnumerable<Hotel>)result.Model).Count()); // Expecting 1 hotel with availability > 0
        }

        [Fact]
        public void Details_ReturnsViewWithHotel()
        {
            // Arrange: Specify the hotelId for the Details action method
            int hotelId = 1;

            // Act: Call the Details action method with the specified hotelId
            var result = _controller.Details(hotelId) as ViewResult;

            // Assert: Verify that the action method returns a ViewResult with the correct model
            Assert.NotNull(result);
            Assert.IsAssignableFrom<Hotel>(result.Model);
            Assert.Equal(hotelId, ((Hotel)result.Model).HotelId); // Expecting hotel with specified hotelId
        }

        [Fact]
        public void Details_ReturnsNotFoundForNonexistentHotel()
        {
            // Arrange: Specify a non-existent hotelId
            int nonExistentHotelId = 100;

            // Act: Call the Details action method with the non-existent hotelId
            var result = _controller.Details(nonExistentHotelId) as NotFoundResult;

            // Assert: Verify that the action method returns a NotFoundResult
            Assert.NotNull(result);
        }

    }
}

