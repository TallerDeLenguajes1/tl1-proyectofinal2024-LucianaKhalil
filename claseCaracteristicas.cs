using System.Text.Json.Serialization;
using System;
using System.Net.Http;
using System.Text.Json;

namespace Proyecto
{
    public class CaracteristicasPersonaje
    {
        [JsonPropertyName("fuerza")]
        public int Fuerza { get; set; }

        [JsonPropertyName("destreza")]
        public int Destreza { get; set; }

        [JsonPropertyName("armadura")]
        public int Armadura { get; set; }
    }
}
