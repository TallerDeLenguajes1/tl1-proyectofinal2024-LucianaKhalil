using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Proyecto;
using Formato;

namespace ArchivoDeLasTormentas
{
    public class Program
    {
        private static readonly ConsoleColor TituloColor = ConsoleColor.Cyan;
        private static readonly ConsoleColor TextoNormalColor = ConsoleColor.White;
        private static readonly ConsoleColor TextoErrorColor = ConsoleColor.Red;
        private static readonly ConsoleColor TextoExitoColor = ConsoleColor.Green;


        private static string nombreArchivoPersonajes = "personaje.json";
        private static string nombreArchivoHistorial = "historial.json";

        public static async Task Main(string[] args)
        {
            while (true)
            {
                Console.ForegroundColor = TituloColor;
                string titulo = "El Archivo de las tormentas";
                ConsolaFormato.EscribirCentrado("===================================================", TituloColor);
                ConsolaFormato.EscribirCentrado(titulo.ToUpper(), TituloColor);
                ConsolaFormato.EscribirCentrado("===================================================\n", TituloColor);

                await ConsolaFormato.EscribirConEfecto("En el devastado mundo de Roshar, donde la guerra y las tormentas han moldeado el destino de la humanidad, una batalla decisiva se avecina. Los Radiantes, antiguos guerreros venerados por su capacidad de canalizar el poder de las tormentas, han vuelto a levantarse, enfrentando a las implacables fuerzas de Odium. En este momento crucial, tú encarnas a un Radiante, el último baluarte de esperanza en un mundo desgarrado por la guerra. Tu misión es clara: enfrentarte al campeón de Odium y sus nueve temibles seguidores en un duelo épico. La verdadera desolación está a punto de desatarse. El destino de Roshar está en tus manos.", ConsoleColor.White);
                Console.WriteLine();

                ConsolaFormato.EscribirCentrado("====== MENÚ PRINCIPAL ======", TituloColor);
                ConsolaFormato.EscribirCentrado("1. Jugar");
                ConsolaFormato.EscribirCentrado("2. Ver Historial de Ganadores");
                ConsolaFormato.EscribirCentrado("3. Salir");
                ConsolaFormato.EscribirCentrado("====================");

                int opcion = ObtenerOpcionMenu();
                switch (opcion)
                {
                    case 1:
                        await Jugar();
                        break;
                    case 2:
                        MostrarHistorial();
                        break;
                    case 3:
                        Console.WriteLine("¡Nos vemos, Radiante!");
                        return;
                    default:
                        Console.WriteLine("Opción inválida. Elige un número del 1 al 3");
                        break;
                }
            }
        }

        private static int ObtenerOpcionMenu()
        {
            int opcion;
            Console.Write("Selecciona una opción: ");
            while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > 3)
            {
                Console.WriteLine("Opción inválida. Por favor, elige un número entre 1 y 3.");
                Console.Write("Selecciona una opción: ");
            }
            return opcion;
        }

       private static async Task Jugar()
{
    FabricaDePersonajes fabrica = new FabricaDePersonajes();
    PersonajesJson manejadorDePersonajes = new PersonajesJson();//maneja la lectura y escritura de los personajes en un archivo JSON (personaje.json).
    List<Personaje> personajes = new List<Personaje>();

    if (manejadorDePersonajes.Existe(nombreArchivoPersonajes))
    {
        personajes = manejadorDePersonajes.LeerPersonajes(nombreArchivoPersonajes);//si el personaje.json existe
    }
    else
    {
        for (int i = 0; i < 11; i++)
        {
            Personaje personaje = await fabrica.ObtenerPersonaje();
            personajes.Add(personaje);
        }
        manejadorDePersonajes.GuardarPersonajes(personajes, nombreArchivoPersonajes);
    }

    Console.WriteLine("==== LISTA DE PERSONAJES ====");
    int id = 1;
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
        Console.WriteLine("===================================");
        id++;
        await Task.Delay(1000); // Esperar 1 segundo
    }

    Console.WriteLine("Elige el índice del personaje que quieres usar para luchar (1-11):");
    int indiceElegido;
    while (!int.TryParse(Console.ReadLine(), out indiceElegido) || indiceElegido < 1 || indiceElegido > 11)
    {
        Console.WriteLine("Índice inválido. Por favor, elige un número entre 1 y 11:");
    }

    Personaje personajeUsuario = personajes[indiceElegido - 1];
    Console.ForegroundColor = TextoExitoColor;
    Console.WriteLine("Personaje seleccionado:");
    Console.WriteLine($"ID: {indiceElegido}");
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
    enemigos.RemoveAt(indiceElegido - 1);

    bool ganadorUsuario = true;
    Random random = new Random();
    // Guardar las características originales del personaje del usuario
            int fuerzaOriginal = personajeUsuario.Caracteristicas.Fuerza;
            int destrezaOriginal = personajeUsuario.Caracteristicas.Destreza;
            int velocidadOriginal = personajeUsuario.Caracteristicas.Velocidad;
    foreach (var enemigo in enemigos)
    {
        if (!ganadorUsuario)
        {
            break;
        }
        else
        {
            Console.ForegroundColor = TextoErrorColor;
            Console.WriteLine("===================================");
            Console.WriteLine($"Pelea contra: {enemigo.Datos.Nombre}");
            Console.WriteLine($"Clase: {enemigo.Datos.Clase}");
            Console.WriteLine($"Raza: {enemigo.Datos.Raza}");
            Console.WriteLine($"Puntos de Vida: {enemigo.Datos.PuntosDeVida}");
            Console.WriteLine("Características:");
            Console.WriteLine($"  Fuerza: {enemigo.Caracteristicas.Fuerza}");
            Console.WriteLine($"  Destreza: {enemigo.Caracteristicas.Destreza}");
            Console.WriteLine($"  Velocidad: {enemigo.Caracteristicas.Velocidad}");
            Console.WriteLine("===========================");
            Console.ResetColor();

            await MostrarTiradaDeDados(personajeUsuario, enemigo, random);//método asíncrono que muestra la animación o resultado de una tirada de dados para ambos personajes 

            ganadorUsuario = Combate.FormulaCombate(personajeUsuario, enemigo);
            if (ganadorUsuario)
            {
                Console.ForegroundColor = TextoExitoColor;
                Console.WriteLine($"¡{personajeUsuario.Datos.Nombre} ha ganado la pelea contra {enemigo.Datos.Nombre}!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = TextoErrorColor;
                Console.WriteLine($"¡{personajeUsuario.Datos.Nombre} ha perdido la pelea contra {enemigo.Datos.Nombre}!");
                Console.ResetColor();
            }
             // Restaurar las características originales del personaje del usuario
                    personajeUsuario.Caracteristicas.Fuerza = fuerzaOriginal;
                    personajeUsuario.Caracteristicas.Destreza = destrezaOriginal;
                    personajeUsuario.Caracteristicas.Velocidad = velocidadOriginal;
        }
    }

    if (ganadorUsuario)
    {
        Console.ForegroundColor = TextoExitoColor;
        Console.WriteLine("¡Felicidades! ¡Has derrotado a todos los enemigos y ganado el juego!");
        Console.ResetColor();
        HistorialJson historial = new HistorialJson();
        historial.GuardarGanador(personajeUsuario, nombreArchivoHistorial);
        Console.ForegroundColor = TextoExitoColor;
        Console.WriteLine("Ganador guardado en historial.json");
        Console.ResetColor();
    }
    else
    {
        Console.WriteLine("No has logrado derrotar a todos los enemigos. No se guardará el historial.");
    }

    Console.WriteLine("Presiona cualquier tecla para volver al menú principal...");
    Console.ReadKey();
}

        private static async Task MostrarTiradaDeDados(Personaje personajeUsuario, Personaje enemigo, Random random)
        {
            Console.WriteLine("Tiras un dado de 20 caras y elige a qué características deseas sumarle el resultado (1:Fuerza, 2:Destreza o 3:Velocidad)");
            int caracteristicaElegida;
            while (!int.TryParse(Console.ReadLine(), out caracteristicaElegida) || caracteristicaElegida < 1 || caracteristicaElegida > 3)
            {
                Console.WriteLine("Característica inválida. Elija un número entre 1 y 3");
            }

            int resultadoD20 = random.Next(1, 21);
            await MostrarResultadoDado($"{personajeUsuario.Datos.Nombre} tira el dado y obtiene: {resultadoD20}");

            switch (caracteristicaElegida)
            {
                case 1:
                    personajeUsuario.Caracteristicas.Fuerza += resultadoD20;
                    break;
                case 2:
                    personajeUsuario.Caracteristicas.Destreza += resultadoD20;
                    break;
                case 3:
                    personajeUsuario.Caracteristicas.Velocidad += resultadoD20;
                    break;
            }

            await Task.Delay(1000);

            Console.ForegroundColor = TextoNormalColor;
            Console.WriteLine("========Características actualizadas de tu personaje=======");
            Console.WriteLine($"  Fuerza: {personajeUsuario.Caracteristicas.Fuerza}");
            Console.WriteLine($"  Destreza: {personajeUsuario.Caracteristicas.Destreza}");
            Console.WriteLine($"  Velocidad: {personajeUsuario.Caracteristicas.Velocidad}");
            Console.ResetColor();

            int resultadoD20enemigo = random.Next(1, 21);
            await MostrarResultadoDado($"El enemigo tira el dado y obtiene: {resultadoD20enemigo}");

            int caracteristicaEnemigo = random.Next(1, 4);
            switch (caracteristicaEnemigo)
            {
                case 1:
                    enemigo.Caracteristicas.Fuerza += resultadoD20enemigo;
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

            await Task.Delay(1000);
        }

        private static async Task MostrarResultadoDado(string mensaje)//Crea un efecto visual en la consola donde el mensaje parece ser escrito letra por letra, en lugar de aparecer todo de una vez.
        {
            foreach (char c in mensaje)
            {
                Console.Write(c);
                await Task.Delay(50); // Controla la velocidad del efecto
            }
            Console.WriteLine();
            await Task.Delay(1000); // Pausa antes del próximo mensaje
        }

      
      private static void MostrarHistorial()
{
    HistorialJson historialJson = new HistorialJson();

    if (!File.Exists(nombreArchivoHistorial))
    {
        Console.WriteLine("No hay historial disponible.");
        Console.WriteLine("Presiona cualquier tecla para volver al menú principal...");
        Console.ReadKey();
        return;
    }

    List<EntradaHistorial> ganadores = historialJson.LeerGanadores(nombreArchivoHistorial);//historial.json

    Console.WriteLine("==== HISTORIAL DE GANADORES ====");
    foreach (var entrada in ganadores)
    {
        var ganador = entrada.Ganador;
        Console.WriteLine($"Nombre: {ganador.Datos.Nombre}");
        Console.WriteLine($"Clase: {ganador.Datos.Clase}");
        Console.WriteLine($"Raza: {ganador.Datos.Raza}");
        Console.WriteLine($"Puntos de Vida: {ganador.Datos.PuntosDeVida}");
        Console.WriteLine("Características:");
        Console.WriteLine($"  Fuerza: {ganador.Caracteristicas.Fuerza}");
        Console.WriteLine($"  Destreza: {ganador.Caracteristicas.Destreza}");
        Console.WriteLine($"  Velocidad: {ganador.Caracteristicas.Velocidad}");
        Console.WriteLine($"Fecha y Hora: {entrada.FechaYHora}");
        Console.WriteLine("******************");
    }
    Console.WriteLine("Presiona cualquier tecla para volver al menú principal...");
    Console.ReadKey();
}
    }
}