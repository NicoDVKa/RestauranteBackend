

using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Azure.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using RestauranteWebApi.Controllers;

namespace Tests.ControllersTests
{
    public class MercaderiaControllerTests
    {
        [Fact]
        public async Task SearchMercaderia_ShouldReturnListOfMercaderiaGetResponse()
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            IList<MercaderiaGetResponse> expectedListOfMercaderiaGetResponse = new List<MercaderiaGetResponse>()
            {
                new MercaderiaGetResponse()
                {
                    Id = 1,
                    Imagen = "A",
                    Nombre = "B",
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
                    Imagen = "Aa",
                    Nombre = "Bb",
                    Precio = 1000,
                    Tipo = new TipoMercaderiaResponse()
                    {
                        Id = 11,
                        Descripcion = "aa"
                    }
                },
                new MercaderiaGetResponse()
                {
                    Id = 3,
                    Imagen = "Aaa",
                    Nombre = "Bbb",
                    Precio = 10000,
                    Tipo = new TipoMercaderiaResponse()
                    {
                        Id = 111,
                        Descripcion = "aaa"
                    }
                }
            };

            mockServiceMercaderia.Setup(m => m.SearchMercaderia(It.IsAny<int?>(), It.IsAny<string?>(), It.IsAny<string>()))
                                 .ReturnsAsync(expectedListOfMercaderiaGetResponse);

            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.SearchMercaderia(It.IsAny<int?>(), It.IsAny<string?>(), "ASC") as JsonResult;
            var mercaderia = result.Value as IList<MercaderiaGetResponse>;

            // Assert
            result.StatusCode.Should().Be(200);
            mercaderia.Should().BeEquivalentTo(expectedListOfMercaderiaGetResponse);
        }

        [Fact]
        public async Task SearchMercaderia_ShouldReturnEmptyList()
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            var expectedResponse = new BadRequest()
            {
                Message = "Orden inválido"
            };

            IList<MercaderiaGetResponse> mercaderiaGetResponses = new List<MercaderiaGetResponse>();

            mockServiceMercaderia.Setup(m => m.SearchMercaderia(It.IsAny<int?>(), It.IsAny<string?>(), It.IsAny<string>()))
                                 .ReturnsAsync(mercaderiaGetResponses);

            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.SearchMercaderia(It.IsAny<int?>(), It.IsAny<string?>(), "ASC") as JsonResult;
            var mercaderia = result.Value as IList<MercaderiaGetResponse>;

            // Assert
            result.StatusCode.Should().Be(200);
            mercaderia.Should().BeEmpty();
        }

        [Theory]
        [InlineData("asdasa")]
        [InlineData("asc1")]
        [InlineData("desc1")]
        public async Task SearchMercaderia_OrdenIsNotValid_ShouldReturnBadRequest(string? invalidOrden)
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            var expectedResponse = new BadRequest()
            {
                Message = "Orden inválido"
            };

            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.SearchMercaderia(It.IsAny<int?>(), It.IsAny<string?>(), invalidOrden);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            jsonResult.StatusCode.Should().Be(400);
            jsonResult.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task CreateMercaderia_MercaderiaIsValid_ShouldReturnMercaderiaResponseWithStatusCode201()
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            var expectedMercaderiaResponse = new MercaderiaResponse()
            {
                Id = 1,
                Nombre = "a",
                Precio = 1,
                Imagen = "a",
                Ingredientes = "a",
                Preparacion = "a",
                Tipo = new TipoMercaderiaResponse()
                {
                    Id = 1,
                    Descripcion = "a",
                }
            };

            var mercaderiaRequest = new MercaderiaRequest()
            {
                Nombre = "a",
                Precio = 1,
                Imagen = "a",
                Ingredientes = "a",
                Preparacion = "a",
                Tipo = 1
            };

            var tipoMercaderiaResponse = new TipoMercaderiaResponse()
            {
                Id = 1,
                Descripcion = "a",
            };

            mockServiceMercaderia.Setup(m => m.CreateMercaderia(It.IsAny<MercaderiaRequest>()))
                                 .ReturnsAsync(expectedMercaderiaResponse);
            
            mockServiceValidateMercaderia.Setup(m => m.MercaderiaIsValid(It.IsAny<MercaderiaRequest>(), false))
                                         .ReturnsAsync(true);

            mockServiceTipoMercaderia.Setup(m => m.GetTipoMercaderiaById(It.IsAny<int>()))
                                     .ReturnsAsync(tipoMercaderiaResponse);

            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.CreateMercaderia(mercaderiaRequest) as JsonResult;


            // Assert
            result.StatusCode.Should().Be(201);
            result.Value.Should().BeEquivalentTo(expectedMercaderiaResponse);
        }

        [Fact]
        public async Task CreateMercaderia_MercaderiaIsNotValid_ShouldReturnBadRequestWithStatusCode400()
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            var mercaderiaInvalidRequest = new MercaderiaRequest()
            {
                Nombre = It.IsAny<string>(),
                Precio = It.IsAny<double>(),
                Imagen = It.IsAny<string>(),
                Ingredientes = It.IsAny<string>(),
                Preparacion = It.IsAny<string>(),
                Tipo = It.IsAny<int>()
            };

            var expectedResponse = new BadRequest()
            {
                Message = "Algun error en uno de los campos"
            };

            mockServiceValidateMercaderia.Setup(m => m.MercaderiaIsValid(It.IsAny<MercaderiaRequest>(), false))
                                         .ReturnsAsync(false);

            mockServiceValidateMercaderia.Setup(m => m.GetError())
                                    .Returns(expectedResponse.Message);

            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.CreateMercaderia(mercaderiaInvalidRequest) as JsonResult;


            // Assert
            result.StatusCode.Should().Be(400);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task CreateMercaderia_TipoMercaderiaDoesNotExist_ShouldReturnBadRequestWithStatusCode400()
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            var mercaderiaRequest = new MercaderiaRequest()
            {
                Nombre = "a",
                Precio = 1,
                Imagen = "a",
                Ingredientes = "a",
                Preparacion = "a",
                Tipo = 1
            };

            var expectedResponse = new BadRequest()
            {
                Message = $"No existe un tipo de mercaderia con el ID {mercaderiaRequest.Tipo}"
            };

            mockServiceValidateMercaderia.Setup(m => m.MercaderiaIsValid(It.IsAny<MercaderiaRequest>(), false))
                                         .ReturnsAsync(true);

            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.CreateMercaderia(mercaderiaRequest) as JsonResult;


            // Assert
            result.StatusCode.Should().Be(400);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task CreateMercaderia_NombreMercaderiaDoesExist_ShouldReturnBadRequestWithStatusCode409()
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            var mercaderiaResponse = new MercaderiaResponse()
            {
                Id = 1,
                Nombre = "a",
                Precio = 1,
                Imagen = "a",
                Ingredientes = "a",
                Preparacion = "a",
                Tipo = new TipoMercaderiaResponse()
                {
                    Id = 1,
                    Descripcion = "a",
                }
            };

            var mercaderiaRequest = new MercaderiaRequest()
            {
                Nombre = "a",
                Precio = 1,
                Imagen = "a",
                Ingredientes = "a",
                Preparacion = "a",
                Tipo = 1
            };

            var tipoMercaderiaResponse = new TipoMercaderiaResponse()
            {
                Id = 1,
                Descripcion = "a",
            };

            var expectedResponse = new BadRequest()
            {
               Message = "Ya existe una mercaderia con ese nombre"
            };

            mockServiceValidateMercaderia.Setup(m => m.MercaderiaIsValid(It.IsAny<MercaderiaRequest>(), false))
                                         .ReturnsAsync(true);

            mockServiceTipoMercaderia.Setup(m => m.GetTipoMercaderiaById(It.IsAny<int>()))
                                     .ReturnsAsync(tipoMercaderiaResponse);

            mockServiceMercaderia.Setup(m => m.GetMercaderiaByName(It.IsAny<string>()))
                                 .ReturnsAsync(mercaderiaResponse);

            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.CreateMercaderia(mercaderiaRequest) as JsonResult;


            // Assert
            result.StatusCode.Should().Be(409);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public async Task GetMercaderiaById_IdIsNegativeOrZero_ShouldReturnBadRequestWithStatusCode400(int invalidId)
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            var expectedResponse = new BadRequest()
            {
                Message = "Ingreso un ID inválido"
            };

            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.GetMercaderiaById(invalidId) as JsonResult;


            // Assert
            result.StatusCode.Should().Be(400);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task GetMercaderiaById_IdDoesNotExist_ShouldReturnBadRequestWithStatusCode404()
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            var id = 1;

            var expectedResponse = new BadRequest()
            {
                Message = $"No existe una mercaderia con el ID {id}"
            };

            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.GetMercaderiaById(id) as JsonResult;


            // Assert
            result.StatusCode.Should().Be(404);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task GetMercaderiaById__ShouldReturnMercaderiaResponseWithStatusCode200()
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            var id = 1;

            var expectedMercaderia = new MercaderiaResponse()
            {
                Id = 1,
                Nombre = "a",
                Precio = 1,
                Imagen = "a",
                Ingredientes = "a",
                Preparacion = "a",
                Tipo = new TipoMercaderiaResponse()
                {
                    Id = 1,
                    Descripcion = "a",
                }
            };

            mockServiceMercaderia.Setup(m => m.GetMercaderiaById(It.IsAny<int>()))
                                 .ReturnsAsync(expectedMercaderia);

            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.GetMercaderiaById(id) as JsonResult;


            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(expectedMercaderia);
        }

        [Fact]
        public async Task UpdateMercaderia_ShouldReturnMercaderiaResponseWithStatusCode200()
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            var expectedMercaderiaResponse = new MercaderiaResponse()
            {
                Id = 1,
                Nombre = "a",
                Precio = 1,
                Imagen = "a",
                Ingredientes = "a",
                Preparacion = "a",
                Tipo = new TipoMercaderiaResponse()
                {
                    Id = 1,
                    Descripcion = "a",
                }
            };

            var mercaderiaRequest = new MercaderiaRequest()
            {
                Nombre = "a",
                Precio = 1,
                Imagen = "a",
                Ingredientes = "a",
                Preparacion = "a",
                Tipo = 1
            };

            var tipoMercaderiaResponse = new TipoMercaderiaResponse()
            {
                Id = 1,
                Descripcion = "a",
            };

            mockServiceMercaderia.Setup(m => m.UpdateMercaderia(It.IsAny<int>(),It.IsAny<MercaderiaRequest>()))
                                 .ReturnsAsync(expectedMercaderiaResponse);

            mockServiceValidateMercaderia.Setup(m => m.MercaderiaIsValid(It.IsAny<MercaderiaRequest>(), true))
                                         .ReturnsAsync(true);

            mockServiceTipoMercaderia.Setup(m => m.GetTipoMercaderiaById(It.IsAny<int>()))
                                     .ReturnsAsync(tipoMercaderiaResponse);

            mockServiceMercaderia.Setup(m => m.GetMercaderiaById(It.IsAny<int>()))
                                 .ReturnsAsync(expectedMercaderiaResponse);

            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.UpdateMercaderia(It.IsAny<int>(),mercaderiaRequest) as JsonResult;


            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(expectedMercaderiaResponse);
        }

        [Fact]
        public async Task UpdateMercaderia_MercaderiaIsNotValid_ShouldReturnBadRequestWithStatusCode400()
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            var expectedResponse = new BadRequest()
            {
               Message = "Algun error en uno de los campos"
            };

            var mercaderiaRequest = new MercaderiaRequest()
            {
                Nombre = "a",
                Precio = 1,
                Imagen = "a",
                Ingredientes = "a",
                Preparacion = "a",
                Tipo = 1
            };

            mockServiceValidateMercaderia.Setup(m => m.MercaderiaIsValid(It.IsAny<MercaderiaRequest>(), true))
                                         .ReturnsAsync(false);

            mockServiceValidateMercaderia.Setup(m => m.GetError())
                                         .Returns(expectedResponse.Message);

            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.UpdateMercaderia(It.IsAny<int>(), mercaderiaRequest) as JsonResult;


            // Assert
            result.StatusCode.Should().Be(400);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task UpdateMercaderia_MercaderiaDoesNotExist_ShouldReturnBadRequestWithStatusCode404()
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            var id = 1;

            var mercaderiaRequest = new MercaderiaRequest()
            {
                Nombre = "a",
                Precio = 1,
                Imagen = "a",
                Ingredientes = "a",
                Preparacion = "a",
                Tipo = 1
            };

            var expectedResponse = new BadRequest()
            {
                Message = $"No existe una mercaderia con el ID {id}"
            };


            mockServiceValidateMercaderia.Setup(m => m.MercaderiaIsValid(It.IsAny<MercaderiaRequest>(), true))
                                         .ReturnsAsync(true);

            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.UpdateMercaderia(id, mercaderiaRequest) as JsonResult;


            // Assert
            result.StatusCode.Should().Be(404);
            result.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task UpdateMercaderia_TipoMercaderiaDoesNotExist_ShouldReturnBadRequestWithStatusCode400()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task UpdateMercaderia_NombreMercaderiaAlreadyInUse_ShouldReturnBadRequestWithStatusCode409()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task DeleteMercaderia_MercaderiaDoesNotExist_ShouldReturnBadRequestWithStatusCode400()
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            var idToDelete = It.IsAny<int>();

            var expectedResponse = new BadRequest()
            {
                Message = $"No existe una mercaderia con el ID {idToDelete}"
            };

            mockServiceMercaderia.Setup(m => m.GetMercaderiaById(It.IsAny<int>()))
                                 .ReturnsAsync((MercaderiaResponse)null);


            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.DeleteMercaderia(idToDelete);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            jsonResult.StatusCode.Should().Be(400);
            jsonResult.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task DeleteMercaderia_MercaderiaIsOnComanda_ShouldReturnBadRequestWithStatusCode409()
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            var expectedMercaderia = new MercaderiaResponse()
            {
                Id = It.IsAny<int>(),
                Nombre = It.IsAny<string>(),
                Precio = It.IsAny<double>(),
                Imagen = It.IsAny<string>(),
                Ingredientes = It.IsAny<string>(),
                Preparacion = It.IsAny<string>(),
                Tipo = It.IsAny<TipoMercaderiaResponse>()
            };

            IList<MercaderiaComandaResponse> comandaMercaderiaList = new List<MercaderiaComandaResponse>()
            {
                new MercaderiaComandaResponse
                {
                    Id = It.IsAny<int>(),
                    Nombre = It.IsAny<string>(),
                    Precio  = It.IsAny<double>(),
                },
                new MercaderiaComandaResponse
                {
                    Id = It.IsAny<int>(),
                    Nombre = It.IsAny<string>(),
                    Precio  = It.IsAny<double>(),
                },
                new MercaderiaComandaResponse
                {
                    Id = It.IsAny<int>(),
                    Nombre = It.IsAny<string>(),
                    Precio  = It.IsAny<double>(),
                }
            };

            var expectedResponse = new BadRequest()
            {
                Message = "No se puede eliminar la mercaderia ya que hay comandas que dependen de ella"
            };

            mockServiceMercaderia.Setup(m => m.GetMercaderiaById(It.IsAny<int>()))
                                 .ReturnsAsync(expectedMercaderia);

            mockServiceMercaderia.Setup(m => m.DeleteMercaderia(It.IsAny<int>()))
                                 .ReturnsAsync(expectedMercaderia);

            mockServiceComandaMercaderia.Setup(m => m.GetComandaMercaderiaByMercaderiaId(It.IsAny<int>()))
                                        .ReturnsAsync(comandaMercaderiaList);



            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.DeleteMercaderia(It.IsAny<int>());


            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            jsonResult.StatusCode.Should().Be(409);
            jsonResult.Value.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task DeleteMercaderia_ShouldReturnMercaderiaResponseWithStatusCode200()
        {
            // Arrange
            var mockServiceMercaderia = new Mock<IServiceMercaderia>();
            var mockServiceTipoMercaderia = new Mock<IServiceTipoMercaderia>();
            var mockServiceComandaMercaderia = new Mock<IServiceComandaMercaderia>();
            var mockServiceValidateMercaderia = new Mock<IServiceValidateMercaderia>();

            var expectedMercaderia = new MercaderiaResponse()
            {
                Id = It.IsAny<int>(),
                Nombre = It.IsAny<string>(),
                Precio = It.IsAny<double>(),
                Imagen = It.IsAny<string>(),
                Ingredientes = It.IsAny<string>(),
                Preparacion = It.IsAny<string>(),
                Tipo = It.IsAny<TipoMercaderiaResponse>()
            };

            IList<MercaderiaComandaResponse> comandaMercaderiaList = new List<MercaderiaComandaResponse>();

            mockServiceMercaderia.Setup(m => m.GetMercaderiaById(It.IsAny<int>()))
                                 .ReturnsAsync(expectedMercaderia);

            mockServiceMercaderia.Setup(m => m.DeleteMercaderia(It.IsAny<int>()))
                                 .ReturnsAsync(expectedMercaderia);

            mockServiceComandaMercaderia.Setup(m => m.GetComandaMercaderiaByMercaderiaId(It.IsAny<int>()))
                                        .ReturnsAsync(comandaMercaderiaList);



            var controller = new MercaderiaController(mockServiceMercaderia.Object,
                                                      mockServiceTipoMercaderia.Object,
                                                      mockServiceComandaMercaderia.Object,
                                                      mockServiceValidateMercaderia.Object);

            //Act
            var result = await controller.DeleteMercaderia(It.IsAny<int>());


            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            jsonResult.StatusCode.Should().Be(200);
            jsonResult.Value.Should().BeEquivalentTo(expectedMercaderia);
        }
    }
}
