
using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Application.UseCases;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Tests.ServicesTests
{
    public class ServiceMercaderiaTests
    { 
        [Fact]
        public async Task GetAllMercaderia_ShouldReturnListOfMercaderiaResponse()
        {
            // Arrange
            var mockQuery = new Mock<IQueryMercaderia>();
            var mockCommand = new Mock<ICommandMercaderia>();

            IList<Mercaderia> listOfMercaderia = new List<Mercaderia>()
            {
                new Mercaderia { MercaderiaId = 1,
                                 Nombre = "Producto 1",
                                 Imagen = "x",
                                 Preparacion = "x",
                                 Precio = 100,
                                 Ingredientes = "x",
                                 TipoMercaderia = new TipoMercaderia
                                 {
                                     TipoMercaderiaId = 1,
                                     Descripcion = "1",
                                 }
                },
                new Mercaderia { MercaderiaId = 2,
                                 Nombre = "Producto 2",
                                 Imagen = "a",
                                 Preparacion = "a",
                                 Precio = 50,
                                 Ingredientes = "a",
                                 TipoMercaderia = new TipoMercaderia
                                 {
                                     TipoMercaderiaId = 2,
                                     Descripcion = "a",
                                 }
                }
            };

            IList<MercaderiaResponse> expectedMercaderiaResponse = new List<MercaderiaResponse>()
            {
                new MercaderiaResponse { Id = 1,
                                         Nombre = "Producto 1",
                                         Imagen = "x",
                                         Preparacion = "x",
                                         Precio = 100,
                                         Ingredientes = "x",
                                         Tipo = new TipoMercaderiaResponse
                                         {
                                             Id = 1,
                                             Descripcion = "1",
                                         }
                },
                new MercaderiaResponse { Id = 2,
                                         Nombre = "Producto 2",
                                         Imagen = "a",
                                         Preparacion = "a",
                                         Precio = 50,
                                         Ingredientes = "a",
                                         Tipo = new TipoMercaderiaResponse
                                         {
                                             Id = 2,
                                             Descripcion = "a",
                                         }
                }
            };

            mockQuery.Setup(q => q.GetAllMercaderias()).Returns(Task.FromResult(listOfMercaderia));

            var service = new ServiceMercaderia(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.GetAllMercaderia();

            // Assert 
            result.Should().BeEquivalentTo(expectedMercaderiaResponse);
        }

        [Fact]
        public async Task GetAllMercaderia_ShouldReturnEmptyList()
        {
            // Arrange
            var mockQuery = new Mock<IQueryMercaderia>();
            var mockCommand = new Mock<ICommandMercaderia>();

            IList<Mercaderia> mercaderia = new List<Mercaderia>();

            mockQuery.Setup(q => q.GetAllMercaderias()).Returns(Task.FromResult(mercaderia));

            var service = new ServiceMercaderia(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.GetAllMercaderia();

            // Assert 
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetMercaderiaById_ShouldReturnMercaderiaResponse()
        {
            // Arrange
            var mockQuery = new Mock<IQueryMercaderia>();
            var mockCommand = new Mock<ICommandMercaderia>();

            Mercaderia mercaderia = new Mercaderia
            {
                MercaderiaId = 1,
                Nombre = "Producto 1",
                Imagen = "x",
                Preparacion = "x",
                Precio = 100,
                Ingredientes = "x",
                TipoMercaderia = new TipoMercaderia
                {
                    TipoMercaderiaId = 1,
                    Descripcion = "1",
                }
            };
            
            MercaderiaResponse expectedMercaderiaResponse = new MercaderiaResponse
            {
                Id = 1,
                Nombre = "Producto 1",
                Imagen = "x",
                Preparacion = "x",
                Precio = 100,
                Ingredientes = "x",
                Tipo = new TipoMercaderiaResponse
                {
                    Id = 1,
                    Descripcion = "1",
                }     
            };

            mockQuery.Setup(q => q.GetMercaderiaById(It.IsAny<int>())).Returns(Task.FromResult(mercaderia));

            var service = new ServiceMercaderia(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.GetMercaderiaById(1);

            // Assert
            result.Should().BeEquivalentTo(expectedMercaderiaResponse);
        }

        [Fact]
        public async Task GetMercaderiaById_ShouldReturnNull()
        {
            // Arrange
            var mockQuery = new Mock<IQueryMercaderia>();
            var mockCommand = new Mock<ICommandMercaderia>();
                    
            var service = new ServiceMercaderia(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.GetMercaderiaById(1);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task SearchMercaderia_ShouldReturnAEmptyList()
        {
            // Arrange
            var mockQuery = new Mock<IQueryMercaderia>();
            var mockCommand = new Mock<ICommandMercaderia>();

            IList<Mercaderia> expectedMercaderia = new List<Mercaderia>();
            mockQuery.Setup(q => q.SearchMercaderia(It.IsAny<int?>(), It.IsAny<string?>(), It.IsAny<string>())).Returns(Task.FromResult(expectedMercaderia));

            var service = new ServiceMercaderia(mockQuery.Object, mockCommand.Object);
        
            // Act
            var result = await service.SearchMercaderia(tipo: null, name: null, orden: "asc");

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task SearchMercaderia_ShouldReturnAListOfMercaderiaGetResponse()
        {
            // Arrange
            var mockQuery = new Mock<IQueryMercaderia>();
            var mockCommand = new Mock<ICommandMercaderia>();

            IList<Mercaderia> listOfMercaderia = new List<Mercaderia>()
            {
                new Mercaderia { MercaderiaId = 1,
                                 Nombre = "Producto 1",
                                 Imagen = "x",
                                 Preparacion = "x",
                                 Precio = 100,
                                 Ingredientes = "x",
                                 TipoMercaderia = new TipoMercaderia
                                 {
                                     TipoMercaderiaId = 1,
                                     Descripcion = "1",
                                 }
                },
                new Mercaderia { MercaderiaId = 2,
                                 Nombre = "Producto 2",
                                 Imagen = "a",
                                 Preparacion = "a",
                                 Precio = 50,
                                 Ingredientes = "a",
                                 TipoMercaderia = new TipoMercaderia
                                 {
                                     TipoMercaderiaId = 2,
                                     Descripcion = "a",
                                 }
                }
            };

            IList<MercaderiaGetResponse> expectedMercaderiaResponse = new List<MercaderiaGetResponse>()
            {
               
                new MercaderiaGetResponse { Id = 1,
                                         Nombre = "Producto 1",
                                         Imagen = "x",
                                         Precio = 100,
                                         Tipo = new TipoMercaderiaResponse
                                         {
                                             Id = 1,
                                             Descripcion = "1",
                                         }
                },
                new MercaderiaGetResponse { Id = 2,
                                         Nombre = "Producto 2",
                                         Imagen = "a",
                                         Precio = 50,
                                         Tipo = new TipoMercaderiaResponse
                                         {
                                             Id = 2,
                                             Descripcion = "a",
                                         }
                },
            };

            mockQuery.Setup(q => q.SearchMercaderia(It.IsAny<int?>(), It.IsAny<string?>(), It.IsAny<string>())).Returns(Task.FromResult(listOfMercaderia));

            var service = new ServiceMercaderia(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.SearchMercaderia(tipo: null, name: null, orden: "asc");

            // Assert
            result.Should().BeEquivalentTo(expectedMercaderiaResponse);
        }

        [Fact]
        public async Task CreateMercaderia_ShouldReturnMercaderiaResponse()
        {
            // Arrange
            var mockQuery = new Mock<IQueryMercaderia>();
            var mockCommand = new Mock<ICommandMercaderia>();

            Mercaderia mercaderia = new Mercaderia()
            {
                MercaderiaId = 1,
                Nombre = "Producto 1",
                Imagen = "x",
                Preparacion = "x",
                Precio = 100,
                Ingredientes = "x",
                TipoMercaderia = new TipoMercaderia
                {
                    TipoMercaderiaId = 1,
                    Descripcion = "1",
                }
            };

            MercaderiaRequest mercaderiaRequest = new MercaderiaRequest
            {
                Nombre = "Producto 1",
                Tipo = 1,
                Precio = 100,
                Ingredientes = "x",
                Preparacion = "x",
                Imagen = "x"  
            };

            MercaderiaResponse expectedMercaderiaResponse = new MercaderiaResponse()
            {
                Id = 1,
                Nombre = "Producto 1",
                Imagen = "x",
                Preparacion = "x",
                Precio = 100,
                Ingredientes = "x",
                Tipo = new TipoMercaderiaResponse
                {
                    Id = 1,
                    Descripcion = "1",
                }
            };

            mockCommand.Setup(c => c.CreateMercaderia(It.IsAny<Mercaderia>())).Returns(Task.FromResult(mercaderia));

            var service = new ServiceMercaderia(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.CreateMercaderia(mercaderiaRequest);

            // Assert 
            result.Should().BeEquivalentTo(expectedMercaderiaResponse);
        }

        [Fact]
        public async Task DeleteMercaderia_ShouldReturnMercaderiaResponse()
        {
            // Arrange
            var mockQuery = new Mock<IQueryMercaderia>();
            var mockCommand = new Mock<ICommandMercaderia>();

            Mercaderia mercaderia = new Mercaderia()
            {
                MercaderiaId = 1,
                Nombre = "Producto 1",
                Imagen = "x",
                Preparacion = "x",
                Precio = 100,
                Ingredientes = "x",
                TipoMercaderia = new TipoMercaderia
                {
                    TipoMercaderiaId = 1,
                    Descripcion = "1",
                }
            };

            MercaderiaResponse expectedMercaderiaResponse = new MercaderiaResponse()
            {
                Id = 1,
                Nombre = "Producto 1",
                Imagen = "x",
                Preparacion = "x",
                Precio = 100,
                Ingredientes = "x",
                Tipo = new TipoMercaderiaResponse
                {
                    Id = 1,
                    Descripcion = "1",
                }
            };

            mockCommand.Setup(c => c.DeleteMercaderia(It.IsAny<int>())).Returns(Task.FromResult(mercaderia));

            var service = new ServiceMercaderia(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.DeleteMercaderia(1);

            // Assert 
            result.Should().BeEquivalentTo(expectedMercaderiaResponse);
        }

        [Fact]
        public async Task UpdateMercaderia_ShouldReturnMercaderiaResponse()
        {
            // Arrange
            var mockQuery = new Mock<IQueryMercaderia>();
            var mockCommand = new Mock<ICommandMercaderia>();

            Mercaderia mercaderiaByQuery = new Mercaderia()
            {
                MercaderiaId = 1,
                Nombre = "Producto 1",
                Imagen = "x",
                Preparacion = "x",
                Precio = 100,
                Ingredientes = "x",
                TipoMercaderia = new TipoMercaderia
                {
                    TipoMercaderiaId = 1,
                    Descripcion = "1",
                }
            };

            MercaderiaRequest mercaderiaRequest = new MercaderiaRequest()
            {
                Nombre = "Producto 2",
                Imagen = "a",
                Preparacion = "a",
                Precio = 1,
                Ingredientes = "a",
                Tipo = 5,
            };

            Mercaderia mercaderiaByCommand = new Mercaderia()
            {
                MercaderiaId = 1,
                Nombre = "Producto 2",
                Imagen = "a",
                Preparacion = "a",
                Precio = 1,
                Ingredientes = "a",
                TipoMercaderia = new TipoMercaderia
                {
                    TipoMercaderiaId = 5,
                    Descripcion = "aaa",
                }
            };

            MercaderiaResponse mercaderiaResponse = new MercaderiaResponse()
            {
                Id = 1,
                Nombre = "Producto 2",
                Imagen = "a",
                Preparacion = "a",
                Precio = 1,
                Ingredientes = "a",
                Tipo = new TipoMercaderiaResponse
                {
                    Id = 5,
                    Descripcion = "aaa",
                }
            };

            mockQuery.Setup(q => q.GetMercaderiaById(It.IsAny<int>())).Returns(Task.FromResult(mercaderiaByQuery));
            mockCommand.Setup(c => c.UpdateMercaderia(It.IsAny<int>(), It.IsAny<Mercaderia>())).Returns(Task.FromResult(mercaderiaByCommand));

            var service = new ServiceMercaderia(mockQuery.Object, mockCommand.Object);


            // Act
            var result = await service.UpdateMercaderia(1, mercaderiaRequest);


            // Assert
            result.Should().BeEquivalentTo(mercaderiaResponse);
        }

        [Fact]
        public async Task GetMercaderiaByName_ShouldReturnMercaderiaResponse()
        {
            // Arrange
            var mockQuery = new Mock<IQueryMercaderia>();
            var mockCommand = new Mock<ICommandMercaderia>();

            Mercaderia mercaderia = new Mercaderia()
            {
                MercaderiaId = 1,
                Nombre = "Producto 1",
                Imagen = "x",
                Preparacion = "x",
                Precio = 100,
                Ingredientes = "x",
                TipoMercaderia = new TipoMercaderia
                {
                    TipoMercaderiaId = 1,
                    Descripcion = "1",
                }
            };

            MercaderiaResponse expectedMercaderia = new MercaderiaResponse()
            {
                Id = 1,
                Nombre = "Producto 1",
                Imagen = "x",
                Preparacion = "x",
                Precio = 100,
                Ingredientes = "x",
                Tipo = new TipoMercaderiaResponse
                {
                    Id = 1,
                    Descripcion = "1",
                }
            };

            mockQuery.Setup(q => q.GetMercaderiaByName(It.IsAny<string>())).Returns(Task.FromResult(mercaderia));
            

            var service = new ServiceMercaderia(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.GetMercaderiaByName("a");

            // Assert
            result.Should().BeEquivalentTo(expectedMercaderia); 
        }

        [Fact]
        public async Task GetMercaderiaByName_ShouldReturnNull()
        {
            // Arrange
            var mockQuery = new Mock<IQueryMercaderia>();
            var mockCommand = new Mock<ICommandMercaderia>();

            var service = new ServiceMercaderia(mockQuery.Object, mockCommand.Object);

            // Act
            var result = await service.GetMercaderiaByName("aaa");

            // Assert
            result.Should().BeNull();   
        }
    }
}
