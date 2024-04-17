namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestFlightController()
        {
            // Arrange
            var flightControllerTests = new FlightControllerTest();

            // Act & Assert
            flightControllerTests.Index_ReturnsViewWithFlights();
            flightControllerTests.Details_ReturnsViewWithFlight();
            flightControllerTests.Details_ReturnsNotFoundForNonexistentFlight();
        }

        [Fact]
        public void TestHotelController()
        {
            // Arrange
            var hotelControllerTests = new HotelControllerTest();

            // Act & Assert
            hotelControllerTests.Index_ReturnsViewWithHotels();
            hotelControllerTests.Details_ReturnsViewWithHotel();
            hotelControllerTests.Details_ReturnsNotFoundForNonexistentHotel();
        }

        [Fact]
        public void TestCarController()
        {
            // Arrange
            var carControllerTests = new CarControllerTest();

            // Act & Assert
            carControllerTests.Index_ReturnsViewWithCars();
            carControllerTests.Details_ReturnsViewWithCar();
            carControllerTests.Details_ReturnsNotFoundForNonexistentCar();
        }

        [Fact]
        public void TestBookController()
        {
            // Arrange
            var bookControllerTests = new BookControllerTest();

            // Act & Assert
            bookControllerTests.BookFlight_DecrementsFlightAvailability();
            bookControllerTests.BookHotel_DecrementsHotelAvailability();
            bookControllerTests.BookCar_DecrementsCarAvailability();
        }
    }
}
