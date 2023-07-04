using Application.Models.Request;
using Application.Models.Response;

namespace Application.Interfaces
{
    public interface IServiceMercaderia
    {
        Task<IList<MercaderiaResponse>> GetAllMercaderia();
        Task<MercaderiaResponse> CreateMercaderia(MercaderiaRequest request);
        Task<MercaderiaResponse> GetMercaderiaById(int id);
        Task<MercaderiaResponse> GetMercaderiaByName(string name);
        Task<MercaderiaResponse> UpdateMercaderia(int id, MercaderiaRequest request);
        Task<MercaderiaResponse> DeleteMercaderia(int id);
        Task<IList<MercaderiaGetResponse>> SearchMercaderia(int? tipo, string? name, string orden);
    }
}
