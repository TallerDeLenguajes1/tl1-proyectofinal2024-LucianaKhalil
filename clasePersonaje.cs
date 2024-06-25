using Proyecto;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
public class Personaje
{
    [JsonPropertyName("nombre")]
    public string Nombre { get; set; }

    [JsonPropertyName("clase")]
    public string Clase { get; set; }

    [JsonPropertyName("puntosDeVida")]
    public int PuntosDeVida { get; set; }

    [JsonPropertyName("raza")]
    public string Raza { get; set; }
}
