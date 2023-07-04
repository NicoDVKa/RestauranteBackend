using Domain.Entities;

namespace Application.Interfaces
{
    public interface IQueryComanda
    {
        public Task<IList<Comanda>> GetAll();

        public Task<IList<Comanda>> GetComandaByDate(DateTime date);

        public Task<Comanda> GetComandaById(Guid id);
    }
}
