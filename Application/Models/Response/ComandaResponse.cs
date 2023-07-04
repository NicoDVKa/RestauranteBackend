
namespace Application.Models.Response
{
    public class ComandaResponse
    {
        public Guid Id { get; set; }
        public IList<MercaderiaComandaResponse>? Mercaderias { get; set; }
        public FormaEntrega FormaEntrega { get; set; }
        public double Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}
