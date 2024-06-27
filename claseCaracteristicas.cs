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

        [JsonPropertyName("velocidad")]
        public int Velocidad { get; set; }
    }


}
//hacer metodo que de acuerdo a su raza y especie determine las caracteristicas y luego se le sume aleatoriamente con dado

