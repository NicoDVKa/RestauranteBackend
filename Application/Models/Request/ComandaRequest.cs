
namespace Application.Models.Request
{
    public class ComandaRequest
    {
        public IList<int>? Mercaderias { get; set;}
        public int FormaEntrega { get; set; }
    }
}
