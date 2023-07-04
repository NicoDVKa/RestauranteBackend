using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Commands
{
    public class CommandMercaderia : ICommandMercaderia
    {
        private readonly AppDbContext _context;

        public CommandMercaderia(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Mercaderia> CreateMercaderia(Mercaderia mercaderia)
        {
            await _context.Mercaderia.AddAsync(mercaderia);

            await _context.SaveChangesAsync();

            return mercaderia;
        }

        public async Task<Mercaderia> DeleteMercaderia(int id)
        {
            Mercaderia delete =  await _context.Mercaderia.SingleOrDefaultAsync(m => m.MercaderiaId == id);

            _context.Remove(delete);

            await _context.SaveChangesAsync();

            return delete;
        }

        public async Task<Mercaderia> UpdateMercaderia(int id, Mercaderia mercaderia)
        {
            Mercaderia update = await _context.Mercaderia.Include(m => m.TipoMercaderia)
                                                         .FirstOrDefaultAsync(m => m.MercaderiaId == id);

            update.Nombre = mercaderia.Nombre;
            update.Ingredientes = mercaderia.Ingredientes;
            update.Preparacion = mercaderia.Preparacion;
            update.Precio = mercaderia.Precio;
            update.Imagen = mercaderia.Imagen;
            update.TipoMercaderiaId = mercaderia.TipoMercaderiaId;

            await _context.SaveChangesAsync();

            return update;
        }
    }
}
