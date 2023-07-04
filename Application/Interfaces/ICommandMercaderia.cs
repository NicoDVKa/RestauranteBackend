
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICommandMercaderia
    {
        public Task<Mercaderia> CreateMercaderia(Mercaderia mercaderia);
        public Task<Mercaderia> UpdateMercaderia(int id, Mercaderia mercaderia);
        public Task<Mercaderia> DeleteMercaderia(int id);
    }
}
