

using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Application.UseCases;
using Domain.Entities;
using FluentAssertions;
using Infraestructure.Commands;
using Infraestructure.Queries;
using Moq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        [Fact]
        public async Task GetAll_ShouldBeReturnEmptyList()
        {
            // Arrange
            var mockCommandComanda = new Mock<ICommandComanda>();
            var mockQueryComanda = new Mock<IQueryComanda>();
            var mockCommandComandaMercaderia = new Mock<ICommandComandaMercaderia>();
            var mockQueryComandaMercaderia = new Mock<IQueryComandaMercaderia>();

            IList<Comanda> comandas = new List<Comanda>();
            mockQueryComanda.Setup(q =>  q.GetAll()).ReturnsAsync(comandas);

            var service = new ServiceComanda(mockQueryComanda.Object,
                                            mockCommandComanda.Object,
                                            mockCommandComandaMercaderia.Object,
                                            mockQueryComandaMercaderia.Object
            );
            // Act
            var result = await service.GetAll();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_ShouldBeReturnListOfComandaResponses()
        {
            // Arrange
            var mockCommandComanda = new Mock<ICommandComanda>();
            var mockQueryComanda = new Mock<IQueryComanda>();
            var mockCommandComandaMercaderia = new Mock<ICommandComandaMercaderia>();
            var mockQueryComandaMercaderia = new Mock<IQueryComandaMercaderia>();

            Comanda[] comandasVector = new Comanda[]
            {
                new Comanda()
                {
                    ComandaId = Guid.NewGuid(),
                    PrecioTotal = (int)100.45,
                    Fecha = DateTime.Now,
                    FormaEntregaId = 3,
                    FormaEntrega = new Domain.Entities.FormaEntrega
                    {
                         FormaEntregaId = 3,
                         Descripcion = "xaa"
                    },
                    ComandaMercaderias = new List<ComandaMercaderia>
                    {
                        new ComandaMercaderia
                        {
                            ComandaMercaderiaId = 1,
                            MercaderiaId = 2,
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
                                    TipoMercaderiaId = 2,
                                    Descripcion = "2",
                                }

                            }
                        },
                        new ComandaMercaderia
                        {
                            ComandaMercaderiaId = 2,
                            MercaderiaId = 2,
                          
                            Mercaderia = new Mercaderia
                            {
                                MercaderiaId = 2,
                                Nombre = "Producto 2",
                                Imagen = "x",
                                Preparacion = "x",
                                Precio = 500,
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
                          
                            Mercaderia = new Mercaderia
                            {
                                MercaderiaId = 2,
                                Nombre = "Producto 2",
                                Imagen = "x",
                                Preparacion = "x",
                                Precio = 500,
                                Ingredientes = "x",
                                TipoMercaderia = new TipoMercaderia
                                {
                                    TipoMercaderiaId = 2,
                                    Descripcion = "2",
                                }

                            }
                        },
                    }
                },
                new Comanda()
                {
                    ComandaId = Guid.NewGuid(),
                    PrecioTotal = (int)1200.45,
                    Fecha = DateTime.Now,
                    FormaEntregaId = 2,
                    FormaEntrega = new Domain.Entities.FormaEntrega
                    {
                         FormaEntregaId = 2,
                         Descripcion = "xa"
                    },
                    ComandaMercaderias = new List<ComandaMercaderia>
                    {
                        new ComandaMercaderia
                        {
                            ComandaMercaderiaId = 4,
                            MercaderiaId = 2,

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
                    }
                },
                new Comanda()
                {
                    ComandaId = Guid.NewGuid(),
                    PrecioTotal = (int)1020.45,
                    Fecha = DateTime.Now,
                    FormaEntregaId = 2,
                    FormaEntrega = new Domain.Entities.FormaEntrega
                    {
                         FormaEntregaId = 2,
                         Descripcion = "xa"
                    },
                    ComandaMercaderias = new List<ComandaMercaderia>
                    {
                        new ComandaMercaderia
                        {
                            ComandaMercaderiaId = 5,
                            MercaderiaId = 3,
                           
                            Mercaderia = new Mercaderia
                            {
                                MercaderiaId = 3,
                                Nombre = "Producto 3",
                                Imagen = "x",
                                Preparacion = "x",
                                Precio = 1100,
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
                            ComandaMercaderiaId = 6,
                            MercaderiaId = 2,
                          
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
                    }
                },
            };

            IList<Comanda> comandas = new List<Comanda>(comandasVector);

            IList<ComandaResponse> expectedComandaResponses = new List<ComandaResponse>
            {
                new ComandaResponse
                {
                    Id = comandasVector[0].ComandaId,
                    Total = (int)100.45,
                    Fecha = comandasVector[0].Fecha,
                    FormaEntrega = new Application.Models.Response.FormaEntrega()
                    {
                        Id = 3,
                        Descripcion = "xaa",
                    },
                    Mercaderias = new List<MercaderiaComandaResponse>
                    {
                        new MercaderiaComandaResponse
                        {
                            Id = 1,
                            Nombre = "Producto 1",
                            Precio = 100,
                        },
                        new MercaderiaComandaResponse
                        {
                            Id = 2,
                            Nombre = "Producto 2",
                            Precio = 500,
                        },
                        new MercaderiaComandaResponse
                        {
                            Id = 3,
                            Nombre = "Producto 2",
                            Precio = 500,
                        },
                    }
                }, 
                new ComandaResponse
                {
                    Id =comandasVector[1].ComandaId,
                    Total = (int)1200.45,
                    Fecha = comandasVector[1].Fecha,
                    FormaEntrega = new Application.Models.Response.FormaEntrega()
                    {
                        Id = 2,
                        Descripcion = "xa",
                    },
                    Mercaderias = new List<MercaderiaComandaResponse>
                    {
                        new MercaderiaComandaResponse
                        {
                            Id = 4,
                            Nombre = "Producto 2",
                            Precio = 100,
                        }
                    }
                },
                new ComandaResponse
                {
                    Id = comandasVector[2].ComandaId,
                    Total = (int)1020.45,
                    Fecha = comandasVector[2].Fecha,
                    FormaEntrega = new Application.Models.Response.FormaEntrega()
                    {
                        Id = 2,
                        Descripcion = "xa",
                    },
                    Mercaderias = new List<MercaderiaComandaResponse>
                    {
                        new MercaderiaComandaResponse
                        {
                            Id = 5,
                            Nombre = "Producto 3",
                            Precio = 1100,
                        },
                        new MercaderiaComandaResponse
                        {
                            Id = 6,
                            Nombre = "Producto 2",
                            Precio = 100,
                        }
                    }
                }, 

            };

            mockQueryComanda.Setup(q => q.GetAll()).ReturnsAsync(comandas);

            var service = new ServiceComanda(mockQueryComanda.Object,
                                            mockCommandComanda.Object,
                                            mockCommandComandaMercaderia.Object,
                                            mockQueryComandaMercaderia.Object
            );
            // Act
            var result = await service.GetAll();

            // Assert
            result.Should().BeEquivalentTo(expectedComandaResponses);
        }
    }
}
