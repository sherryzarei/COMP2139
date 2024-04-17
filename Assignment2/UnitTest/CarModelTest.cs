using Xunit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using A1_COMP2139.Models;

namespace UnitTest
{
    public class CarModelTest
    {
        [Fact] //passed
        public void Company_Required()
        {
            // Arrange
            var car = new Car();

            // Act
            var results = ValidateModel(car);

            // Assert
            Assert.Contains(results, r => r.MemberNames.Contains("RentalCompany"));
        }

        [Fact]
        public void Price_ShouldBeInRange()
        {
            // Arrange
            var property = typeof(Car).GetProperty("Price");

            // Act
            var rangeAttribute = property.GetCustomAttribute(typeof(RangeAttribute)) as RangeAttribute;

            // Assert
            Assert.NotNull(rangeAttribute);
            Assert.Equal(0.01, rangeAttribute.Minimum);  // Corrected minimum value
            Assert.Equal(double.MaxValue, rangeAttribute.Maximum);
        }


        [Fact]  //passed
        public void ImageFile_ShouldBeNotMapped()
        {
            // Arrange
            var property = typeof(Car).GetProperty("ImageFile");

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
