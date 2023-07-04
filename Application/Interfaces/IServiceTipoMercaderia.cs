using Application.Models.Response;

namespace Application.Interfaces
{
    public interface IServiceTipoMercaderia
    {
        Task<TipoMercaderiaResponse> GetTipoMercaderiaById(int id);
    }
}
