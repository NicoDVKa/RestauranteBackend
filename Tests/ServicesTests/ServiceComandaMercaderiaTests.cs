

using Application.Interfaces;
using Application.Models.Response;
using Application.UseCases;
using Domain.Entities;
using FluentAssertions;
using Infraestructure.Commands;
using Moq;
using System.ComponentModel.Design;

namespace Tests.ServicesTests
{
    public class ServiceComandaMercaderiaTests
    {
        [Fact]
        public async Task CreateComandaMercaderia_ShouldReturnInt()
        {
            // Arrange
            var mockCommand = new Mock<ICommandComandaMercaderia>();
            var mockQuery = new Mock<IQueryComandaMercaderia>();

            var expectedComandaMercaderiaId = 1;

            mockCommand.Setup(c => c.CreateComandaMercaderia(It.IsAny<ComandaMercaderia>())).ReturnsAsync(expectedComandaMercaderiaId);

            var service = new ServiceComandaMercaderia(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.CreateComandaMercaderia(It.IsAny<Guid>(), It.IsAny<int>());

            // Assert
            result.Should().Be(expectedComandaMercaderiaId);
        }

        [Fact]
        public async Task GetComandaMercaderiaByMercaderiaId_ShouldReturnEmptyList()
        {
            // Arrange
            var mockCommand = new Mock<ICommandComandaMercaderia>();
            var mockQuery = new Mock<IQueryComandaMercaderia>();

            IList<ComandaMercaderia> expectedComandaMercaderia = new List<ComandaMercaderia>();

            mockQuery.Setup(q => q.GetComandaMercaderiaByMercaderiaId(It.IsAny<int>())).ReturnsAsync(expectedComandaMercaderia);

            var service = new ServiceComandaMercaderia(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.GetComandaMercaderiaByMercaderiaId(It.IsAny<int>());

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetComandaMercaderiaByMercaderiaId_ShouldReturnListOfMercaderiaComandaResponse()
        {
            // Arrange
            var mockCommand = new Mock<ICommandComandaMercaderia>();
            var mockQuery = new Mock<IQueryComandaMercaderia>();

            IList<ComandaMercaderia> expectedComandaMercaderia = new List<ComandaMercaderia>()
            {
                new ComandaMercaderia
                {
                    ComandaMercaderiaId = 1,
                    MercaderiaId = 1,
                    ComandaId = Guid.NewGuid(),
                    Mercaderia = new Mercaderia
                    {
                        Precio = 10,
                        Nombre = "a"
                    }
                },
                new ComandaMercaderia
                {
                    ComandaMercaderiaId = 2,
                    MercaderiaId = 2,
                    ComandaId = Guid.NewGuid(),
                    Mercaderia = new Mercaderia
                    {
                        Precio = 100,
                        Nombre = "aa"
                    }
                },
                new ComandaMercaderia
                {
                    ComandaMercaderiaId = 3,
                    MercaderiaId = 3,
                    ComandaId = Guid.NewGuid(),
                    Mercaderia = new Mercaderia
                    {
                        Precio = 1000,
                        Nombre = "aaa"
                    }
                }
            };

            IList<MercaderiaComandaResponse> expectedComandaMercaderiaResponse = new List<MercaderiaComandaResponse>()
            {
                new MercaderiaComandaResponse
                {
                    Id = 1,
                    Nombre = "a",
                    Precio = 10
                },
                new MercaderiaComandaResponse
                {
                    Id = 2,
                    Nombre = "aa",
                    Precio = 100
                },
                new MercaderiaComandaResponse
                {
                    Id = 3,
                    Nombre = "aaa",
                    Precio = 1000
                }
            };

            mockQuery.Setup(q => q.GetComandaMercaderiaByMercaderiaId(It.IsAny<int>())).ReturnsAsync(expectedComandaMercaderia);

            var service = new ServiceComandaMercaderia(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.GetComandaMercaderiaByMercaderiaId(It.IsAny<int>());

            // Assert
            result.Should().BeEquivalentTo(expectedComandaMercaderiaResponse);
        }
    }
}
