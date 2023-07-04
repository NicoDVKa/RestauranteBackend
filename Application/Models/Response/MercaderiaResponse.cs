
namespace Application.Models.Response
{
    public class MercaderiaResponse
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public TipoMercaderiaResponse Tipo { get; set; }
        public double Precio { get; set; }
        public string? Ingredientes { get; set; }
        public string? Preparacion { get; set; }
        public string? Imagen { get; set; }
    }
}
