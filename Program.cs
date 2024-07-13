using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Proyecto;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("En el mundo de Roshar, la guerra entre los humanos y las fuerzas de Odium ha alcanzado un punto crítico. Los Radiantes, antiguos guerreros imbuidos de poderes sagrados y temidos por su habilidad para manipular las tormentas, han regresado. En un mundo al borde del colapso, estos seres legendarios traen consigo la esperanza de restaurar el equilibrio perdido y enfrentar a los agentes del Caos que amenazan con sumir a Roshar en la oscuridad eterna. En esta encrucijada decisiva, tú asumes el papel de un Radiante, dispuesto a luchar por la supervivencia de tu pueblo y el futuro de un mundo devastado por la guerra y la desolación.");
        FabricaDePersonajes fabrica = new FabricaDePersonajes();
        PersonajesJson manejadorDePersonajes = new PersonajesJson();
        string nombreArchivoPersonajes = "personaje.json";//guardan todos los personajes
        string nombreArchivoHistorial = "historial.json";//guardan ganadores
        List<Personaje> personajes = new List<Personaje>();
        // Verificar si el archivo "personaje.json" existe y tiene datos
        if (manejadorDePersonajes.Existe(nombreArchivoPersonajes))
        {
            personajes = manejadorDePersonajes.LeerPersonajes(nombreArchivoPersonajes);
            Console.WriteLine("Personajes cargados desde el archivo:");
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
        Console.WriteLine("Elige el indice del personaje que quieres usar para luchar (0-10):");
        int indiceElegido;
        while (!int.TryParse(Console.ReadLine(), out indiceElegido) || indiceElegido < 0 || indiceElegido >= personajes.Count)
        {
            Console.WriteLine("Indice inválido. Por favor elige un numero entre 0 y 9:");
        }

        Personaje personajeUsuario = personajes[indiceElegido];
            Console.WriteLine("Personaje seleccionado:\n");
            Console.WriteLine($"Nombre: {personajeUsuario.Datos.Nombre}");
            Console.WriteLine($"Clase: {personajeUsuario.Datos.Clase}");
            Console.WriteLine($"Raza: {personajeUsuario.Datos.Raza}");
            Console.WriteLine($"Puntos de Vida: {personajeUsuario.Datos.PuntosDeVida}");
            Console.WriteLine("Características:");
            Console.WriteLine($"  Fuerza: {personajeUsuario.Caracteristicas.Fuerza}");
            Console.WriteLine($"  Destreza: {personajeUsuario.Caracteristicas.Destreza}");
            Console.WriteLine($"  Velocidad: {personajeUsuario.Caracteristicas.Velocidad}");

        List<Personaje> enemigos = new List<Personaje>(personajes);
        enemigos.RemoveAt(indiceElegido);//separar personaje usuario de los enemigos

        
        List<Personaje> ganadores = new List<Personaje>();//lista para guardar ganadores
        bool ganadorUsuario= true;
        Random random=new Random();

        foreach(var enemigo in enemigos)
        {
            if(!ganadorUsuario){
                break;
            }else{
                Console.WriteLine($"Pelea contra: {enemigo.Datos.Nombre}");
                Console.WriteLine("************************");
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
            Console.WriteLine("Características actualizadas del personaje elegido:");
            Console.WriteLine($"  Fuerza: {personajeUsuario.Caracteristicas.Fuerza}");
            Console.WriteLine($"  Destreza: {personajeUsuario.Caracteristicas.Destreza}");
            Console.WriteLine($"  Velocidad: {personajeUsuario.Caracteristicas.Velocidad}");
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
                Console.WriteLine("Comienza el combate:");
                ganadorUsuario=Combate.formulaCombate(personajeUsuario, enemigo);
                if(ganadorUsuario){
                     Console.WriteLine($"¡{personajeUsuario.Datos.Nombre} ha ganado la pelea contra {enemigo.Datos.Nombre}!");
                     ganadores.Add(personajeUsuario);
                }else{
                    Console.WriteLine($"¡{personajeUsuario.Datos.Nombre} ha perdido la pelea contra {enemigo.Datos.Nombre}!");
                    ganadores.Add(enemigo);
                }

            }
        }
        // Guardar los ganadores en "historial.json"
        manejadorDePersonajes.GuardarPersonajes(ganadores, nombreArchivoHistorial);
        Console.WriteLine("Ganadores guardados en historial.json");
    }

}
