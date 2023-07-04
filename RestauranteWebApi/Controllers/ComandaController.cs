using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Microsoft.AspNetCore.Mvc;


namespace RestauranteWebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly IServiceComanda _serviceComanda;
        private readonly IServiceMercaderia _serviceMercaderia;
        private readonly IServiceFormaEntrega _serviceFormaEntrega;

        public ComandaController(IServiceComanda serviceComanda, IServiceMercaderia serviceMercaderia, IServiceFormaEntrega serviceFormaEntrega)
        {
            _serviceComanda = serviceComanda;
            _serviceMercaderia = serviceMercaderia;
            _serviceFormaEntrega = serviceFormaEntrega;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? fecha) 
        {
            IList<ComandaResponse> response;
            DateTime date;
            if(DateTime.TryParse(fecha, out date))
            {
                response = await _serviceComanda.GetComandaByDate(date);
                return new JsonResult(response) { StatusCode = 200 };
            }
            else if(fecha != null)
            {
                BadRequest request = new BadRequest()
                {
                    Message = "La fecha ingresada es inválida"
                };

                return BadRequest(request);

            }
            else
            {
                response = await _serviceComanda.GetAll();
                return new JsonResult(response) { StatusCode = 200 } ;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateComanda(ComandaRequest comandaRequest)
        {
            var formaEntregaById = await _serviceFormaEntrega.GetFormaEntregaById(comandaRequest.FormaEntrega);
            if (formaEntregaById == null)
            {   
                BadRequest request = new BadRequest()
                {
                    Message = $"No existe una forma de entrega con el ID {comandaRequest.FormaEntrega}"
                };

                return new JsonResult(request) { StatusCode = 400 };
            }

            if (comandaRequest.Mercaderias == null || comandaRequest.Mercaderias.Count == 0)
            {
                BadRequest request = new BadRequest()
                {
                    Message = "Ingrese mercaderias"
                };

                return new JsonResult(request) { StatusCode = 400 };
            }

            double total = 0;
            MercaderiaResponse mercaderiaResponse;
            foreach(int mercaderiaId in comandaRequest.Mercaderias)
            {
                mercaderiaResponse = await _serviceMercaderia.GetMercaderiaById(mercaderiaId);
                if (mercaderiaResponse == null)
                {
                    BadRequest request = new BadRequest()
                    {
                        Message =$"No existe una mercaderia con el ID {mercaderiaId}"
                    };

                    return new JsonResult(request) { StatusCode = 400 };
                }

                total += mercaderiaResponse.Precio;
            }

           ComandaResponse response = await _serviceComanda.CreateComanda(comandaRequest,total);

            return new JsonResult(response) { StatusCode = 201  };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComandaById(Guid id)
        {
            Guid guid;
            if (!Guid.TryParse(id.ToString(),out guid))
            {
                BadRequest request = new BadRequest()
                {
                    Message = $"Formato de ID inválido"
                };
                return new JsonResult(request) { StatusCode = 400 };
            }
            ComandaGetResponse response = await _serviceComanda.GetComandaById(guid);

            if(response == null)
            {
                BadRequest request = new BadRequest()
                {
                    Message = $"No existe una comanda con el ID {id}"
                };
                return new JsonResult(request) { StatusCode = 404 };
            }

            return new JsonResult(response) { StatusCode = 200};
        }
    }
}
