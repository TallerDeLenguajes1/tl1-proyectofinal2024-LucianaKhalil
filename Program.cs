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
        PersonajesJson manejadorDePersonajes = new PersonajesJson();
        string nombreArchivoPersonajes = "personaje.json";
        string nombreArchivoHistorial = "historial.json";
        List<Personaje> personajes = new List<Personaje>();

        // Verificar si el archivo "personaje.json" existe y tiene datos
        if (manejadorDePersonajes.Existe(nombreArchivoPersonajes))
        {
            personajes = manejadorDePersonajes.LeerPersonajes(nombreArchivoPersonajes);
            Console.WriteLine("Personajes cargados desde el archivo:");
        }
        else
        {
            // Generar 10 personajes aleatorios si el archivo no existe
            for (int i = 0; i < 10; i++)
            {
                Personaje personaje = await fabrica.ObtenerPersonaje();
                personajes.Add(personaje);
            }
            // Guardar los personajes generados en "personaje.json"
            manejadorDePersonajes.GuardarPersonajes(personajes, nombreArchivoPersonajes);
            Console.WriteLine("Se generaron y guardaron 10 personajes aleatorios:");
        }

        // Mostrar información de los personajes
        foreach (var personaje in personajes)
        {
            Console.WriteLine($"Nombre: {personaje.Datos.Nombre}");
            Console.WriteLine($"Clase: {personaje.Datos.Clase}");
            Console.WriteLine($"Raza: {personaje.Datos.Raza}");
            Console.WriteLine($"Puntos de Vida: {personaje.Datos.PuntosDeVida}");
            Console.WriteLine("Características:");
            Console.WriteLine($"  Fuerza: {personaje.Caracteristicas.Fuerza}");
            Console.WriteLine($"  Destreza: {personaje.Caracteristicas.Destreza}");
            Console.WriteLine($"  Velocidad: {personaje.Caracteristicas.Velocidad}");
            Console.WriteLine("******************");
        }

        // Permitir al usuario elegir un personaje para luchar contra los otros 9
        Console.WriteLine("Elige el índice del personaje que quieres usar para luchar (0-9):");
        int indiceElegido;
        while (!int.TryParse(Console.ReadLine(), out indiceElegido) || indiceElegido < 0 || indiceElegido >= personajes.Count)
        {
            Console.WriteLine("Índice inválido. Por favor elige un número entre 0 y 9:");
        }

        Personaje personajeUsuario = personajes[indiceElegido];
        List<Personaje> enemigos = new List<Personaje>(personajes);
        enemigos.RemoveAt(indiceElegido);

        // Simular luchas y guardar ganadores en "historial.json"
        List<Personaje> ganadores = new List<Personaje>();

    //SIMULAR COMBATE ACA

        // Guardar los ganadores en "historial.json"
        manejadorDePersonajes.GuardarPersonajes(ganadores, nombreArchivoHistorial);
        Console.WriteLine("Ganadores guardados en historial.json");
    }

}
