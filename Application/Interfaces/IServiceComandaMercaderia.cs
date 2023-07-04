
using Application.Models.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IServiceComandaMercaderia
    {
          Task<int> CreateComandaMercaderia(Guid comandaId, int mercaderiaId);

         Task<IList<MercaderiaComandaResponse>> GetComandaMercaderiaByMercaderiaId(int mercaderiaId);
    }
}
