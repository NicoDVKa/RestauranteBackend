using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Queries
{
    public class QueryMercaderia : IQueryMercaderia
    {
        private readonly AppDbContext _context;

        public QueryMercaderia(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Mercaderia>> GetAllMercaderias()
        {
            IList<Mercaderia> mercaderias = await _context.Mercaderia
                                                          .Include(m => m.TipoMercaderia)
                                                          .ToListAsync<Mercaderia>();

            return mercaderias;
        }

        public async Task<Mercaderia> GetMercaderiaById(int id)
        {
            Mercaderia response = await _context.Mercaderia
                                                .Include(m => m.TipoMercaderia)
                                                .SingleOrDefaultAsync(m => m.MercaderiaId == id);
            return response;
        }

        public async Task<Mercaderia> GetMercaderiaByName(string name)
        {
            Mercaderia response = await _context.Mercaderia
                                                .Include(m => m.TipoMercaderia)
                                                .SingleOrDefaultAsync(m => m.Nombre == name.Trim().ToLower());
            return response;
        }

        public async Task<IList<Mercaderia>> SearchMercaderia(int? tipo, string? name, string orden)
        {
            var query = _context.Mercaderia
                                .Include(m => m.TipoMercaderia)
                                .Where(m => m.Precio > 0);
            if (name != null)
            {
                query = query.Where(m => m.Nombre.StartsWith(name));
            }

            if (tipo != null)
            {
                query = query.Where(m => m.TipoMercaderiaId == tipo);
            }

            if (orden == "asc")
            {
                query = query.OrderBy(m => m.Precio);
            }
            else
            {
                query = query.OrderByDescending(m => m.Precio);
            }

            var response = await query.ToListAsync();

            return response;
        }
    }
}
