using Xunit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using A1_COMP2139.Models;

namespace UnitTest
{
    public class FlightModelTest
    {
        [Fact]
        public void Airline_Required()
        {
            // Arrange
            var flight = new Flight();

            // Act
            var results = ValidateModel(flight);

            // Assert
            Assert.Contains(results, r => r.MemberNames.Contains("Airline"));
        }

        [Fact]
        public void Price_ShouldBeInRange()
        {
            // Arrange
            var property = typeof(Flight).GetProperty("Price");

            // Act
            var rangeAttribute = property.GetCustomAttribute(typeof(RangeAttribute)) as RangeAttribute;

            // Assert
            Assert.NotNull(rangeAttribute);  // Check if the RangeAttribute is applied
            Assert.Equal(0.01, rangeAttribute.Minimum); // Verify that the minimum value is correctly set to 0.01
            Assert.Equal(10000.00, rangeAttribute.Maximum); // Verify that the maximum value is correctly set to 10,000.00
        }


        [Fact]
        public void ImageFile_ShouldBeNotMapped()
        {
            // Arrange
            var property = typeof(Flight).GetProperty("ImageFile");

            // Act
            var notMappedAttribute = property.GetCustomAttribute(typeof(NotMappedAttribute)) as NotMappedAttribute;

            // Assert
            Assert.NotNull(notMappedAttribute);
        }

        private static System.Collections.Generic.List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
