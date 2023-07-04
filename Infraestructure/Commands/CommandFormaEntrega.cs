using Application.Interfaces;
using Infraestructure.Persistence;

namespace Infraestructure.Commands
{
    public class CommandFormaEntrega : ICommandFormaEntrega
    {
        private readonly AppDbContext _context;

        public CommandFormaEntrega(AppDbContext context)
        {
            _context = context;
        }
    }
}
