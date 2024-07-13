

using Application.Models.Request;
using Application.UseCases;
using FluentAssertions;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Headers;

namespace Tests.ServicesTests
{
    public class ServiceValidateMercaderiaTests
    {
        [Theory]
        [InlineData("Nombre", "Nicolas", 50, false)]
        [InlineData("Ingredientes", "aa", 2, false)]
        [InlineData("Preparacion", "aaa", 3, false)]
        [InlineData("Imagen", "http//....", 255, false)]
        [InlineData("Imagen", "http//....", 255, true)]
        [InlineData("Imagen", null, 255, true)]
        public void StringIsValid_ShouldReturnTrue(String tag, String? verify, int maxLength, Boolean allowNull)
        {
            // Arrange
            var mockHttpClient =  new Mock<IHttpClientFactory>();
            var service = new ServiceValidateMercaderia(mockHttpClient.Object);

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
        public void StringIsValid_AllowNullIsFalseAndTagIsNull_ShouldReturnFalse(String tag, 
                                                                                 String? verify, 
                                                                                 int maxLength, 
                                                                                 Boolean allowNull)
        {
            // Arrange
            String expectedError = $"{tag}-Ingrese un valor";
            var mockHttpClient = new Mock<IHttpClientFactory>();
            var service = new ServiceValidateMercaderia(mockHttpClient.Object);

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
        public void StringIsValid_AllowNullIsFalseAndTagIsEmpty_ShouldReturnFalse(String tag, 
                                                                                  String? verify, 
                                                                                  int maxLength, 
                                                                                  Boolean allowNull)
        {
            // Arrange
            String expectedError = $"{tag}-Ingrese un valor";
            var mockHttpClient = new Mock<IHttpClientFactory>();
            var service = new ServiceValidateMercaderia(mockHttpClient.Object);

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
        public void StringIsValid_TagLengthIsGreaterThanMaxLength_ShouldReturnFalse(String tag, 
                                                                                    String? verify, 
                                                                                    int maxLength, 
                                                                                    Boolean allowNull)
        {
            // Arrange
            String expectedError = $"{tag}-El campo no cumple con el formato";
            var mockHttpClient = new Mock<IHttpClientFactory>();
            var service = new ServiceValidateMercaderia(mockHttpClient.Object);

            // Act
            var result = service.StringIsValid(tag, verify, maxLength, allowNull);
            var error = service.GetError();

            //Assert
            result.Should().BeFalse();
            error.Should().BeEquivalentTo(expectedError);
        }


        [Theory]
        [InlineData("nico",100,"aaa","")]
        [InlineData("", 100, "aaa", "algo")]
        [InlineData("nico", 100, "", "algo")]
        [InlineData(null, 1, "aa", "")]
        public async Task MercaderiaIsValid_AllowNullIsTrueWithImageNull_ShouldReturnFalse(String? nombre, 
                                                                                           Double precio, 
                                                                                           String? ingredientes, 
                                                                                           String? preparacion)
        {
            // Arrange
            MercaderiaRequest mercaderiaRequest = new MercaderiaRequest()
            {
                Nombre = nombre,
                Precio = precio ,
                Ingredientes = ingredientes,
                Preparacion = preparacion
            };

            Boolean allowNull = true;

            var mockHttpClient = new Mock<IHttpClientFactory>();
            var service = new ServiceValidateMercaderia(mockHttpClient.Object);

            // Act
            var result = await service.MercaderiaIsValid(mercaderiaRequest,allowNull);
            var error = service.GetError();

            // Assert
            result.Should().BeFalse();
            error.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData("nico", 100, "aaa", "a")]
        [InlineData("a", 100, "aaa", "algo")]
        [InlineData("nico", 100, null, "algo")]
        [InlineData(null, 1, "aa", null)]
        [InlineData(null, 1, null, null)]
        public async Task MercaderiaIsValid_AllowNullIsTrueWithImageNull_ShouldReturnTrue(String? nombre, 
                                                                                          Double precio, 
                                                                                          String? ingredientes, 
                                                                                          String? preparacion)
        {
            // Arrange
            MercaderiaRequest mercaderiaRequest = new MercaderiaRequest()
            {
                Nombre = nombre,
                Precio = precio,
                Ingredientes = ingredientes,
                Preparacion = preparacion
            };

            Boolean allowNull = true;

            var mockHttpClient = new Mock<IHttpClientFactory>();
            var service = new ServiceValidateMercaderia(mockHttpClient.Object);

            // Act
            var result = await service.MercaderiaIsValid(mercaderiaRequest, allowNull);
            var error = service.GetError();

            // Assert
            result.Should().BeTrue();
            error.Should().BeNullOrEmpty();
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(0)]
        [InlineData(int.MinValue)]
        public async Task MercaderiaIsValid_PrecioIsInvalid_ShouldBeReturnFalse(double precio)
        {
            // Arrange
            MercaderiaRequest mercaderiaRequest = new MercaderiaRequest()
            {     
                Precio = precio,
            };
            String expectedError = $"precio-El precio ingresado no cumple con el formato";
            Boolean allowNull = true;

            var mockHttpClient = new Mock<IHttpClientFactory>();
            var service = new ServiceValidateMercaderia(mockHttpClient.Object);

            // Act
            var result = await service.MercaderiaIsValid(mercaderiaRequest, allowNull);
            var error = service.GetError();

            // Assert
            result.Should().BeFalse();
            error.Should().BeEquivalentTo(expectedError);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public async Task MercaderiaIsValid_PrecioIsInvalid_ShouldBeReturnTrue(double precio)
        {
            // Arrange
            MercaderiaRequest mercaderiaRequest = new MercaderiaRequest()
            {
                Precio = precio,
            };
          
            Boolean allowNull = true;

            var mockHttpClient = new Mock<IHttpClientFactory>();
            var service = new ServiceValidateMercaderia(mockHttpClient.Object);

            // Act
            var result = await service.MercaderiaIsValid(mercaderiaRequest, allowNull);
            var error = service.GetError();

            // Assert
            result.Should().BeTrue();
            error.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task MercaderiaIsValid_ImagenURLNotFound_ShouldBeReturnFalse()
        {
            // Arrange
            MercaderiaRequest mercaderiaRequest = new MercaderiaRequest()
            {
                Precio = 1,
                Imagen = "http://example.com",
            };

            Boolean allowNull = true;

            var expectedError = "imagen-La URL de la imagen ingresada no es valida";


            var response = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("It isn't worked :( ")
            };

            // Mock the HttpMessageHandler to return a 404 response for any GET request
            var _httpMessageHandler = new Mock<HttpMessageHandler>();
            _httpMessageHandler.Protected()
              .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                  ItExpr.IsAny<CancellationToken>()
               )
              .ReturnsAsync(new HttpResponseMessage
              {
                  StatusCode = HttpStatusCode.NotFound,
                  Content = new StringContent("It isn't worked :( ")
              });

            var httpClient = new HttpClient(_httpMessageHandler.Object);

            // Mock the IHttpClientFactory to return the mocked HttpClient
            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var service = new ServiceValidateMercaderia(mockFactory.Object);

            // Act
            var result = await service.MercaderiaIsValid(mercaderiaRequest, allowNull);
            var error = service.GetError();

            // Assert
            result.Should().BeFalse();
            error.Should().BeEquivalentTo(expectedError);
        }

        [Fact]
        public async Task MercaderiaIsValid_ImagenURLIsNotValid_ShouldBeReturnFalse()
        {
            // Arrange
            MercaderiaRequest mercaderiaRequest = new MercaderiaRequest()
            {
                Precio = 1,
                Imagen = "aaa",
            };

            Boolean allowNull = true;

            var expectedError = "imagen-Ha ocurrido un error con la URL de la imagen";

            var httpClient = new HttpClient();

            // Mock the IHttpClientFactory to return the mocked HttpClient
            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var service = new ServiceValidateMercaderia(mockFactory.Object);

            // Act
            var result = await service.MercaderiaIsValid(mercaderiaRequest, allowNull);
            var error = service.GetError();

            // Assert
            result.Should().BeFalse();
            error.Should().BeEquivalentTo(expectedError);
        }

        [Theory]
        [InlineData("application/a")]
        [InlineData("xx/json")]
        [InlineData("video/jpg")]
        [InlineData("html/jpg")]
        public async Task MercaderiaIsValid_URLIsNotImage_ShouldBeReturnFalse(String mediaType)
        {
            // Arrange
            MercaderiaRequest mercaderiaRequest = new MercaderiaRequest()
            {
                Precio = 1,
                Imagen = "http://example.com",
            };
            Boolean allowNull = true;
            var expectedError = "imagen-La URL no devuelve una imagen";

            var _httpMessageHandler = new Mock<HttpMessageHandler>();
            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(string.Empty)
                    {
                        Headers = { ContentType = new MediaTypeHeaderValue(mediaType) }
                    }
                });

            var httpClient = new HttpClient(_httpMessageHandler.Object);

            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var service = new ServiceValidateMercaderia(mockFactory.Object);

            // Act
            var result = await service.MercaderiaIsValid(mercaderiaRequest, allowNull);
            var error = service.GetError();

            // Assert
            result.Should().BeFalse();
            error.Should().BeEquivalentTo(expectedError);
        }

        [Theory]
        [InlineData("image/a")]
        [InlineData("image/json")]
        [InlineData("image/xx")]
        [InlineData("image/kp")]
        public async Task MercaderiaIsValid_URLIsAnImageButBadExtension_ShouldBeReturnFalse(String mediaType)
        {
            // Arrange
            MercaderiaRequest mercaderiaRequest = new MercaderiaRequest()
            {
                Precio = 1,
                Imagen = "http://example.com",
            };
            Boolean allowNull = true;
            var expectedError = "imagen-La URL no devuelve una imagen con el formato valido";

            var _httpMessageHandler = new Mock<HttpMessageHandler>();
            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(string.Empty)
                    {
                        Headers = { ContentType = new MediaTypeHeaderValue(mediaType) }
                    }
                });

            var httpClient = new HttpClient(_httpMessageHandler.Object);

            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var service = new ServiceValidateMercaderia(mockFactory.Object);

            // Act
            var result = await service.MercaderiaIsValid(mercaderiaRequest, allowNull);
            var error = service.GetError();

            // Assert
            result.Should().BeFalse();
            error.Should().BeEquivalentTo(expectedError);
        }

        [Theory]
        [InlineData("nico", 100, "aaa", "a")]
        [InlineData("a", 100, "aaa", "algo")]
        [InlineData("nico", 100, null, "algo")]
        [InlineData(null, 1, "aa", null)]
        [InlineData(null, 1, null, null)]
        public async Task MercaderiaIsValid_AllowNullIsFalseWithImageNull_ShouldReturnFalse(String? nombre, 
                                                                                            Double precio, 
                                                                                            String? ingredientes, 
                                                                                            String? preparacion)
        {
            // Arrange
            MercaderiaRequest mercaderiaRequest = new MercaderiaRequest()
            {
                Nombre = nombre,
                Precio = precio,
                Ingredientes = ingredientes,
                Preparacion = preparacion
            };

            Boolean allowNull = false;

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var service = new ServiceValidateMercaderia(mockHttpClientFactory.Object);

            // Act
            var result = await service.MercaderiaIsValid(mercaderiaRequest, allowNull);
            var error = service.GetError();

            // Assert
            result.Should().BeFalse();
            error.Should().NotBeNullOrEmpty();
        }
    

        [Theory]
        [InlineData("nicolas", 70, "miau", "hacer asi", "image/jpg")]
        [InlineData("Aa", 1060, "miaAu", "hacer asi1", "image/webp")]
        [InlineData("a1232", 13000, "miau", "hacer asi2", "image/avif")]
        [InlineData("a12321", 10300, "miau", "hacer asi3", "image/png")]
        [InlineData("a1232", 120, "miau", "hacer asi4", "image/jpeg")]
        public async Task MercaderiaIsValid_AllowNullIsFalse_ShouldReturnTrue(String nombre, 
                                                                              Double precio, 
                                                                              String ingredientes, 
                                                                              String preparacion, 
                                                                              String mediaType)
        {
            // Arrange
            MercaderiaRequest mercaderiaRequest = new MercaderiaRequest()
            {
                Nombre = nombre,
                Precio = precio,
                Ingredientes = ingredientes,
                Preparacion = preparacion,
                Imagen = "http://example.com",
            };
            Boolean allowNull = false;

            var _httpMessageHandler = new Mock<HttpMessageHandler>();
            _httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(string.Empty)
                    {
                        Headers = { ContentType = new MediaTypeHeaderValue(mediaType) }
                    }
                });

            var httpClient = new HttpClient(_httpMessageHandler.Object);

            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var service = new ServiceValidateMercaderia(mockFactory.Object);

            // Act
            var result = await service.MercaderiaIsValid(mercaderiaRequest, allowNull);
            var error = service.GetError();

            // Assert
            result.Should().BeTrue();
            error.Should().BeNullOrEmpty();
        }
    }
}
