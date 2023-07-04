using Application.Interfaces;
using Application.Models.Response;
using Domain.Entities;

namespace Application.UseCases
{
    public class ServiceComandaMercaderia : IServiceComandaMercaderia
    {
        private readonly IQueryComandaMercaderia _query;
        private readonly ICommandComandaMercaderia _command;

        public ServiceComandaMercaderia(IQueryComandaMercaderia query, ICommandComandaMercaderia command)
        {
            _query = query;
            _command = command;
        }

        public async Task<int> CreateComandaMercaderia(Guid comandaId, int mercaderiaId)
        {
            ComandaMercaderia comandaMercaderia = new ComandaMercaderia
            {
                ComandaId = comandaId,
                MercaderiaId = mercaderiaId
            };

            int comandaMercaderiaId = await _command.CreateComandaMercaderia(comandaMercaderia);

            return comandaMercaderiaId;
        }

        public async Task<IList<MercaderiaComandaResponse>> GetComandaMercaderiaByMercaderiaId(int mercaderiaId)
        {
            IList<ComandaMercaderia> comandaMercaderias = await _query.GetComandaMercaderiaByMercaderiaId(mercaderiaId);

            IList<MercaderiaComandaResponse> response = new List<MercaderiaComandaResponse>();

            if (comandaMercaderias.Count == 0)
            {
                return response;
            }

            foreach(ComandaMercaderia comandaMercaderia in comandaMercaderias) 
            {
                MercaderiaComandaResponse mercaderiaComandaResponse = new MercaderiaComandaResponse()
                {
                    Id = comandaMercaderia.ComandaMercaderiaId,
                    Nombre = comandaMercaderia.Mercaderia.Nombre,
                    Precio = comandaMercaderia.Mercaderia.Precio
                };

                response.Add(mercaderiaComandaResponse);
            }

            return response;
        }
    }
}
