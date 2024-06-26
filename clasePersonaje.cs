using System.Text.Json.Serialization;

namespace Proyecto
{
    public class Personaje
    {
        [JsonPropertyName("datos")]
        public DatosPersonaje Datos { get; set; }

        [JsonPropertyName("caracteristicas")]
        public CaracteristicasPersonaje Caracteristicas { get; set; }
    }
}
