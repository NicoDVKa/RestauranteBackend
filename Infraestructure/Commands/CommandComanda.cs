using Application.Interfaces;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Commands
{
    public class CommandComanda : ICommandComanda
    {
        private readonly AppDbContext _context;

        public CommandComanda(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comanda> CreateComanda(Comanda comanda)
        {
            await _context.Comanda.AddAsync(comanda);

            await _context.SaveChangesAsync();

            return comanda;
        }
    }
}
