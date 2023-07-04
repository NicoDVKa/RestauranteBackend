
using Application.Interfaces;
using Infraestructure.Persistence;

namespace Infraestructure.Commands
{
    public class CommandTipoMercaderia : ICommandTipoMercaderia
    {
        private readonly AppDbContext _context;

        public CommandTipoMercaderia(AppDbContext context)
        {
            _context = context;
        }
    }
}
