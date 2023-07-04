using Domain.Entities;

namespace Application.Interfaces
{
    public interface IQueryFormaEntrega
    {
        public Task<IList<FormaEntrega>> GetAllFormaEntrega();

        public Task<FormaEntrega> GetFormaEntregaById(int id);
    }
}
