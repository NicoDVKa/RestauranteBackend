
using Application.Interfaces;
using Application.Models.Response;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestauranteWebApi.Controllers;

namespace Tests.ControllersTests
{
    public class ComandaControllerTests
    {
        [Theory]
        [InlineData("a")]
        [InlineData("aa")]
        [InlineData("aaa")]
        [InlineData("aasd")]

        [InlineData("07/15/2024")] // Formato MM/dd/yyyy
        public async Task GetAll_FechaIsInvalid_ShouldReturnBadRequest400(string? fecha)
        {
            // Arrange
            var mockServiceComanda =  new Mock<IServiceComanda>();
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceFormaEntrega =  new Mock<IServiceFormaEntrega>();

            var expectedResponse = new BadRequest
            {
                Message = "La fecha ingresada es inválida"
            };

            var controller = new ComandaController(mockServiceComanda.Object,
                                                   mockServiceMercaderia.Object,
                                                   mockServiceFormaEntrega.Object);

            // Act
            var result = await controller.GetAll(fecha) as BadRequestObjectResult;

            // Assert
            result.StatusCode.Should().Be(400);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task GetAll_FechaIsNull_ShouldReturnListOfComandaResponseWithStatusCode200()
        {
            // Arrange
            var mockServiceComanda = new Mock<IServiceComanda>();
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceFormaEntrega = new Mock<IServiceFormaEntrega>();

            IList<ComandaResponse> expectedResponse = new List<ComandaResponse>()
            {
                new ComandaResponse()
                {
                    Fecha = DateTime.Now,
                    FormaEntrega = new FormaEntrega()
                    {
                        Descripcion = "A",
                        Id = 1
                    },
                    Id = Guid.NewGuid(),
                    Mercaderias = new List<MercaderiaComandaResponse>()
                    {
                        new MercaderiaComandaResponse()
                        {
                            Id = 1,
                            Nombre = "A",
                            Precio = 100
                        },
                        new MercaderiaComandaResponse()
                        {
                            Id = 2,
                            Nombre = "A",
                            Precio = 100
                        }
                    },
                    Total = 200
                },
                 new ComandaResponse()
                {
                    Fecha = DateTime.Now,
                    FormaEntrega = new FormaEntrega()
                    {
                        Descripcion = "A",
                        Id = 1
                    },
                    Id = Guid.NewGuid(),
                    Mercaderias = new List<MercaderiaComandaResponse>()
                    {
                        new MercaderiaComandaResponse()
                        {
                            Id = 1,
                            Nombre = "A",
                            Precio = 100
                        },
                        new MercaderiaComandaResponse()
                        {
                            Id = 2,
                            Nombre = "A",
                            Precio = 100
                        }
                    },
                    Total = 200
                }
            };

            mockServiceComanda.Setup(m => m.GetAll())
                              .ReturnsAsync(expectedResponse);

            var controller = new ComandaController(mockServiceComanda.Object,
                                                   mockServiceMercaderia.Object,
                                                   mockServiceFormaEntrega.Object);

            // Act
            var result = await controller.GetAll(null) as JsonResult;

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Theory]
        [InlineData("2024-07-15")] // Formato ISO (yyyy-MM-dd)
        [InlineData("15/07/2024")] // Formato dd/MM/yyyy
        [InlineData("2024/07/15")] // Formato yyyy/MM/dd
        [InlineData("15-Jul-2024")] // Formato dd-MMM-yyyy
        [InlineData("July 15, 2024")] // Formato MMMM dd, yyyy
        [InlineData("2024.07.15")] // Formato yyyy.MM.dd
        [InlineData("15.07.2024")] // Formato dd.MM.yyyy
        public async Task GetAll_FechaIsInCorrectFormat_ShouldReturnListOfComandaResponseWithStatusCode200(string? fecha)
        {
            // Arrange
            var mockServiceComanda = new Mock<IServiceComanda>();
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceFormaEntrega = new Mock<IServiceFormaEntrega>();

            IList<ComandaResponse> expectedResponse = new List<ComandaResponse>()
            {
                new ComandaResponse()
                {
                    Fecha = DateTime.Now,
                    FormaEntrega = new FormaEntrega()
                    {
                        Descripcion = "A",
                        Id = 1
                    },
                    Id = Guid.NewGuid(),
                    Mercaderias = new List<MercaderiaComandaResponse>()
                    {
                        new MercaderiaComandaResponse()
                        {
                            Id = 1,
                            Nombre = "A",
                            Precio = 100
                        },
                        new MercaderiaComandaResponse()
                        {
                            Id = 2,
                            Nombre = "A",
                            Precio = 100
                        }
                    },
                    Total = 200
                },
                 new ComandaResponse()
                {
                    Fecha = DateTime.Now,
                    FormaEntrega = new FormaEntrega()
                    {
                        Descripcion = "A",
                        Id = 1
                    },
                    Id = Guid.NewGuid(),
                    Mercaderias = new List<MercaderiaComandaResponse>()
                    {
                        new MercaderiaComandaResponse()
                        {
                            Id = 1,
                            Nombre = "A",
                            Precio = 100
                        },
                        new MercaderiaComandaResponse()
                        {
                            Id = 2,
                            Nombre = "A",
                            Precio = 100
                        }
                    },
                    Total = 200
                }
            };

            mockServiceComanda.Setup(m => m.GetComandaByDate(It.IsAny<DateTime>()))
                              .ReturnsAsync(expectedResponse);

            var controller = new ComandaController(mockServiceComanda.Object,
                                                   mockServiceMercaderia.Object,
                                                   mockServiceFormaEntrega.Object);

            // Act
            var result = await controller.GetAll(fecha) as JsonResult;

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task GetAll_FechaIsNull_ShouldReturnEmptyListWithStatusCode200()
        {
            // Arrange
            var mockServiceComanda = new Mock<IServiceComanda>();
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceFormaEntrega = new Mock<IServiceFormaEntrega>();

            IList<ComandaResponse> expectedResponse = new List<ComandaResponse>();

            mockServiceComanda.Setup(m => m.GetAll())
                              .ReturnsAsync(expectedResponse);

            var controller = new ComandaController(mockServiceComanda.Object,
                                                   mockServiceMercaderia.Object,
                                                   mockServiceFormaEntrega.Object);

            // Act
            var result = await controller.GetAll(null) as JsonResult;

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Theory]
        [InlineData("2024-07-15")] // Formato ISO (yyyy-MM-dd)
        [InlineData("15/07/2024")] // Formato dd/MM/yyyy
        [InlineData("2024/07/15")] // Formato yyyy/MM/dd
        [InlineData("15-Jul-2024")] // Formato dd-MMM-yyyy
        [InlineData("July 15, 2024")] // Formato MMMM dd, yyyy
        [InlineData("2024.07.15")] // Formato yyyy.MM.dd
        [InlineData("15.07.2024")] // Formato dd.MM.yyyy
        public async Task GetAll_FechaIsInCorrectFormat_ShouldReturnEmptyListResponseWithStatusCode200(string? fecha)
        {
            // Arrange
            var mockServiceComanda = new Mock<IServiceComanda>();
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceFormaEntrega = new Mock<IServiceFormaEntrega>();

            IList<ComandaResponse> expectedResponse = new List<ComandaResponse>();

            mockServiceComanda.Setup(m => m.GetComandaByDate(It.IsAny<DateTime>()))
                              .ReturnsAsync(expectedResponse);

            var controller = new ComandaController(mockServiceComanda.Object,
                                                   mockServiceMercaderia.Object,
                                                   mockServiceFormaEntrega.Object);

            // Act
            var result = await controller.GetAll(fecha) as JsonResult;

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }



    }
}
