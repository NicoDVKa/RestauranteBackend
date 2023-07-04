

using Domain.Entities;

namespace Application.Interfaces
{
    public interface IQueryComandaMercaderia
    {
        Task<IList<ComandaMercaderia>> GetComandaMercaderiaByMercaderiaId(int mercaderiaId);
    }
}
