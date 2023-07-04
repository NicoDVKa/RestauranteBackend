using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Queries
{
    public class QueryFormaEntrega : IQueryFormaEntrega
    {
        private readonly AppDbContext _context;

        public QueryFormaEntrega(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<FormaEntrega>> GetAllFormaEntrega()
        {
            IList<FormaEntrega> formaEntregas = await _context.FormaEntrega.ToListAsync();

            return formaEntregas;
        }

        public async Task<FormaEntrega> GetFormaEntregaById(int id)
        {
            FormaEntrega formaEntrega = await _context.FormaEntrega.SingleOrDefaultAsync(f => f.FormaEntregaId == id);

            return formaEntrega;
        }
    }
}
