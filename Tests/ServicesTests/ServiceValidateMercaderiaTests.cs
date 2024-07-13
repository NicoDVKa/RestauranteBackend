

using Application.UseCases;
using FluentAssertions;

namespace Tests.ServicesTests
{
    public class ServiceValidateMercaderiaTests
    {
        [Theory]
        [InlineData("Nombre","Nicolas", 50, false)]
        [InlineData("Ingredientes", "aa", 2, false)]
        [InlineData("Preparacion", "aaa", 3, false)]
        [InlineData("Imagen", "http//....", 255, false)]
        [InlineData("Imagen", "http//....", 255, true)]
        [InlineData("Imagen", null, 255, true)]
        public void StringIsValid_ShouldReturnTrue(String tag, String? verify, int maxLength, Boolean allowNull)
        {
            // Arrange
            var service = new ServiceValidateMercaderia();

            // Act
            var result = service.StringIsValid(tag, verify, maxLength, allowNull);

            //Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("Nombre", null, 50, false)]
        [InlineData("Ingredientes", null, 2, false)]
        [InlineData("Preparacion", null, 3, false)]
        [InlineData("Imagen", null, 255, false)]
        [InlineData("123", null, 255, false)]
        public void StringIsValid_AllowNullIsFalseAndTagIsNull_ShouldReturnFalse(String tag, String? verify, int maxLength, Boolean allowNull)
        {
            // Arrange
            String expectedError = $"{tag}-Ingrese un valor";
            var service = new ServiceValidateMercaderia();
            
            // Act
            var result = service.StringIsValid(tag, verify, maxLength, allowNull);
            var error = service.GetError();

            //Assert
            result.Should().BeFalse();
            error.Should().BeEquivalentTo(expectedError);
        }

        [Theory]
        [InlineData("Nombre", "  ", 50, false)]
        [InlineData("Ingredientes", "  ", 2, false)]
        [InlineData("Preparacion", "", 3, false)]
        [InlineData("Imagen", "   ", 255, false)]
        public void StringIsValid_AllowNullIsFalseAndTagIsEmpty_ShouldReturnFalse(String tag, String? verify, int maxLength, Boolean allowNull)
        {
            // Arrange
            String expectedError = $"{tag}-Ingrese un valor";
            var service = new ServiceValidateMercaderia();

            // Act
            var result = service.StringIsValid(tag, verify, maxLength, allowNull);
            var error = service.GetError();

            //Assert
            result.Should().BeFalse();
            error.Should().BeEquivalentTo(expectedError);
        }

        [Theory]
        [InlineData("Nombre", "n", 0, false)]
        [InlineData("Ingredientes", "asdasdas", 2, false)]
        [InlineData("Preparacion", "asdsadasd", 3, false)]
        [InlineData("Imagen", "asdfghjklñpoi", 10, false)]
        public void StringIsValid_TagLengthIsGreaterThanMaxLength_ShouldReturnFalse(String tag, String? verify, int maxLength, Boolean allowNull)
        {
            // Arrange
            String expectedError = $"{tag}-El campo no cumple con el formato";
            var service = new ServiceValidateMercaderia();

            // Act
            var result = service.StringIsValid(tag, verify, maxLength, allowNull);
            var error = service.GetError();

            //Assert
            result.Should().BeFalse();
            error.Should().BeEquivalentTo(expectedError);
        }
    }
}
