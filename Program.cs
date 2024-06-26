using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Proyecto;

public class Program
{
    public static async Task Main(string[] args)
    {
        FabricaDePersonajes fabrica = new FabricaDePersonajes();
        Personaje personajeNuevo = await fabrica.ObtenerPersonaje();
    
        if (personajeNuevo != null)
        {
            Console.WriteLine($"Personaje obtenido desde la API: {JsonSerializer.Serialize(personajeNuevo, new JsonSerializerOptions { WriteIndented = true })}");

            // Guardar el personaje en un archivo
            List<Personaje> personajes;

            PersonajesJson manejadorDePersonajes = new PersonajesJson();
            string nombreArchivo = "personaje.json";
            personajes = manejadorDePersonajes.LeerPersonajes(nombreArchivo) ?? new List<Personaje>();
            personajes.Add(personajeNuevo);
            manejadorDePersonajes.GuardarPersonajes(personajes, nombreArchivo);

            // Verificar si el archivo existe y tiene datos
            bool existe = manejadorDePersonajes.Existe(nombreArchivo);
            Console.WriteLine($"¿El archivo {nombreArchivo} existe y tiene datos? {existe}");

            // Leer personajes del archivo
            if (existe)
            {
                List<Personaje> personajesLeidos = manejadorDePersonajes.LeerPersonajes(nombreArchivo);
                Console.WriteLine($"Personajes leídos del archivo:");

                foreach (var personaje in personajesLeidos)
                {
                    Console.WriteLine($"Nombre: {personaje.Datos.Nombre}");
                    Console.WriteLine($"Clase: {personaje.Datos.Clase}");
                    Console.WriteLine($"Raza: {personaje.Datos.Raza}");
                    Console.WriteLine($"Puntos de Vida: {personaje.Datos.PuntosDeVida}");
                    Console.WriteLine("Características:");
                    Console.WriteLine($"  Fuerza: {personaje.Caracteristicas.Fuerza}");
                    Console.WriteLine($"  Destreza: {personaje.Caracteristicas.Destreza}");
                    Console.WriteLine($"  Armadura: {personaje.Caracteristicas.Armadura}");
                    Console.WriteLine();
                }
            }
        }
        else
        {
            Console.WriteLine("No se pudo obtener el personaje desde la API.");
        }
    }
}
