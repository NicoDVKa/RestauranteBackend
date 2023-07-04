using Application.Models.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IQueryMercaderia
    {
        public Task<IList<Mercaderia>> GetAllMercaderias();
        public Task<Mercaderia> GetMercaderiaByName(string name);
        public Task<Mercaderia> GetMercaderiaById(int id);
        public Task<IList<Mercaderia>> SearchMercaderia(int? tipo, string? name, string orden);
    }
}
