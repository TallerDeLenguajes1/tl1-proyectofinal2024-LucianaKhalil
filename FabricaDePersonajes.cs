using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

public class FabricaDePersonajes
{
    private HttpClient client = new HttpClient();
    private  Random random = new Random();
    private  string[] nombrePsje = {"Dijkstra", "Dorian", "Bin", "Remy", "Bor"};//faltan nombres

    public async Task<PersonajeJson> obtenerPersonaje()
    {
        try
        {
            // Obtener la lista de clases
            var clasesResponse = await client.GetAsync("https://www.dnd5eapi.co/api/classes");
            clasesResponse.EnsureSuccessStatusCode();
            var clasesBody = await clasesResponse.Content.ReadAsStringAsync();
            var clases = JsonSerializer.Deserialize<ReferenciaLista>(clasesBody);

            // Obtener la lista de razas
            var razasResponse = await client.GetAsync("https://www.dnd5eapi.co/api/races");
            razasResponse.EnsureSuccessStatusCode();
            var razasBody = await razasResponse.Content.ReadAsStringAsync();
            var razas = JsonSerializer.Deserialize<ReferenciaLista>(razasBody);

            if (clases == null || clases.results.Count == 0 || razas == null || razas.results.Count == 0)
            {
                throw new Exception("No se encontraron clases o razas.");
            }

            // Seleccionar una clase y una raza aleatoriamente
            var claseAleatoria = clases.results[random.Next(clases.results.Count)];
            var razaAleatoria = razas.results[random.Next(razas.results.Count)];
            var nombreAleatorio = nombrePsje[random.Next(nombrePsje.Length)];

            // Crear un personaje aleatorio
            PersonajeJson personajeJson = new PersonajeJson
            {
                Nombre = nombreAleatorio,
                Clase = claseAleatoria.name,
                Raza = razaAleatoria.name,
                PuntosDeVida = random.Next(1, 101)//averiguar
            };

            return personajeJson;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Problemas de acceso a la API");
            Console.WriteLine("Message :{0} ", e.Message);
            return null;
        }
    }
}
//Esta clase envuelve una lista de objetos Referencia. La API devuelve un JSON que contiene una lista de resultados, y esta clase permite deserializar ese JSON.
public class ReferenciaLista
{
    [JsonPropertyName("results")]
    public List<Referencia> results { get; set; }
}

public class Referencia
{
    [JsonPropertyName("index")]
    public string index { get; set; }

    [JsonPropertyName("name")]
    public string name { get; set; }

    [JsonPropertyName("url")]
    public string url { get; set; }
}

public class PersonajeJson
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