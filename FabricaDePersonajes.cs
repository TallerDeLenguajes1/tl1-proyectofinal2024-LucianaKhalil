using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using claseAPI;
using caracteristicasBonosyRazas;

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
                    Console.WriteLine("No se encontraron razas o clases");
                }

                // Seleccionar una clase y una raza aleatoriamente
                var claseAleatoria = listaClases.Results[random.Next(listaClases.Results.Count)];
                var razaAleatoria = listaRazas.Results[random.Next(listaRazas.Results.Count)];
                var nombreAleatorio = nombresPersonaje[random.Next(nombresPersonaje.Length)];
                
                //instancia bonos por clase y raza
                var bonos= new Bonos();

                //obtener los bonos
                var bonosRaza=bonos.ObtenerBonosPorRaza(razaAleatoria.name);
                var bonosClase=bonos.ObtenerBonosPorClase(claseAleatoria.name);
                // Crear un personaje aleatorio
                Personaje personajeJson = new Personaje()
                {
                    Datos = new DatosPersonaje
                    {
                        Nombre = nombreAleatorio,
                        Clase = claseAleatoria.name,
                        Raza = razaAleatoria.name,
                        PuntosDeVida = 100 //inicializo vida completa
                    },
                    Caracteristicas = new CaracteristicasPersonaje
                    {
                        Fuerza = bonosRaza.Fuerza+bonosClase.Fuerza,
                        Destreza = bonosRaza.Destreza+bonosClase.Destreza,
                        Velocidad = bonosRaza.Velocidad+bonosClase.Velocidad
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
}