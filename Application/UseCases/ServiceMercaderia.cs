using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;

namespace Application.UseCases
{
    public class ServiceMercaderia : IServiceMercaderia
    {
        private readonly IQueryMercaderia _query;
        private readonly ICommandMercaderia _command;

        public ServiceMercaderia(IQueryMercaderia query, ICommandMercaderia command)
        {
            _query = query;
            _command = command;
        }

        public async Task<IList<MercaderiaResponse>> GetAllMercaderia()
        {
            IList<Mercaderia> mercaderias = await _query.GetAllMercaderias();
            IList<MercaderiaResponse> responseMercaderias = new List<MercaderiaResponse>();

            if(mercaderias.Count == 0)
            {
                return responseMercaderias;
            }

            foreach(Mercaderia mercaderia in mercaderias)
            {
                MercaderiaResponse response = new MercaderiaResponse
                {
                    Id = mercaderia.MercaderiaId,
                    Nombre = mercaderia.Nombre,
                    Precio = mercaderia.Precio,
                    Ingredientes = mercaderia.Ingredientes,
                    Preparacion = mercaderia.Preparacion,
                    Imagen = mercaderia.Imagen,

                    Tipo = new TipoMercaderiaResponse
                    {
                        Id = mercaderia.TipoMercaderia.TipoMercaderiaId,
                        Descripcion = mercaderia.TipoMercaderia.Descripcion
                    }
                };

                responseMercaderias.Add(response);
            }

            return responseMercaderias;
        }

        public async Task<MercaderiaResponse> CreateMercaderia(MercaderiaRequest request)
        {
            Mercaderia mercaderia = new Mercaderia()
            {
                Nombre = request.Nombre.Trim().ToLower(),
                Precio = (int)request.Precio,
                TipoMercaderiaId = request.Tipo,
                Ingredientes = request.Ingredientes.Trim().ToLower(),
                Preparacion = request.Preparacion.Trim().ToLower(),
                Imagen = request.Imagen.Trim()
            };

            Mercaderia create = await _command.CreateMercaderia(mercaderia);

            MercaderiaResponse response = new MercaderiaResponse()
            {
                Id = create.MercaderiaId,
                Nombre = create.Nombre,
                Precio = create.Precio,
                Ingredientes = create.Ingredientes,
                Preparacion = create.Preparacion,
                Imagen = create.Imagen,

                Tipo = new TipoMercaderiaResponse()
                {
                    Id = create.TipoMercaderia.TipoMercaderiaId,
                    Descripcion = create.TipoMercaderia.Descripcion
                }
            };

            return response;

        }

        public async Task<MercaderiaResponse> UpdateMercaderia(int id, MercaderiaRequest request)
        {
            Mercaderia getMercaderiaById = await _query.GetMercaderiaById(id);

            Mercaderia mercaderia = new Mercaderia()
            {
                Nombre = request.Nombre == null ? getMercaderiaById.Nombre : request.Nombre.Trim().ToLower(),
                Precio = (int)request.Precio ,
                TipoMercaderiaId = request.Tipo,
                Ingredientes = request.Ingredientes == null ? getMercaderiaById.Ingredientes : request.Ingredientes.Trim().ToLower(),
                Preparacion = request.Preparacion == null ? getMercaderiaById.Preparacion : request.Preparacion.Trim().ToLower(),
                Imagen = request.Imagen == null ? getMercaderiaById.Imagen : request.Imagen.Trim()
            };

            Mercaderia create = await _command.UpdateMercaderia(id,mercaderia);

            MercaderiaResponse response = new MercaderiaResponse()
            {
                Id = create.MercaderiaId,
                Nombre = create.Nombre,
                Precio = create.Precio,
                Ingredientes = create.Ingredientes,
                Preparacion = create.Preparacion,
                Imagen = create.Imagen,

                Tipo = new TipoMercaderiaResponse()
                {
                    Id = create.TipoMercaderia.TipoMercaderiaId,
                    Descripcion = create.TipoMercaderia.Descripcion
                }
            };

            return response;
        }

        public async Task<MercaderiaResponse> GetMercaderiaByName(string name)
        {
            Mercaderia mercaderia = await _query.GetMercaderiaByName(name);

            if (mercaderia == null)
            {
                return null;
            }

            MercaderiaResponse response = new MercaderiaResponse()
            {
                Id = mercaderia.MercaderiaId,
                Nombre = mercaderia.Nombre,
                Precio = mercaderia.Precio,
                Ingredientes = mercaderia.Ingredientes,
                Preparacion = mercaderia.Preparacion,
                Imagen = mercaderia.Imagen,

                Tipo = new TipoMercaderiaResponse()
                {
                    Id = mercaderia.TipoMercaderia.TipoMercaderiaId,
                    Descripcion = mercaderia.TipoMercaderia.Descripcion
                }
            };


            return response;
        }

        public async Task<MercaderiaResponse> GetMercaderiaById(int id)
        {
            Mercaderia mercaderia = await _query.GetMercaderiaById(id);

            if (mercaderia == null)
            {
                return null;
            }

            MercaderiaResponse response = new MercaderiaResponse()
            {
                Id = mercaderia.MercaderiaId,
                Nombre = mercaderia.Nombre,
                Precio = mercaderia.Precio,
                Ingredientes = mercaderia.Ingredientes,
                Preparacion = mercaderia.Preparacion,
                Imagen = mercaderia.Imagen,

                Tipo = new TipoMercaderiaResponse()
                {
                    Id = mercaderia.TipoMercaderia.TipoMercaderiaId,
                    Descripcion = mercaderia.TipoMercaderia.Descripcion
                }
            };


            return response;
        }

        public async Task<MercaderiaResponse> DeleteMercaderia(int id)
        {
            Mercaderia delete = await _command.DeleteMercaderia(id);

            MercaderiaResponse response = new MercaderiaResponse()
            {
                Id = delete.MercaderiaId,
                Nombre = delete.Nombre,
                Precio = delete.Precio,
                Ingredientes = delete.Ingredientes,
                Preparacion = delete.Preparacion,
                Imagen = delete.Imagen,

                Tipo = new TipoMercaderiaResponse()
                {
                    Id = delete.TipoMercaderia.TipoMercaderiaId,
                    Descripcion = delete.TipoMercaderia.Descripcion
                }
            };

            return response;
        }

        public async Task<IList<MercaderiaGetResponse>> SearchMercaderia(int? tipo, string? name, string orden)
        {
            name = name != null ? name.ToLower() : null;
            orden =  orden.ToLower();

            IList<Mercaderia> mercaderias = await _query.SearchMercaderia(tipo, name, orden);
            IList<MercaderiaGetResponse> responseMercaderias = new List<MercaderiaGetResponse>();

            if (mercaderias.Count == 0)
            {
                return responseMercaderias;
            }

            foreach (Mercaderia mercaderia in mercaderias)
            {
                MercaderiaGetResponse response = new MercaderiaGetResponse
                {
                    Id = mercaderia.MercaderiaId,
                    Nombre = mercaderia.Nombre,
                    Precio = mercaderia.Precio,
                    Imagen = mercaderia.Imagen,

                    Tipo = new TipoMercaderiaResponse
                    {
                        Id = mercaderia.TipoMercaderia.TipoMercaderiaId,
                        Descripcion = mercaderia.TipoMercaderia.Descripcion
                    }
                };

                responseMercaderias.Add(response);
            }

            return responseMercaderias;
        }
    }
}
