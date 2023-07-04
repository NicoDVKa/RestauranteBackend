using Application.Models.Response;

namespace Application.Interfaces
{
    public interface IServiceFormaEntrega
    {
        Task<IList<FormaEntrega>> GetAllFormaEntrega();

        Task<FormaEntrega> GetFormaEntregaById(int id);
    }
}
