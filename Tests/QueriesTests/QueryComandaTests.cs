
using Domain.Entities;
using FluentAssertions;
using Infraestructure.Persistence;
using Infraestructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace Tests.QueriesTests
{
    public class QueryComandaTests
    {
        [Fact]
        public async Task GetAll_ShouldReturnListOfComanda()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
            using var context = new AppDbContext(options);
            
            var tipoMercaderia = new TipoMercaderia 
            { 
                Descripcion = "Entrada" 
            };
            var mercaderia = new Mercaderia 
            { 
                Nombre = "Deditos de queso",
                Imagen = "a",
                Precio = 100,
                Ingredientes = "a",
                Preparacion = "a",
                TipoMercaderiaId = 1 
            };
            var formaEntrega = new FormaEntrega 
            { 
                Descripcion = "Salon" 
            };

            context.TipoMercaderia.Add(tipoMercaderia);
            context.Mercaderia.Add(mercaderia);
            context.FormaEntrega.Add(formaEntrega);

            var comanda1 = new Comanda
            {
                ComandaId = Guid.NewGuid(),
                PrecioTotal = 100,
                Fecha = DateTime.Now,
                FormaEntregaId = formaEntrega.FormaEntregaId,
                ComandaMercaderias = new List<ComandaMercaderia>
                {
                    new ComandaMercaderia { MercaderiaId = mercaderia.MercaderiaId }
                }
            };
            var comanda2 = new Comanda
            {
                ComandaId = Guid.NewGuid(),
                PrecioTotal = 200,
                Fecha = DateTime.Now.AddDays(1),
                FormaEntregaId = formaEntrega.FormaEntregaId,
                ComandaMercaderias = new List<ComandaMercaderia>
                {
                    new ComandaMercaderia { MercaderiaId = mercaderia.MercaderiaId }
                }
            };

            context.Comanda.AddRange(comanda1, comanda2);
            context.SaveChanges();

            var expectedResponse = new List<Comanda>()
            {
                comanda1, comanda2
            };

            var queryComanda = new QueryComanda(context);

            // Act
            IList<Comanda> comands = await queryComanda.GetAll();
            context.Database.EnsureDeleted();

            // Assert
            comands.Should().BeEquivalentTo(expectedResponse);

        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyList()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
            using var context = new AppDbContext(options);

            var queryComanda = new QueryComanda(context);

            // Act
            IList<Comanda> comands = await queryComanda.GetAll();

            // Assert
            comands.Should().BeEmpty();
        }

        [Fact]
        public async Task GetComandaById_ShouldReturnComanda()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
            using var context = new AppDbContext(options);

            var tipoMercaderia = new TipoMercaderia
            {
                Descripcion = "Entrada"
            };
            var mercaderia = new Mercaderia
            {
                Nombre = "Deditos de queso",
                Imagen = "a",
                Precio = 100,
                Ingredientes = "a",
                Preparacion = "a",
                TipoMercaderiaId = 1
            };
            var formaEntrega = new FormaEntrega
            {
                Descripcion = "Salon"
            };

            context.TipoMercaderia.Add(tipoMercaderia);
            context.Mercaderia.Add(mercaderia);
            context.FormaEntrega.Add(formaEntrega);

            var comanda1 = new Comanda
            {
                ComandaId = Guid.NewGuid(),
                PrecioTotal = 100,
                Fecha = DateTime.Now,
                FormaEntregaId = formaEntrega.FormaEntregaId,
                ComandaMercaderias = new List<ComandaMercaderia>
                {
                    new ComandaMercaderia { MercaderiaId = mercaderia.MercaderiaId }
                }
            };
            var comanda2 = new Comanda
            {
                ComandaId = Guid.NewGuid(),
                PrecioTotal = 200,
                Fecha = DateTime.Now.AddDays(1),
                FormaEntregaId = formaEntrega.FormaEntregaId,
                ComandaMercaderias = new List<ComandaMercaderia>
                {
                    new ComandaMercaderia { MercaderiaId = mercaderia.MercaderiaId }
                }
            };

            context.Comanda.AddRange(comanda1, comanda2);
            context.SaveChanges();

            var expectedResponse = comanda2;

            var queryComanda = new QueryComanda(context);

            // Act
            Comanda comanda = await queryComanda.GetComandaById(expectedResponse.ComandaId);
            context.Database.EnsureDeleted();

            // Assert
            comanda.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task GetComandaById_ComandaIdDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
            using var context = new AppDbContext(options);

            var tipoMercaderia = new TipoMercaderia
            {
                Descripcion = "Entrada"
            };
            var mercaderia = new Mercaderia
            {
                Nombre = "Deditos de queso",
                Imagen = "a",
                Precio = 100,
                Ingredientes = "a",
                Preparacion = "a",
                TipoMercaderiaId = 1
            };
            var formaEntrega = new FormaEntrega
            {
                Descripcion = "Salon"
            };

            context.TipoMercaderia.Add(tipoMercaderia);
            context.Mercaderia.Add(mercaderia);
            context.FormaEntrega.Add(formaEntrega);


            var comanda1 = new Comanda
            {
                ComandaId = Guid.NewGuid(),
                PrecioTotal = 100,
                Fecha = DateTime.Now,
                FormaEntregaId = formaEntrega.FormaEntregaId,
                ComandaMercaderias = new List<ComandaMercaderia>
                {
                    new ComandaMercaderia { MercaderiaId = mercaderia.MercaderiaId }
                }
            };
            var comanda2 = new Comanda
            {
                ComandaId = Guid.NewGuid(),
                PrecioTotal = 200,
                Fecha = DateTime.Now.AddDays(1),
                FormaEntregaId = formaEntrega.FormaEntregaId,
                ComandaMercaderias = new List<ComandaMercaderia>
                {
                    new ComandaMercaderia { MercaderiaId = mercaderia.MercaderiaId }
                }
            };

            context.Comanda.AddRange(comanda1, comanda2);
            context.SaveChanges();

            var expectedResponse = comanda2;
            var queryComanda = new QueryComanda(context);
            
            // Act
            Comanda comanda = await queryComanda.GetComandaById(Guid.NewGuid());
            context.Database.EnsureDeleted();
            // Assert
            comanda.Should().BeNull();
        }

        [Fact]
        public async Task GetComandaByDate_ShouldReturnListOfComanda()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
            using var context = new AppDbContext(options);

            var tipoMercaderia = new TipoMercaderia
            {
                Descripcion = "Entrada"
            };
            var mercaderia = new Mercaderia
            {
                Nombre = "Deditos de queso",
                Imagen = "a",
                Precio = 100,
                Ingredientes = "a",
                Preparacion = "a",
                TipoMercaderiaId = 1
            };
            var formaEntrega = new FormaEntrega
            {
                Descripcion = "Salon"
            };

            context.TipoMercaderia.Add(tipoMercaderia);
            context.Mercaderia.Add(mercaderia);
            context.FormaEntrega.Add(formaEntrega);

            var comanda1 = new Comanda
            {
                ComandaId = Guid.NewGuid(),
                PrecioTotal = 100,
                Fecha = DateTime.Now,
                FormaEntregaId = formaEntrega.FormaEntregaId,
                ComandaMercaderias = new List<ComandaMercaderia>
                {
                    new ComandaMercaderia { MercaderiaId = mercaderia.MercaderiaId }
                }
            };
            var comanda2 = new Comanda
            {
                ComandaId = Guid.NewGuid(),
                PrecioTotal = 200,
                Fecha = DateTime.Now.AddDays(1),
                FormaEntregaId = formaEntrega.FormaEntregaId,
                ComandaMercaderias = new List<ComandaMercaderia>
                {
                    new ComandaMercaderia { MercaderiaId = mercaderia.MercaderiaId }
                }
            };
            var comanda3 = new Comanda
            {
                ComandaId = Guid.NewGuid(),
                PrecioTotal = 200,
                Fecha = DateTime.Now.AddDays(1),
                FormaEntregaId = formaEntrega.FormaEntregaId,
                ComandaMercaderias = new List<ComandaMercaderia>
                {
                    new ComandaMercaderia { MercaderiaId = mercaderia.MercaderiaId }
                }
            };

            var date = DateTime.Now.AddDays(1);

            context.Comanda.AddRange(comanda1, comanda2, comanda3);

            context.SaveChanges();

            var expectedResponse = new List<Comanda>()
            {
                comanda2, comanda3
            };

            var queryComanda = new QueryComanda(context);

            // Act
            IList<Comanda> comandas = await queryComanda.GetComandaByDate(date);
            context.Database.EnsureDeleted();
            
            // Assert
            comandas.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task GetComandaByDate_ShouldReturnEmptyList()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;
            using var context = new AppDbContext(options);

            var tipoMercaderia = new TipoMercaderia
            {
                Descripcion = "Entrada"
            };
            var mercaderia = new Mercaderia
            {
                Nombre = "Deditos de queso",
                Imagen = "a",
                Precio = 100,
                Ingredientes = "a",
                Preparacion = "a",
                TipoMercaderiaId = 1
            };
            var formaEntrega = new FormaEntrega
            {
                Descripcion = "Salon"
            };

            context.TipoMercaderia.Add(tipoMercaderia);
            context.Mercaderia.Add(mercaderia);
            context.FormaEntrega.Add(formaEntrega);

            var comanda1 = new Comanda
            {
                ComandaId = Guid.NewGuid(),
                PrecioTotal = 100,
                Fecha = DateTime.Now,
                FormaEntregaId = formaEntrega.FormaEntregaId,
                ComandaMercaderias = new List<ComandaMercaderia>
                {
                    new ComandaMercaderia { MercaderiaId = mercaderia.MercaderiaId }
                }
            };
            var comanda2 = new Comanda
            {
                ComandaId = Guid.NewGuid(),
                PrecioTotal = 200,
                Fecha = DateTime.Now.AddDays(1),
                FormaEntregaId = formaEntrega.FormaEntregaId,
                ComandaMercaderias = new List<ComandaMercaderia>
                {
                    new ComandaMercaderia { MercaderiaId = mercaderia.MercaderiaId }
                }
            };
            var comanda3 = new Comanda
            {
                ComandaId = Guid.NewGuid(),
                PrecioTotal = 200,
                Fecha = DateTime.Now.AddDays(1),
                FormaEntregaId = formaEntrega.FormaEntregaId,
                ComandaMercaderias = new List<ComandaMercaderia>
                {
                    new ComandaMercaderia { MercaderiaId = mercaderia.MercaderiaId }
                }
            };

            var date = DateTime.Now.AddDays(10);

            context.Comanda.AddRange(comanda1, comanda2, comanda3);
            context.SaveChanges();

            var queryComanda = new QueryComanda(context);

            // Act
            IList<Comanda> comandas = await queryComanda.GetComandaByDate(date);
            context.Database.EnsureDeleted();

            // Assert
            comandas.Should().BeEmpty();
        }
    }
}
