using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Commands
{
    public class CommandComandaMercaderia: ICommandComandaMercaderia
    {
        private readonly AppDbContext _context;

        public CommandComandaMercaderia(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateComandaMercaderia(ComandaMercaderia comandaMercaderia)
        {
            await _context.ComandaMercaderia.AddAsync(comandaMercaderia);

            await _context.SaveChangesAsync();
            
            return comandaMercaderia.ComandaMercaderiaId;
        }
    }
}
