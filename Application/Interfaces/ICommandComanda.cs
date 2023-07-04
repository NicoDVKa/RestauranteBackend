using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICommandComanda
    {
        public Task<Comanda> CreateComanda(Comanda comanda);
    }
}
