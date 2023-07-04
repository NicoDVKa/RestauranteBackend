
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IQueryTipoMercaderia
    {
        Task<TipoMercaderia> GetTipoMercaderiaById(int id);
    }
}
