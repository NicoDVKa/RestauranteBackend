
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

        [Fact]
        public async Task GetComandaById_IdDoesNotExist_ShouldReturnBadRequestWithStatusCode404()
        {
            // Arrange
            var mockServiceComanda = new Mock<IServiceComanda>();
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceFormaEntrega = new Mock<IServiceFormaEntrega>();

            var id = Guid.NewGuid();

            var expectedResponse = new BadRequest()
            {
                Message = $"No existe una comanda con el ID {id}"
            };

            var controller = new ComandaController(mockServiceComanda.Object,
                                                   mockServiceMercaderia.Object,
                                                   mockServiceFormaEntrega.Object);

            // Act
            var result = await controller.GetComandaById(id) as JsonResult;

            // Assert
            result.StatusCode.Should().Be(404);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task GetComandaById_ShouldReturnComandaGetResponseWithStatusCode200()
        {
            // Arrange
            var mockServiceComanda = new Mock<IServiceComanda>();
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceFormaEntrega = new Mock<IServiceFormaEntrega>();

            var id = Guid.NewGuid();

            ComandaGetResponse expectedResponse = new ComandaGetResponse()
            {
                Fecha = DateTime.Now,
                Id = Guid.NewGuid(),
                Total = 100,
                FormaEntrega = new FormaEntrega()
                {
                    Id = 1,
                    Descripcion = "a"
                },
                Mercaderias = new List<MercaderiaGetResponse>()
                {
                   new MercaderiaGetResponse()
                   {
                       Id = 1,
                       Imagen = "A",
                       Nombre = "1",
                       Precio = 100,
                       Tipo = new TipoMercaderiaResponse()
                       {
                           Id = 1,
                           Descripcion = "a"
                       }
                   },
                   new MercaderiaGetResponse()
                   {
                       Id = 2,
                       Imagen = "b",
                       Nombre = "11",
                       Precio = 1000,
                       Tipo = new TipoMercaderiaResponse()
                       {
                           Id = 2,
                           Descripcion = "b"
                       }
                   }
                }
            };

            mockServiceComanda.Setup(m => m.GetComandaById(It.IsAny<Guid>()))
                              .ReturnsAsync(expectedResponse);

            var controller = new ComandaController(mockServiceComanda.Object,
                                                   mockServiceMercaderia.Object,
                                                   mockServiceFormaEntrega.Object);

            // Act
            var result = await controller.GetComandaById(id) as JsonResult;

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

    }
}
