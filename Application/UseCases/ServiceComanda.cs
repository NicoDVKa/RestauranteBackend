using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;

namespace Application.UseCases
{
    public  class ServiceComanda : IServiceComanda
    {
        private readonly IQueryComanda _queryComanda;
        private readonly ICommandComanda _commandComanda;
        private readonly ICommandComandaMercaderia _commandComandaMercaderia;
        private readonly IQueryComandaMercaderia _queryComandaMercaderia;

        public ServiceComanda(IQueryComanda query, ICommandComanda command, ICommandComandaMercaderia commandComandaMercaderia, IQueryComandaMercaderia queryComandaMercaderia)
        {
            _queryComanda = query;
            _commandComanda = command;
            _commandComandaMercaderia = commandComandaMercaderia;
            _queryComandaMercaderia = queryComandaMercaderia;
        }

        public async Task<ComandaResponse> CreateComanda(ComandaRequest request, double total)
        {
            int totalInt =  (int)total;

            DateTime localDateTime = DateTime.Now;
            DateTime localDateTimeWithKind = DateTime.SpecifyKind(localDateTime, DateTimeKind.Local);
            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(localDateTimeWithKind);

            Comanda comanda = new Comanda
            {
                PrecioTotal = totalInt,
                Fecha = utcDateTime.AddHours(-3),
                FormaEntregaId = request.FormaEntrega
            };

            Comanda create = await _commandComanda.CreateComanda(comanda);

            // Inserto las mercaderias.
            foreach(int mercaderiaId in request.Mercaderias)
            {
                ComandaMercaderia comandaMercaderia = new ComandaMercaderia() 
                { 
                    ComandaId = create.ComandaId, 
                    MercaderiaId = mercaderiaId
                };
                await _commandComandaMercaderia.CreateComandaMercaderia(comandaMercaderia);
            }

            // Busco la comanda por el id para obtener el detalle de la mercaderia.
            Comanda responseComanda = await _queryComanda.GetComandaById(create.ComandaId);

            // Extraigo las mercaderias
            IList<MercaderiaComandaResponse> mercaderias = new List<MercaderiaComandaResponse>();

            foreach (var comandaMercaderia in responseComanda.ComandaMercaderias)
            {
                MercaderiaComandaResponse responseMercaderia = new MercaderiaComandaResponse()
                {
                    Id = comandaMercaderia.ComandaMercaderiaId,
                    Nombre = comandaMercaderia.Mercaderia.Nombre,
                    Precio = comandaMercaderia.Mercaderia.Precio
                };

                mercaderias.Add(responseMercaderia);
            }

            ComandaResponse response = new ComandaResponse
            {
                Id = create.ComandaId,
                Total = create.PrecioTotal,
                Fecha = create.Fecha,
                FormaEntrega = new Models.Response.FormaEntrega
                {
                    Id = create.FormaEntrega.FormaEntregaId,
                    Descripcion = create.FormaEntrega.Descripcion
                },
                Mercaderias = mercaderias
            };

            return response;
        }

        public async Task<IList<ComandaResponse>> GetAll()
        {
            IList<Comanda> comandas = await _queryComanda.GetAll();
            IList<ComandaResponse> response = new List<ComandaResponse>();

            if (comandas.Count == 0)
            {
                return response;
            }

            foreach(Comanda comanda in comandas)
            {
                IList<MercaderiaComandaResponse> mercaderias = new List<MercaderiaComandaResponse>();

                foreach(var comandaMercaderia in comanda.ComandaMercaderias)
                {
                    MercaderiaComandaResponse responseMercaderia = new MercaderiaComandaResponse()
                    {
                        Id = comandaMercaderia.ComandaMercaderiaId,
                        Nombre = comandaMercaderia.Mercaderia.Nombre,
                        Precio = comandaMercaderia.Mercaderia.Precio
                    };

                    mercaderias.Add(responseMercaderia);
                }

                ComandaResponse responseComanda = new ComandaResponse()
                {
                    Id = comanda.ComandaId,
                    Total = comanda.PrecioTotal,
                    Fecha = comanda.Fecha,
                    FormaEntrega = new Models.Response.FormaEntrega()
                    {
                        Id = comanda.FormaEntrega.FormaEntregaId,
                        Descripcion = comanda.FormaEntrega.Descripcion,
                    },

                    Mercaderias = mercaderias   
                };

                response.Add(responseComanda);
            }

            return response;
        }

        public async Task<IList<ComandaResponse>> GetComandaByDate(DateTime date)
        {
            IList<Comanda> comandas = await _queryComanda.GetComandaByDate(date);
            IList<ComandaResponse> response = new List<ComandaResponse>();

            if (comandas.Count == 0)
            {
                return response;
            }

            foreach (Comanda comanda in comandas)
            {
                IList<MercaderiaComandaResponse> mercaderias = new List<MercaderiaComandaResponse>();

                foreach (var comandaMercaderia in comanda.ComandaMercaderias)
                {
                    MercaderiaComandaResponse responseMercaderia = new MercaderiaComandaResponse()
                    {
                        Id = comandaMercaderia.ComandaMercaderiaId,
                        Nombre = comandaMercaderia.Mercaderia.Nombre,
                        Precio = comandaMercaderia.Mercaderia.Precio
                    };

                    mercaderias.Add(responseMercaderia);
                }

                ComandaResponse responseComanda = new ComandaResponse()
                {
                    Id = comanda.ComandaId,
                    Total = comanda.PrecioTotal,
                    Fecha = comanda.Fecha,
                    FormaEntrega = new Models.Response.FormaEntrega()
                    {
                        Id = comanda.FormaEntrega.FormaEntregaId,
                        Descripcion = comanda.FormaEntrega.Descripcion,
                    },

                    Mercaderias = mercaderias
                };

                response.Add(responseComanda);
            }

            return response;
        }

        public async Task<ComandaGetResponse> GetComandaById(Guid id)
        {
            Comanda comanda = await _queryComanda.GetComandaById(id);

            if (comanda == null) 
            {
                return null;
            }

            List<MercaderiaGetResponse>  mercaderias = new List<MercaderiaGetResponse>();

            foreach(var mercaderiaComanda in comanda.ComandaMercaderias)
            {
                MercaderiaGetResponse mercaderiaGetResponse = new MercaderiaGetResponse()
                {
                    Id = mercaderiaComanda.Mercaderia.MercaderiaId,
                    Nombre = mercaderiaComanda.Mercaderia.Nombre,
                    Precio = mercaderiaComanda.Mercaderia.Precio,
                    Imagen =  mercaderiaComanda.Mercaderia.Imagen,
                    Tipo = new TipoMercaderiaResponse()
                    {
                        Id = mercaderiaComanda.Mercaderia.TipoMercaderia.TipoMercaderiaId,
                        Descripcion = mercaderiaComanda.Mercaderia.TipoMercaderia.Descripcion
                    }
                };

                mercaderias.Add(mercaderiaGetResponse);
            }

            ComandaGetResponse response = new ComandaGetResponse()
            {
                Id  = comanda.ComandaId,
                Fecha = comanda.Fecha,
                Total = comanda.PrecioTotal,
                FormaEntrega = new Models.Response.FormaEntrega
                {
                    Id = comanda.FormaEntrega.FormaEntregaId,
                    Descripcion = comanda.FormaEntrega.Descripcion
                },
                Mercaderias = mercaderias
            };

            return response;
        }

    }
}
