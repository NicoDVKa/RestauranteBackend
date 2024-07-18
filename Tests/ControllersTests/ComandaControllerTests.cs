
using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
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
        [InlineData("13/13/2024")] // Formato dd/MM/yyyy
        public async Task GetAll_FechaIsInvalid_ShouldReturnBadRequest400(string? fecha)
        {
            // Arrange
            var mockServiceComanda = new Mock<IServiceComanda>();
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceFormaEntrega = new Mock<IServiceFormaEntrega>();

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
                    FormaEntrega = new Application.Models.Response.FormaEntrega()
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
                    FormaEntrega = new Application.Models.Response.FormaEntrega()
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
                    FormaEntrega = new Application.Models.Response.FormaEntrega()
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
                    FormaEntrega = new Application.Models.Response.FormaEntrega()
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
                FormaEntrega = new  Application.Models.Response.FormaEntrega()
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

        [Fact]
        public async Task CreateComanda_FormaEntregaDoesNotExist_ShouldReturnBadRequestWithStatusCode400()
        {
            // Arrange
            var mockServiceComanda = new Mock<IServiceComanda>();
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceFormaEntrega = new Mock<IServiceFormaEntrega>();

            var comandaRequest = new ComandaRequest()
            {
                FormaEntrega = 1,
                Mercaderias = new List<int>() { 1, 2, 3, 4, 3, 2, 1, 1 }
            };

            var expectedResponse = new BadRequest()
            {
                Message = $"No existe una forma de entrega con el ID {comandaRequest.FormaEntrega}"
            };

            var controller = new ComandaController(mockServiceComanda.Object,
                                                   mockServiceMercaderia.Object,
                                                   mockServiceFormaEntrega.Object);

            // Act
            var result = await controller.CreateComanda(comandaRequest) as JsonResult;

            // Assert
            result.StatusCode.Should().Be(400);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task CreateComanda_MercaderiasListIsEmpty_ShouldReturnBadRequestWithStatusCode400()
        {
            // Arrange
            var mockServiceComanda = new Mock<IServiceComanda>();
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceFormaEntrega = new Mock<IServiceFormaEntrega>();

            var comandaRequest = new ComandaRequest()
            {
                FormaEntrega = 1,
                Mercaderias = new List<int>()
            };

            var expectedResponse = new BadRequest()
            {
                Message = "Ingrese mercaderias"
            };

            var formaEntregaResponse = new Application.Models.Response.FormaEntrega()
            {
                Id = 1,
                Descripcion = "a"
            };

            mockServiceFormaEntrega.Setup(f => f.GetFormaEntregaById(It.IsAny<int>()))
                                   .ReturnsAsync(formaEntregaResponse);

            var controller = new ComandaController(mockServiceComanda.Object,
                                                   mockServiceMercaderia.Object,
                                                   mockServiceFormaEntrega.Object);

            // Act
            var result = await controller.CreateComanda(comandaRequest) as JsonResult;

            // Assert
            result.StatusCode.Should().Be(400);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task CreateComanda_MercaderiasListIsNull_ShouldReturnBadRequestWithStatusCode400()
        {
            // Arrange
            var mockServiceComanda = new Mock<IServiceComanda>();
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceFormaEntrega = new Mock<IServiceFormaEntrega>();

            var comandaRequest = new ComandaRequest()
            {
                FormaEntrega = 1,
                Mercaderias = null
            };

            var expectedResponse = new BadRequest()
            {
                Message = "Ingrese mercaderias"
            };


            var formaEntregaResponse = new  Application.Models.Response.FormaEntrega()
            {
                Id = 1,
                Descripcion = "a"
            };

            mockServiceFormaEntrega.Setup(f => f.GetFormaEntregaById(It.IsAny<int>()))
                                   .ReturnsAsync(formaEntregaResponse);


            var controller = new ComandaController(mockServiceComanda.Object,
                                                   mockServiceMercaderia.Object,
                                                   mockServiceFormaEntrega.Object);

            // Act
            var result = await controller.CreateComanda(comandaRequest) as JsonResult;

            // Assert
            result.StatusCode.Should().Be(400);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task CreateComanda_MercaderiaIdDoesNotExist_ShouldReturnBadRequestWithStatusCode400()
        {
            // Arrange
            var mockServiceComanda = new Mock<IServiceComanda>();
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceFormaEntrega = new Mock<IServiceFormaEntrega>();

            var comandaRequest = new ComandaRequest()
            {
                FormaEntrega = 1,
                Mercaderias = new List<int>() { 1,2,3,4,5,3,2}
            };

            var expectedResponse = new BadRequest()
            {
                Message = $"No existe una mercaderia con el ID {comandaRequest.Mercaderias.FirstOrDefault()}"
            };


            var formaEntregaResponse = new Application.Models.Response.FormaEntrega()
            {
                Id = 1,
                Descripcion = "a"
            };

            mockServiceFormaEntrega.Setup(f => f.GetFormaEntregaById(It.IsAny<int>()))
                                   .ReturnsAsync(formaEntregaResponse);


            var controller = new ComandaController(mockServiceComanda.Object,
                                                   mockServiceMercaderia.Object,
                                                   mockServiceFormaEntrega.Object);

            // Act
            var result = await controller.CreateComanda(comandaRequest) as JsonResult;

            // Assert
            result.StatusCode.Should().Be(400);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task CreateComanda_ComandaRequestIsValid_ShouldReturnComandaResponseWithStatusCode201()
        {
            // Arrange
            var mockServiceComanda = new Mock<IServiceComanda>();
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceFormaEntrega = new Mock<IServiceFormaEntrega>();

            var comandaRequest = new ComandaRequest()
            {
                FormaEntrega = 1,
                Mercaderias = new List<int>() { 1, 2 }
            };

            var formaEntregaResponse = new Application.Models.Response.FormaEntrega()
            {
                Id = 1,
                Descripcion = "a"
            };

            var mercaderiaResponse = new MercaderiaResponse()
            {
                Id = 1,
                Nombre = "A",
                Precio = 50,
                Preparacion = "a",
                Imagen = "a",
                Ingredientes = "a",
                Tipo = new TipoMercaderiaResponse()
                {
                    Id = 1,
                    Descripcion = "q"
                }

            };

            var expectedResponse = new ComandaResponse()
            {
                Id = Guid.NewGuid(),
                Fecha = DateTime.Now,
                FormaEntrega = formaEntregaResponse,
                Total = 100,
                Mercaderias = new List<MercaderiaComandaResponse>()
                {
                    new MercaderiaComandaResponse()
                    {
                        Id = 1,
                        Nombre = mercaderiaResponse.Nombre,
                        Precio = mercaderiaResponse.Precio
                    },
                    new MercaderiaComandaResponse()
                    {
                        Id = 2,
                        Nombre = mercaderiaResponse.Nombre,
                        Precio = mercaderiaResponse.Precio
                    },
                }
            };

            

            mockServiceFormaEntrega.Setup(f => f.GetFormaEntregaById(It.IsAny<int>()))
                                   .ReturnsAsync(formaEntregaResponse);

            mockServiceComanda.Setup(c => c.CreateComanda(It.IsAny<ComandaRequest>(), It.IsAny<double>()))
                              .ReturnsAsync(expectedResponse);

            mockServiceMercaderia.Setup(m => m.GetMercaderiaById(It.IsAny<int>()))
                                 .ReturnsAsync(mercaderiaResponse);

            var controller = new ComandaController(mockServiceComanda.Object,
                                                   mockServiceMercaderia.Object,
                                                   mockServiceFormaEntrega.Object);

            // Act
            var result = await controller.CreateComanda(comandaRequest) as JsonResult;

            // Assert
            result.StatusCode.Should().Be(201);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }
    }
    
}
