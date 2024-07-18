
using Domain.Entities;
using FluentAssertions;
using Infraestructure.Persistence;
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
            
            context.Database.EnsureDeleted();

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
            context.SaveChanges();

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

            // Act
            IList<Comanda> comands = await context.Comanda.Include(c => c.ComandaMercaderias)
                                                          .ThenInclude(c => c.Mercaderia)
                                                          .ThenInclude(c => c.TipoMercaderia)
                                                          .Include(c => c.FormaEntrega)
                                                          .ToListAsync();
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

            context.Database.EnsureDeleted();

            // Act
            IList<Comanda> comands = await context.Comanda.Include(c => c.ComandaMercaderias)
                                                          .ThenInclude(c => c.Mercaderia)
                                                          .ThenInclude(c => c.TipoMercaderia)
                                                          .Include(c => c.FormaEntrega)
                                                          .ToListAsync();
            // Assert
            comands.Should().BeEmpty();
        }


    }
}
