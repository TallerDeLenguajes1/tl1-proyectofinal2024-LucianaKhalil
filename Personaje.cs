using System.Text.Json.Serialization;

public class PersonajeJson{//elimine nivel personaje

    [JsonPropertyName("nombre")]
    public string Nombre { get; set; }

    [JsonPropertyName("clase")]
    public string Clase { get; set; }

    [JsonPropertyName("puntosDeVida")]
    public int PuntosDeVida { get; set; }

    [JsonPropertyName("raza")]
    public string Raza { get; set; }

}
