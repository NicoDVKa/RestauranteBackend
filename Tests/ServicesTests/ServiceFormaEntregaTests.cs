

using Application.Interfaces;
using Application.UseCases;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Tests.ServicesTests
{
    public class ServiceFormaEntregaTests
    {
        [Fact]
        public async Task GetAllFormaEntrega_ShouldReturnFormaEntregaList()
        {
            // Arrange
            var mockCommand = new Mock<ICommandFormaEntrega>();
            var mockQuery = new Mock<IQueryFormaEntrega>();

            IList<FormaEntrega> formaEntregas = new List<FormaEntrega>()
            {
                new FormaEntrega
                {
                    FormaEntregaId = 1,
                    Descripcion = "Delivery"
                },
                new FormaEntrega
                {
                    FormaEntregaId = 2,
                    Descripcion = "En Restaurante"
                },
                new FormaEntrega
                {
                    FormaEntregaId = 3,
                    Descripcion = "Para llevar"
                }
            };

            IList<Application.Models.Response.FormaEntrega> expectedFormaEntregas = new List<Application.Models.Response.FormaEntrega>()
            {
                new Application.Models.Response.FormaEntrega
                {
                    Id = 1,
                    Descripcion = "Delivery"
                },
                new Application.Models.Response.FormaEntrega
                {
                    Id = 2,
                    Descripcion = "En Restaurante"
                },
                new Application.Models.Response.FormaEntrega
                {
                    Id = 3,
                    Descripcion = "Para llevar"
                }
            };

            mockQuery.Setup(q => q.GetAllFormaEntrega()).ReturnsAsync(formaEntregas);

            var service = new ServiceFormaEntrega(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.GetAllFormaEntrega();

            // Assert
            result.Should().BeEquivalentTo(expectedFormaEntregas);
        }

        [Fact]
        public async Task GetAllFormaEntrega_ShouldReturnEmptyList()
        {
            // Arrange
            var mockCommand = new Mock<ICommandFormaEntrega>();
            var mockQuery = new Mock<IQueryFormaEntrega>();

            IList<FormaEntrega> formaEntregas = new List<FormaEntrega>();

            mockQuery.Setup(q => q.GetAllFormaEntrega()).ReturnsAsync(formaEntregas);

            var service = new ServiceFormaEntrega(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.GetAllFormaEntrega();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetFormaEntregaById_ShouldReturnFormaEntrega()
        {
            // Arrange
            var mockCommand = new Mock<ICommandFormaEntrega>();
            var mockQuery = new Mock<IQueryFormaEntrega>();

            FormaEntrega formaEntrega = new FormaEntrega()
            {
                FormaEntregaId = 1,
                Descripcion = "Delivery"  
            };

            Application.Models.Response.FormaEntrega expectedFormaEntrega = new Application.Models.Response.FormaEntrega()
            {
                Id = 1,
                Descripcion = "Delivery"
            };

            mockQuery.Setup(q => q.GetFormaEntregaById(It.IsAny<int>())).ReturnsAsync(formaEntrega);

            var service = new ServiceFormaEntrega(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.GetFormaEntregaById(It.IsAny<int>());

            // Assert
            result.Should().BeEquivalentTo(expectedFormaEntrega);
        }

        [Fact]
        public async Task GetFormaEntregaById_ShouldReturnNull()
        {
            // Arrange
            var mockCommand = new Mock<ICommandFormaEntrega>();
            var mockQuery = new Mock<IQueryFormaEntrega>();

            var service = new ServiceFormaEntrega(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.GetFormaEntregaById(It.IsAny<int>());

            // Assert
            result.Should().BeNull();
        }
    }
}
