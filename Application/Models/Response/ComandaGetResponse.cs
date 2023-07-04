

namespace Application.Models.Response
{
    public class ComandaGetResponse
    {
        public Guid Id { get; set; }
        public List<MercaderiaGetResponse>? Mercaderias { get; set; }
        public FormaEntrega FormaEntrega { get; set; }
        public double Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}
