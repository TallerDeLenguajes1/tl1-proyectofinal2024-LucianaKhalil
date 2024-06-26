using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Proyecto
{
    public class FabricaDePersonajes
    {
        private HttpClient client = new HttpClient();
        private Random random = new Random();
        private string[] nombresPersonaje = { "Dijkstra", "Dorian", "Bin", "Remy", "Bor" };

        public async Task<Personaje> ObtenerPersonaje()
        {
            try
            {
                // Obtener la lista de clases
                var clasesResponse = await client.GetAsync("https://www.dnd5eapi.co/api/classes");
                clasesResponse.EnsureSuccessStatusCode();
                var contenidoClases = await clasesResponse.Content.ReadAsStringAsync();
                var listaClases = JsonSerializer.Deserialize<ReferenciaLista>(contenidoClases);

                // Obtener la lista de razas
                var razasResponse = await client.GetAsync("https://www.dnd5eapi.co/api/races");
                razasResponse.EnsureSuccessStatusCode();
                var contenidoRazas = await razasResponse.Content.ReadAsStringAsync();
                var listaRazas = JsonSerializer.Deserialize<ReferenciaLista>(contenidoRazas);

                if (listaClases == null || listaClases.Results.Count == 0 || listaRazas == null || listaRazas.Results.Count == 0)
                {
                    throw new Exception("No se encontraron clases o razas.");
                }

                // Seleccionar una clase y una raza aleatoriamente
                var claseAleatoria = listaClases.Results[random.Next(listaClases.Results.Count)];
                var razaAleatoria = listaRazas.Results[random.Next(listaRazas.Results.Count)];
                var nombreAleatorio = nombresPersonaje[random.Next(nombresPersonaje.Length)];

                // Crear un personaje aleatorio
                Personaje personajeJson = new Personaje()
                {
                    Datos = new DatosPersonaje
                    {
                        Nombre = nombreAleatorio,
                        Clase = claseAleatoria.Nombre,
                        Raza = razaAleatoria.Nombre,
                        PuntosDeVida = 100 // Averiguar cómo calcular
                    },
                    Caracteristicas = new CaracteristicasPersonaje
                    {
                        Fuerza = random.Next(1, 21),
                        Destreza = random.Next(1, 21),
                        Armadura = random.Next(10, 20)
                    }
                };

                return personajeJson;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Problemas de acceso a la API");
                Console.WriteLine("Mensaje: {0} ", e.Message);
                return null;
            }
        }
    }

    public class ReferenciaLista
    {
        [JsonPropertyName("results")]
        public List<Referencia> Results { get; set; }
    }

    public class Referencia
    {
        [JsonPropertyName("index")]
        public string Indice { get; set; }

        [JsonPropertyName("name")]
        public string Nombre { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
