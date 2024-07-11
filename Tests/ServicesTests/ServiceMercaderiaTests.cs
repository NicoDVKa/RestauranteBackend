
using Application.Interfaces;
using Application.Models.Response;
using Application.UseCases;
using Domain.Entities;
using FluentAssertions;
using Moq;
using System.Collections;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            Assert.Equivalent(expectedMercaderiaResponse, result);     
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
            Assert.Equivalent(expectedMercaderiaResponse, result);
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
            Assert.Null(result);
        }
    }
}
