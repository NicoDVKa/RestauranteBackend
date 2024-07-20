

using Domain.Entities;
using FluentAssertions;
using Infraestructure.Persistence;
using Infraestructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace Tests.QueriesTests
{
    public class QueryMercaderiaTests
    {
        [Fact]
        public async Task GetAllMercaderias_ShouldReturnEmptyList()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "MercaderiasTests")
                .Options;
            using var context = new AppDbContext(options);
            var query = new QueryMercaderia(context);

            // Act
            IList<Mercaderia> mercaderias = await query.GetAllMercaderias();
            await context.Database.EnsureDeletedAsync();
            // Assert
            mercaderias.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllMercaderias_ShouldReturnListOfMercaderia()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "MercaderiasTests")
                .Options;
            using var context = new AppDbContext(options);

            var tipoMercaderias = new List<TipoMercaderia>()
            {
                new TipoMercaderia()
                {
                    Descripcion = "Entrada"
                },
                new TipoMercaderia()
                {
                    Descripcion = "Minuta"
                },
                new TipoMercaderia()
                {
                    Descripcion = "Bebida"
                },
            };
            var mercaderias = new List<Mercaderia>()
            {
                new Mercaderia()
                {
                    Nombre = "Deditos de queso",
                    Imagen = "a",
                    Precio = 100,
                    Ingredientes = "a",
                    Preparacion = "a",
                    TipoMercaderiaId = 1
                },
                new Mercaderia()
                {
                    Nombre = "aa",
                    Imagen = "b",
                    Precio = 110,
                    Ingredientes = "b",
                    Preparacion = "b",
                    TipoMercaderiaId = 2
                },
                new Mercaderia()
                {
                    Nombre = "bb",
                    Imagen = "c",
                    Precio = 111,
                    Ingredientes = "c",
                    Preparacion = "c",
                    TipoMercaderiaId = 3
                },
            };
            context.TipoMercaderia.AddRange(tipoMercaderias);
            context.Mercaderia.AddRange(mercaderias);
            await context.SaveChangesAsync();
            var expectedResponse = new List<Mercaderia>(mercaderias);
            
            var query = new QueryMercaderia(context);

            // Act
            IList<Mercaderia> mercaderiasGetByQuery = await query.GetAllMercaderias();
            await context.Database.EnsureDeletedAsync();

            // Assert
            mercaderias.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
