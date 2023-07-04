using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Queries
{
    public class QueryComanda : IQueryComanda
    {
        private readonly AppDbContext _context;

        public QueryComanda(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Comanda>> GetAll()
        {
            IList<Comanda> comands = await _context.Comanda.Include(c => c.ComandaMercaderias)
                                                           .ThenInclude(c => c.Mercaderia)
                                                           .ThenInclude(c => c.TipoMercaderia)
                                                           .Include(c => c.FormaEntrega)
                                                           .ToListAsync();
            return comands;
        }

        public async Task<IList<Comanda>> GetComandaByDate(DateTime date)
        {
            IList<Comanda> comandas = await _context.Comanda
                                                    .Include(c => c.ComandaMercaderias)
                                                    .ThenInclude(c => c.Mercaderia)
                                                    .ThenInclude(c => c.TipoMercaderia)
                                                    .Include(c => c.FormaEntrega)
                                                    .Where(c => c.Fecha.Year == date.Year && c.Fecha.Month == date.Month && c.Fecha.Day == date.Day)
                                                    .ToListAsync();
            return comandas;
        }

        public async Task<Comanda> GetComandaById(Guid id)
        {
           Comanda comanda = await _context.Comanda.Include(c => c.ComandaMercaderias)
                                                   .ThenInclude(c => c.Mercaderia)
                                                   .ThenInclude(c => c.TipoMercaderia)
                                                   .Include(c => c.FormaEntrega)
                                                   .SingleOrDefaultAsync(c => c.ComandaId == id);
            return comanda;
        }


    }
}
