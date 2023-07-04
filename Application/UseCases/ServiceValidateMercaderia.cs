
using Application.Interfaces;
using Application.Models.Request;
using System.Text.RegularExpressions;

namespace Application.UseCases
{
    public class ServiceValidateMercaderia : IServiceValidateMercaderia
    {
        private string _error;

        public string GetError()
        {
           return _error;
        }

        public async Task<bool> MercaderiaIsValid(MercaderiaRequest request, bool allowNull)
        {
            if (!stringIsValid("Nombre",request.Nombre,50,allowNull))
            {
                return false;
            }

            if (request.Precio <= 0 || request.Precio > int.MaxValue)
            {
                _error = "precio-El precio ingresado no cumple con el formato";
                return false;
            }

            if (!stringIsValid("Ingredientes", request.Ingredientes, 255, allowNull))
            {
                return false;
            }

            if (!stringIsValid("Preparacion", request.Preparacion, 255, allowNull))
            {
                return false;
            }

            if (!stringIsValid("Imagen", request.Imagen, 255, allowNull))
            {
                return false;
            }

            if (request.Imagen != null)
            {
                try
                {
                    // Validar URL de la imagen

                    var client = new HttpClient();
                    var response = await client.GetAsync(request.Imagen);


                    if (!response.IsSuccessStatusCode)
                    {
                        _error = "imagen-La URL de la imagen ingresada no es valida";
                        return false;
                    }

                    string contentType = response.Content.Headers.ContentType.MediaType;
                    if (!contentType.StartsWith("image/"))
                    {
                        _error = "imagen-La URL no devuelve una imagen";
                        return false;
                    }

                    var extension = contentType.Split('/')[1];

                    var regex = new Regex(@"^\.?(jpg|jpeg|png|gif|avif|webp)$", RegexOptions.IgnoreCase);
                    if (!regex.IsMatch(extension))
                    {
                        _error = "imagen-La URL no devuelve una imagen con el formato valido";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    _error = "imagen-Ha ocurrido un error con la URL de la imagen";
                    return false;
                }
            }
            
            return true;    
        }

        public bool stringIsValid(string tag, string? veryfy, int maxLenght, bool allowNull)
        {
            if (!allowNull && veryfy == null)
            {
                _error = $"{tag}-Ingrese un valor";
                return false;
            }

            if (veryfy != null)
            {
                if (veryfy.Trim() == "")
                {
                    _error = $"{tag}-Ingrese un valor";
                    return false;
                }

                if (veryfy.Length == 0 || veryfy.Length > maxLenght)
                {
                    _error = $"{tag}-El campo no cumple con el formato";
                    return false;
                }


            }

            return true;
        }
    }
}
