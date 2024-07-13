

using Application.Interfaces;
using Application.Models.Response;
using Application.UseCases;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Tests.ServicesTests
{
    public class ServiceTipoMercaderiaTests
    {
        [Fact]
        public async Task GetTipoMercaderiaById_ShouldReturnTipoMercaderia()
        {
            // Arrange
            var mockCommand = new Mock<ICommandTipoMercaderia>();
            var mockQuery = new Mock<IQueryTipoMercaderia>();

            TipoMercaderia tipoMercaderia = new TipoMercaderia()
            {
                TipoMercaderiaId = 1,
                Descripcion = "Entrada"
            };

            TipoMercaderiaResponse expectedTipoMercaderia = new TipoMercaderiaResponse()
            {
                Id = 1,
                Descripcion = "Entrada"
            };

            mockQuery.Setup(q => q.GetTipoMercaderiaById(It.IsAny<int>())).ReturnsAsync(tipoMercaderia);

            var service = new ServiceTipoMercaderia(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.GetTipoMercaderiaById(It.IsAny<int>());

            // Assert
            result.Should().BeEquivalentTo(expectedTipoMercaderia);
        }

        [Fact]
        public async Task GetTipoMercaderiaById_ShouldReturnNull()
        {
            // Arrange
            var mockCommand = new Mock<ICommandTipoMercaderia>();
            var mockQuery = new Mock<IQueryTipoMercaderia>();

            var service = new ServiceTipoMercaderia(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.GetTipoMercaderiaById(It.IsAny<int>());

            // Assert
            result.Should().BeNull();
        }
    }
}
