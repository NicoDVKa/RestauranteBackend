using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICommandComandaMercaderia
    {
        public Task<int> CreateComandaMercaderia(ComandaMercaderia comandaMercaderia);
    }
}
