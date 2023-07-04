using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace RestauranteWebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MercaderiaController : ControllerBase
    {
        private readonly IServiceMercaderia _serviceMercaderia;
        private readonly IServiceTipoMercaderia _serviceTipoMercaderia;
        private readonly IServiceComandaMercaderia _serviceComandaMercaderia;
        private readonly IServiceValidateMercaderia _serviceValidateMercaderia;

        public MercaderiaController(IServiceMercaderia serviceMercaderia, IServiceTipoMercaderia serviceTipoMercaderia, IServiceComandaMercaderia serviceComandaMercaderia, IServiceValidateMercaderia serviceValidateMercaderia) 
        {
            _serviceMercaderia = serviceMercaderia;
            _serviceTipoMercaderia = serviceTipoMercaderia;
            _serviceComandaMercaderia = serviceComandaMercaderia;
            _serviceValidateMercaderia = serviceValidateMercaderia;
        }

        [HttpGet]
        public async Task<IActionResult> SearchMercaderia([FromQuery] int? tipo, [FromQuery] string? nombre, [FromQuery] string? orden = "ASC")
        {

            if (orden.ToLower() != "asc" && orden.ToLower() != "desc")
            {
                BadRequest request = new BadRequest()
                {
                    Message = "Orden inválido"
                };
                return new JsonResult(request) { StatusCode = 400 };
            }

           IList<MercaderiaGetResponse> mercaderias = await _serviceMercaderia.SearchMercaderia(tipo, nombre, orden);

           return new JsonResult(mercaderias);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMercaderia(MercaderiaRequest request)
        {
            if (!await _serviceValidateMercaderia.MercaderiaIsValid(request,false))
            {
                BadRequest badRequest = new BadRequest()
                {
                    Message = _serviceValidateMercaderia.GetError()
                };

                return new JsonResult(badRequest) { StatusCode = 400 };
            }

            TipoMercaderiaResponse tipoMercaderiaById = await _serviceTipoMercaderia.GetTipoMercaderiaById(request.Tipo);
            if (tipoMercaderiaById == null)
            {
                BadRequest badRequest = new BadRequest()
                {
                    Message = $"No existe un tipo de mercaderia con el ID {request.Tipo}"
                };
                return new JsonResult(badRequest) { StatusCode = 400 };
            }

            MercaderiaResponse mercaderiaByName = await _serviceMercaderia.GetMercaderiaByName(request.Nombre);
            if (mercaderiaByName != null)
            {
                BadRequest badRequest = new BadRequest()
                {
                    Message = $"Ya existe una mercaderia con ese nombre"
                };
                return new JsonResult(badRequest) { StatusCode = 409 };
            }

            MercaderiaResponse response = await _serviceMercaderia.CreateMercaderia(request);
            return new JsonResult(response) {StatusCode = 201};    
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMercaderiaById(int id)
        {
            if (id <= 0)
            {
                BadRequest badRequest = new BadRequest()
                {
                    Message = "Ingreso un ID inválido"
                };
                return new JsonResult(badRequest) { StatusCode = 400 };
            }

            MercaderiaResponse response = await _serviceMercaderia.GetMercaderiaById(id);

            if (response == null)
            {
                BadRequest badRequest = new BadRequest()
                {
                    Message = $"No existe una mercaderia con el ID {id}"
                };
                return new JsonResult(badRequest) { StatusCode = 404 };
            }

            return new JsonResult(response) { StatusCode = 200 };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMercaderia(int id, MercaderiaRequest request)
        {
            if (!await _serviceValidateMercaderia.MercaderiaIsValid(request, true))
            {
                BadRequest badRequest = new BadRequest()
                {
                    Message = _serviceValidateMercaderia.GetError()
                };

                return new JsonResult(badRequest) { StatusCode = 400 };
            }

            MercaderiaResponse mercaderiaById = await _serviceMercaderia.GetMercaderiaById(id);
            if (mercaderiaById == null)
            {
                BadRequest badRequest = new BadRequest()
                {
                    Message = $"No existe una mercaderia con el ID {id}"
                };
                return new JsonResult(badRequest) { StatusCode = 404 };
            }

            TipoMercaderiaResponse tipoMercaderiaById = await _serviceTipoMercaderia.GetTipoMercaderiaById(request.Tipo);
            if (tipoMercaderiaById == null)
            {
                BadRequest badRequest = new BadRequest()
                {
                    Message = $"No existe un tipo de mercaderia con el ID {request.Tipo}"
                };
                return new JsonResult(badRequest) { StatusCode = 400 };
            }

            if (request.Nombre != null)
            {
                MercaderiaResponse mercaderiaByName = await _serviceMercaderia.GetMercaderiaByName(request.Nombre);
                if (mercaderiaByName != null && mercaderiaByName.Id != id)
                {
                    BadRequest badRequest = new BadRequest()
                    {
                        Message = $"Ya existe una mercaderia con ese nombre"
                    };
                    return new JsonResult(badRequest) { StatusCode = 409 };
                }

            }
            MercaderiaResponse response = await _serviceMercaderia.UpdateMercaderia(id,request);
            return new JsonResult(response) { StatusCode = 200 };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMercaderia(int id)
        {
            MercaderiaResponse mercaderiaById = await _serviceMercaderia.GetMercaderiaById(id);
            if (mercaderiaById == null)
            {
                BadRequest badRequest = new BadRequest()
                {
                    Message = $"No existe una mercaderia con el ID {id}"
                };
                return new JsonResult(badRequest) { StatusCode = 400 };
            }

            IList<MercaderiaComandaResponse> mercaderiaComandaByMercaderiaId = await _serviceComandaMercaderia.GetComandaMercaderiaByMercaderiaId(id);
            if (mercaderiaComandaByMercaderiaId.Count > 0)
            {
                BadRequest badRequest = new BadRequest()
                {
                    Message = "No se puede eliminar la mercaderia ya que hay comandas que dependen de ella"
                };
                return new JsonResult(badRequest) { StatusCode = 409 };
            }

            MercaderiaResponse response = await _serviceMercaderia.DeleteMercaderia(id);

            return new JsonResult(response) { StatusCode = 200 };
        }
    }
}
