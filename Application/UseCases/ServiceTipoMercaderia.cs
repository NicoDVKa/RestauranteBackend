
using Application.Interfaces;
using Application.Models.Response;
using Domain.Entities;

namespace Application.UseCases
{
    public class ServiceTipoMercaderia : IServiceTipoMercaderia
    {
        private readonly IQueryTipoMercaderia _query;
        private readonly ICommandTipoMercaderia _command;

        public ServiceTipoMercaderia(IQueryTipoMercaderia query, ICommandTipoMercaderia command)
        {
            _query = query;
            _command = command;
        }

        public async Task<TipoMercaderiaResponse> GetTipoMercaderiaById(int id)
        {
            TipoMercaderia tipoMercaderia = await _query.GetTipoMercaderiaById(id);

            if (tipoMercaderia == null)
            {
                return null;
            }

            TipoMercaderiaResponse response = new TipoMercaderiaResponse()
            {
                Id = tipoMercaderia.TipoMercaderiaId,
                Descripcion = tipoMercaderia.Descripcion
            };

            return response;
        }
    }
}
