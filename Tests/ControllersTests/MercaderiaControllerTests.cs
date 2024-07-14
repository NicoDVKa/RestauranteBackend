

using Application.Interfaces;
using Application.Models.Response;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;
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
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task CreateMercaderia_MercaderiaIsNotValid_ShouldReturnBadRequestWithStatusCode400()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task CreateMercaderia_TipoMercaderiaDoesNotExist_ShouldReturnBadRequestWithStatusCode400()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task CreateMercaderia_NombreMercaderiaDoesExist_ShouldReturnBadRequestWithStatusCode409()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task GetMercaderiaById_IdIsNegative_ShouldReturnBadRequestWithStatusCode400()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task GetMercaderiaById_IdDoesNotExist_ShouldReturnBadRequestWithStatusCode404()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task GetMercaderiaById__ShouldReturnMercaderiaResponseWithStatusCode200()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task UpdateMercaderia_ShouldReturnMercaderiaResponseWithStatusCode200()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task UpdateMercaderia_MercaderiaIsNotValid_ShouldReturnBadRequestWithStatusCode400()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
        }

        [Fact]
        public async Task UpdateMercaderia_MercaderiaDoesNotExist_ShouldReturnBadRequestWithStatusCode404()
        {
            // TODO

            // Arrange

            //Act

            // Assert
            Assert.Fail();
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
