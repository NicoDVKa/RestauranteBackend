using Application.Models.Request;
using Application.Models.Response;

namespace Application.Interfaces
{
    public interface IServiceComanda
    {
        public Task<ComandaResponse> CreateComanda(ComandaRequest request, double total);

        public Task<IList<ComandaResponse>> GetAll();

        public Task<IList<ComandaResponse>> GetComandaByDate(DateTime date);

        public Task<ComandaGetResponse> GetComandaById(Guid id);
    }
}
