
using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Queries
{
    public class QueryComandaMercaderia : IQueryComandaMercaderia
    {
        private readonly AppDbContext _context;

        public QueryComandaMercaderia(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ComandaMercaderia>> GetComandaMercaderiaByMercaderiaId(int mercaderiaId)
        {
            IList<ComandaMercaderia> comandaMercaderias = await _context.ComandaMercaderia
                                                                        .Include(cm => cm.Mercaderia)
                                                                        .Where(cm => cm.MercaderiaId == mercaderiaId)
                                                                        .ToListAsync();
            return comandaMercaderias;
        }
    }
}
