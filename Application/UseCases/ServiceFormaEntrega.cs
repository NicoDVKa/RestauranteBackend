using Application.Interfaces;
using Application.Models.Response;

namespace Application.UseCases
{
    public class ServiceFormaEntrega : IServiceFormaEntrega
    {
        private readonly IQueryFormaEntrega _query;
        private readonly ICommandFormaEntrega _command;

        public ServiceFormaEntrega(IQueryFormaEntrega query, ICommandFormaEntrega command)
        {
            _query = query;
            _command = command;
        }

        public async Task<IList<Models.Response.FormaEntrega>> GetAllFormaEntrega()
        {
            IList<Domain.Entities.FormaEntrega> formaEntregas = await _query.GetAllFormaEntrega();

            IList<Models.Response.FormaEntrega> response = new List<Models.Response.FormaEntrega>();

            if (formaEntregas.Count == 0)
            {
                return response;
            }

            foreach(Domain.Entities.FormaEntrega formaEntrega in formaEntregas) 
            {
                Models.Response.FormaEntrega responseFormaEntrega = new Models.Response.FormaEntrega
                {
                    Id = formaEntrega.FormaEntregaId,
                    Descripcion = formaEntrega.Descripcion
                };

                response.Add(responseFormaEntrega);
            }

            return response;
        }

        public async Task<Models.Response.FormaEntrega> GetFormaEntregaById(int id)
        {
            Domain.Entities.FormaEntrega formaEntrega = await _query.GetFormaEntregaById(id);

            if (formaEntrega == null)
            {
                return null;
            }

            Application.Models.Response.FormaEntrega response = new Application.Models.Response.FormaEntrega()
            {
                Id = formaEntrega.FormaEntregaId,
                Descripcion = formaEntrega.Descripcion
            };

            return response;
        }
    }
}
