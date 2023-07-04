
using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Queries
{
    public class QueryTipoMercaderia : IQueryTipoMercaderia
    {
        private readonly AppDbContext _context;

        public QueryTipoMercaderia(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TipoMercaderia> GetTipoMercaderiaById(int id)
        {
            TipoMercaderia tipoMercaderia = await _context.TipoMercaderia.SingleOrDefaultAsync(tm => tm.TipoMercaderiaId == id);

            return tipoMercaderia;
        }
    }
}
