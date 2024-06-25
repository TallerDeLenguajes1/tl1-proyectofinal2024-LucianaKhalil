namespace Proyecto{
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

public class FabricaDePersonajes
{
    private HttpClient client = new HttpClient();
    private  Random random = new Random();
    private  string[] nombrePsje = {"Dijkstra", "Dorian", "Bin", "Remy", "Bor"};//faltan nombres
    public async Task<Personaje> obtenerPersonaje()
    {
        try
        {
            // Obtener la lista de clases
            var clasesResponse = await client.GetAsync("https://www.dnd5eapi.co/api/classes");
            clasesResponse.EnsureSuccessStatusCode();
            var clasess = await clasesResponse.Content.ReadAsStringAsync();
            var clases = JsonSerializer.Deserialize<ReferenciaLista>(clasess);

            // Obtener la lista de razas
            var razasResponse = await client.GetAsync("https://www.dnd5eapi.co/api/races");
            razasResponse.EnsureSuccessStatusCode();
            var razass = await razasResponse.Content.ReadAsStringAsync();
            var razas = JsonSerializer.Deserialize<ReferenciaLista>(razass);

            if (clases == null || clases.results.Count == 0 || razas == null || razas.results.Count == 0)
            {
                throw new Exception("No se encontraron clases o razas.");
            }

            // Seleccionar una clase y una raza aleatoriamente
            var claseAleatoria = clases.results[random.Next(clases.results.Count)];
            var razaAleatoria = razas.results[random.Next(razas.results.Count)];
            var nombreAleatorio = nombrePsje[random.Next(nombrePsje.Length)];

            // Crear un personaje aleatorio
            Personaje personajeJson = new Personaje()
            {
                Nombre = nombreAleatorio,
                Clase = claseAleatoria.name,
                Raza = razaAleatoria.name,
                PuntosDeVida = 100//averiguar

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
}
