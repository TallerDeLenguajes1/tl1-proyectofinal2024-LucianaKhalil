using System;
using System.Text.Json;


public class Program
{
    public static async Task Main(string[] args)
    {
        FabricaDePersonajes fabrica = new FabricaDePersonajes();
        PersonajeJson personaje = await fabrica.obtenerPersonaje();

        if (personaje != null)
        {
            Console.WriteLine($"Personaje obtenido desde la API: {JsonSerializer.Serialize(personaje, new JsonSerializerOptions { WriteIndented = true })}");

            // Guardar el personaje en un archivo
            var personajes = new List<PersonajeJson> { personaje };
            PersonajesJson manejadorDePersonajes = new PersonajesJson();
            string nombreArchivo = "personajes.json";
            manejadorDePersonajes.GuardarPersonajes(personajes, nombreArchivo);

            // Verificar si el archivo existe y tiene datos
            bool existe = manejadorDePersonajes.Existe(nombreArchivo);
            Console.WriteLine($"¿El archivo {nombreArchivo} existe y tiene datos? {existe}");

            // Leer personajes del archivo
            if (existe)
            {
                List<PersonajeJson> personajesLeidos = manejadorDePersonajes.LeerPersonajes(nombreArchivo);
                Console.WriteLine($"Personajes leídos del archivo: {JsonSerializer.Serialize(personajesLeidos, new JsonSerializerOptions { WriteIndented = true })}");
            }
        }
        else
        {
            Console.WriteLine("No se pudo obtener el personaje desde la API.");
        }
    }
}
