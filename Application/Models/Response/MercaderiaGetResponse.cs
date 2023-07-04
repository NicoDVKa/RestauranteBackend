
namespace Application.Models.Response
{
    public class MercaderiaGetResponse
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public double Precio { get; set; }
        public TipoMercaderiaResponse Tipo { get; set; }
        public string? Imagen { get; set; }
    }
}
