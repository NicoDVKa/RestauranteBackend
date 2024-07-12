

using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Application.UseCases;
using Domain.Entities;
using FluentAssertions;
using Infraestructure.Commands;
using Moq;

namespace Tests.ServicesTests
{
    public class ServiceComandaTests
    {
        [Fact]
        public async Task CreateComanda_ShouldBeReturnComandaResponse()
        {
            // Arrange
            var mockCommandComanda = new Mock<ICommandComanda>();
            var mockQueryComanda = new Mock<IQueryComanda>();   
            var mockCommandComandaMercaderia =  new Mock<ICommandComandaMercaderia>();
            var mockQueryComandaMercaderia = new Mock<IQueryComandaMercaderia>();

            var precioTotal = 300.50;
            var comandaRequest = new ComandaRequest
            {
                Mercaderias = [1,2,2],
                FormaEntrega = 1,
            };
            var comandaCreated = new Comanda
            {
                ComandaId = Guid.NewGuid(),
                PrecioTotal = (int)precioTotal,
                Fecha = DateTime.Now,
                FormaEntregaId = comandaRequest.FormaEntrega
            };
            var comandaByQuery = comandaCreated;
            comandaByQuery.ComandaMercaderias = new List<ComandaMercaderia>
            {
                new ComandaMercaderia
                {
                    ComandaMercaderiaId = 1,
                    MercaderiaId = 1,
                    ComandaId = comandaByQuery.ComandaId,
                    Mercaderia = new Mercaderia
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

                    }
                },
                 new ComandaMercaderia
                {
                    ComandaMercaderiaId = 2,
                    MercaderiaId = 2,
                    ComandaId = comandaByQuery.ComandaId,
                    Mercaderia = new Mercaderia
                    {
                        MercaderiaId = 2,
                        Nombre = "Producto 2",
                        Imagen = "x",
                        Preparacion = "x",
                        Precio = 100,
                        Ingredientes = "x",
                        TipoMercaderia = new TipoMercaderia
                        {
                            TipoMercaderiaId = 2,
                            Descripcion = "2",
                        }

                    }
                },
                  new ComandaMercaderia
                {
                    ComandaMercaderiaId = 3,
                    MercaderiaId = 2,
                    ComandaId = comandaByQuery.ComandaId,
                    Mercaderia = new Mercaderia
                    {
                        MercaderiaId = 2,
                        Nombre = "Producto 2",
                        Imagen = "x",
                        Preparacion = "x",
                        Precio = 100,
                        Ingredientes = "x",
                        TipoMercaderia = new TipoMercaderia
                        {
                            TipoMercaderiaId = 2,
                            Descripcion = "2",
                        }

                    }
                },
            };
            comandaByQuery.FormaEntrega = new Domain.Entities.FormaEntrega
            {
                FormaEntregaId = 1,
                Descripcion = "x"
            };
            IList<MercaderiaComandaResponse> expectedMercaderiasResponse = new List<MercaderiaComandaResponse>
            {
                new MercaderiaComandaResponse
                {
                    Id = 1,
                    Nombre = "Producto 1",
                    Precio = 100
                },
                new MercaderiaComandaResponse
                {
                    Id = 2,
                    Nombre = "Producto 2",
                    Precio = 100
                },
                new MercaderiaComandaResponse
                {
                    Id = 3,
                    Nombre = "Producto 2",
                    Precio = 100
                }
            };
            ComandaResponse expectedComandaResponse = new ComandaResponse
            {
                Id = comandaCreated.ComandaId,
                Total = (int)precioTotal,
                Fecha = comandaCreated.Fecha,
                FormaEntrega = new Application.Models.Response.FormaEntrega
                {
                    Id = 1,
                    Descripcion = "x"
                },
                Mercaderias = expectedMercaderiasResponse
            };

            mockCommandComanda.Setup(c => c.CreateComanda(It.IsAny<Comanda>()))
                              .ReturnsAsync(comandaCreated);
            mockQueryComanda.Setup(c => c.GetComandaById(It.IsAny<Guid>())).ReturnsAsync(comandaByQuery);
            var service = new ServiceComanda(mockQueryComanda.Object, 
                                             mockCommandComanda.Object,
                                             mockCommandComandaMercaderia.Object,
                                             mockQueryComandaMercaderia.Object
            );

            // Act 
            var result = await service.CreateComanda(comandaRequest, precioTotal);
      
            // Assert
            result.Should().BeEquivalentTo(expectedComandaResponse);
        }

    }
}
