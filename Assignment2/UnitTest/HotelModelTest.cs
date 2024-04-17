using Xunit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using A1_COMP2139.Models;

namespace UnitTest
{
    public class HotelModelTest
    {
        [Fact]
        public void HotelName_Required()
        {
            // Arrange
            var hotel = new Hotel();

            // Act
            var results = ValidateModel(hotel);

            // Assert
            Assert.Contains(results, r => r.MemberNames.Contains("HotelName"));
        }

        [Fact]
        public void Location_Required()
        {
            // Arrange
            var hotel = new Hotel();

            // Act
            var results = ValidateModel(hotel);

            // Assert
            Assert.Contains(results, r => r.MemberNames.Contains("Location"));
        }

        [Fact]
        public void Price_ShouldBeInRange()
        {
            // Arrange
            var property = typeof(Hotel).GetProperty("Price");

            // Act
            var rangeAttribute = property.GetCustomAttribute(typeof(RangeAttribute)) as RangeAttribute;

            // Assert
            Assert.NotNull(rangeAttribute);  // Check if the RangeAttribute is applied
            Assert.Equal(0.01, rangeAttribute.Minimum); // Verify that the minimum value is correctly set to 0.01
            Assert.Equal(10000.00, rangeAttribute.Maximum); // Verify that the maximum value is correctly set to 10,000.00
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
