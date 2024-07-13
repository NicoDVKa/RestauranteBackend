
using Application.Models.Request;

namespace Application.Interfaces
{
    public interface IServiceValidateMercaderia
    {
        public Task<bool> MercaderiaIsValid(MercaderiaRequest request, bool allowNull);
        public string GetError();
        public bool StringIsValid(string tag, string? veryfy, int maxLenght, bool allowNull);
    }
}
