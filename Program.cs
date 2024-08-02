using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Proyecto;

public class Program
{   
     private static readonly ConsoleColor TituloColor = ConsoleColor.Cyan;
    private static readonly ConsoleColor TextoNormalColor = ConsoleColor.White;
    private static readonly ConsoleColor TextoDestacadoColor = ConsoleColor.Yellow;
    private static readonly ConsoleColor TextoErrorColor = ConsoleColor.Red;
    private static readonly ConsoleColor TextoExitoColor = ConsoleColor.Green;
    public static async Task Main(string[] args)
    {   

        // Mostrar título con efecto de escritura
        Console.ForegroundColor = TituloColor;
        string titulo = "El Archivo de las tormentas";
        Console.WriteLine("\n                                       ==================================================");
        Console.WriteLine($"                                                  {titulo.ToUpper()}      ");
        Console.WriteLine("                                       ==================================================\n");
        Console.ResetColor();

        // Mostrar introducción con efecto de escritura
        await ConsolaFormato.EscribirConEfecto("En el mundo devastado de Roshar, el destino de la humanidad pende de un hilo. La guerra entre los Radiantes, antiguos guerreros imbuidos de poderes sagrados y temidos por su habilidad para manipular las tormentas, ha regresado y las fuerzas de Odium han alcanzado su punto crítico en una serie de batallas decisivas. En esta encrucijada decisiva, tú asumes el papel de un Radiante, dispuesto a luchar por la supervivencia de tu pueblo y el futuro de un mundo devastado por la guerra: un duelo contra el campeón de Odium y sus nueve despojos. La verdadera desolación se acerca.", ConsoleColor.White);
        Console.WriteLine();
        FabricaDePersonajes fabrica = new FabricaDePersonajes();
        PersonajesJson manejadorDePersonajes = new PersonajesJson();
        string nombreArchivoPersonajes = "personaje.json";//guardan todos los personajes
        string nombreArchivoHistorial = "historial.json";//guardan ganadores
        List<Personaje> personajes = new List<Personaje>();
        // Verificar si el archivo "personaje.json" existe y tiene datos
        if (manejadorDePersonajes.Existe(nombreArchivoPersonajes))
        {
            personajes = manejadorDePersonajes.LeerPersonajes(nombreArchivoPersonajes);
        }
        else
        {
            // Generar 11 personajes aleatorios si el archivo no existe
            for (int i = 0; i < 11; i++)//creo 11 para que jugador elija uno y luche contra los otros 10
            {
                Personaje personaje = await fabrica.ObtenerPersonaje();
                personajes.Add(personaje);
            }
            // Guardar los personajes generados en "personaje.json"
            manejadorDePersonajes.GuardarPersonajes(personajes, nombreArchivoPersonajes);
            
        }
        Console.WriteLine("==== LISTA DE PERSONAJES ====");    // Mostrar información de los personajes
        int id = 1; // Inicializa el contador en 1
        foreach (var personaje in personajes)
        {   
            Console.WriteLine($"ID: {id}");
            Console.WriteLine($"Nombre: {personaje.Datos.Nombre}");
            Console.WriteLine($"Clase: {personaje.Datos.Clase}");
            Console.WriteLine($"Raza: {personaje.Datos.Raza}");
            Console.WriteLine($"Puntos de Vida: {personaje.Datos.PuntosDeVida}");
            Console.WriteLine("Características:");
            Console.WriteLine($"  Fuerza: {personaje.Caracteristicas.Fuerza}");
            Console.WriteLine($"  Destreza: {personaje.Caracteristicas.Destreza}");
            Console.WriteLine($"  Velocidad: {personaje.Caracteristicas.Velocidad}");
            Console.WriteLine("******************");
            id++;
             await Task.Delay(1000); // Esperar 1 segundo
        }

        // Permitir al usuario elegir un personaje para luchar contra los otros 9
        Console.WriteLine("Elige el indice del personaje que quieres usar para luchar (1-11):");
        int indiceElegido;
        while (!int.TryParse(Console.ReadLine(), out indiceElegido) || indiceElegido < 1 || indiceElegido > 11)
        {
            Console.WriteLine("Indice inválido. Por favor elige un numero entre 1 y 11:");
        }

        Personaje personajeUsuario = personajes[indiceElegido-1];
        Console.ForegroundColor = TextoExitoColor;
            Console.WriteLine("Personaje seleccionado:");
            Console.WriteLine($"ID: {id-1}");  // Mostrar el ID
            Console.WriteLine($"Nombre: {personajeUsuario.Datos.Nombre}");
            Console.WriteLine($"Clase: {personajeUsuario.Datos.Clase}");
            Console.WriteLine($"Raza: {personajeUsuario.Datos.Raza}");
            Console.WriteLine($"Puntos de Vida: {personajeUsuario.Datos.PuntosDeVida}");
            Console.WriteLine("Características:");
            Console.WriteLine($"  Fuerza: {personajeUsuario.Caracteristicas.Fuerza}");
            Console.WriteLine($"  Destreza: {personajeUsuario.Caracteristicas.Destreza}");
            Console.WriteLine($"  Velocidad: {personajeUsuario.Caracteristicas.Velocidad}");
            Console.ResetColor();

        List<Personaje> enemigos = new List<Personaje>(personajes);
        enemigos.RemoveAt(indiceElegido-1);//separar personaje usuario de los enemigos

        
        List<Personaje> ganadores = new List<Personaje>();//lista para guardar ganadores
        bool ganadorUsuario= true;
        Random random=new Random();

        foreach(var enemigo in enemigos)
        {
            if(!ganadorUsuario){
                break;
            }else{
                Console.ForegroundColor = TextoExitoColor;
                Console.WriteLine($"Pelea contra: {enemigo.Datos.Nombre}");
                Console.WriteLine("************************");
                Console.ResetColor();
                //SIMULAR TIRADA DE DADOS D20
                Console.WriteLine("Tira un dado de 20 caras y elige a que caracteristicas deseas sumarle el resultado(1:Fuerza, 2:Destreza o 3:Velocidad)");
                int caracteristicaElegida;
                    while (!int.TryParse(Console.ReadLine(), out caracteristicaElegida) || caracteristicaElegida < 1 || caracteristicaElegida> 3)
                    {
                        Console.WriteLine("Cracateristica inválida. Elija un numero entre 1 y 3");
                    }  
                int resultadoD20=random.Next(1,21);//D20
                Console.WriteLine($"El resultado del dado es: {resultadoD20}");
                 switch(caracteristicaElegida){
                    case 1:
                        personajeUsuario.Caracteristicas.Fuerza+=resultadoD20;
                    break;
                    case 2:
                        personajeUsuario.Caracteristicas.Destreza+=resultadoD20;
                    break;
                    case 3:
                        personajeUsuario.Caracteristicas.Velocidad+=resultadoD20;
                    break;
        }
            Console.ForegroundColor = TextoNormalColor;
            Console.WriteLine("Características actualizadas del personaje elegido:");
            Console.WriteLine($"  Fuerza: {personajeUsuario.Caracteristicas.Fuerza}");
            Console.WriteLine($"  Destreza: {personajeUsuario.Caracteristicas.Destreza}");
            Console.WriteLine($"  Velocidad: {personajeUsuario.Caracteristicas.Velocidad}");
            Console.ResetColor();
            //bonos d20 aleatorio para cada enemigo
            int resultadoD20enemigo=random.Next(1,21);
            Console.WriteLine($"Resultado del dado para el enemigo: {resultadoD20enemigo}");//eliminar despues
            int caracteristicaEnemigo = random.Next(1, 4);
            switch (caracteristicaEnemigo)
            {
                case 1:
                    enemigo.Caracteristicas.Fuerza +=resultadoD20enemigo;
                    Console.WriteLine($"El enemigo recibe un bono de {resultadoD20enemigo} en Fuerza.");
                    break;
                case 2:
                    enemigo.Caracteristicas.Destreza += resultadoD20enemigo;
                    Console.WriteLine($"El enemigo recibe un bono de {resultadoD20enemigo} en Destreza.");
                    break;
                case 3:
                    enemigo.Caracteristicas.Velocidad += resultadoD20enemigo;
                    Console.WriteLine($"El enemigo recibe un bono de {resultadoD20enemigo} en Velocidad.");
                    break;
            }
                //COMBATE
                Console.ForegroundColor = TextoDestacadoColor;
                Console.WriteLine("Comienza el combate:");
                Console.ResetColor();

                ganadorUsuario=Combate.FormulaCombate(personajeUsuario, enemigo);
                if(ganadorUsuario){
                    Console.ForegroundColor = TextoExitoColor;
                    Console.WriteLine($"¡{personajeUsuario.Datos.Nombre} ha ganado la pelea contra {enemigo.Datos.Nombre}!");
                    Console.ResetColor();
                     ganadores.Add(personajeUsuario);
                }else{
                     Console.ForegroundColor = TextoErrorColor;
                    Console.WriteLine($"¡{personajeUsuario.Datos.Nombre} ha perdido la pelea contra {enemigo.Datos.Nombre}!");
                    Console.ResetColor();
                    ganadores.Add(enemigo);
                }

            }
        }
        // Guardar los ganadores en "historial.json"
        manejadorDePersonajes.GuardarPersonajes(ganadores, nombreArchivoHistorial);
        Console.ForegroundColor = TextoExitoColor;
        Console.WriteLine("Ganadores guardados en historial.json");
        Console.ResetColor();
    }

}
