using System;
using System.Text.Json;
using Proyecto;
public class Program
{
    public static async Task Main(string[] args)
    {
        FabricaDePersonajes fabrica = new FabricaDePersonajes();
        Personaje personajeNuevo = await fabrica.obtenerPersonaje();
    
        if (personajeNuevo != null)
        {
            Console.WriteLine($"Personaje obtenido desde la API: {JsonSerializer.Serialize(personajeNuevo, new JsonSerializerOptions { WriteIndented = true })}");

            // Guardar el personaje en un archivo
            List<Personaje> personajes;

            PersonajesJson manejadorDePersonajes = new PersonajesJson();
            string nombreArchivo = "personaje.json";
            personajes=manejadorDePersonajes.LeerPersonajes(nombreArchivo);
            personajes.Add(personajeNuevo);
            manejadorDePersonajes.GuardarPersonajes(personajes, nombreArchivo);

            // Verificar si el archivo existe y tiene datos
            bool existe = manejadorDePersonajes.Existe(nombreArchivo);
            Console.WriteLine($"¿El archivo {nombreArchivo} existe y tiene datos? {existe}");

            // Leer personajes del archivo
            if (existe)
            {
                List<Personaje> personajesLeidos = manejadorDePersonajes.LeerPersonajes(nombreArchivo);
                Console.WriteLine($"Personajes leídos del archivo: {JsonSerializer.Serialize(personajesLeidos, new JsonSerializerOptions { WriteIndented = true })}");
            }
        }
        else
        {
            Console.WriteLine("No se pudo obtener el personaje desde la API.");
        }
    }
}
