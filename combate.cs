using System;
using System.Threading;
using Proyecto;

public static class Combate
{
    private const int MAX_VIDA = 100; // inicializo en 100
    private const double BONIFICACION_USUARIO = 1.1; // 10% para el usuario
    private const double REDUCCION_DAÑO_ENEMIGO = 0.9; //reduccion 10% daño al usuario
    private const int CURACION_USUARIO = 20; // Cantidad de puntos de vida curados

    public static bool FormulaCombate(Personaje personajeUsuario, Personaje enemigo)
    {
        Random random = new Random();
        int iniciativaUsuario = (int)(random.Next(1, 21) * BONIFICACION_USUARIO);
        int iniciativaEnemigo = random.Next(1, 21);

        Console.WriteLine("\n===============================");
        Console.WriteLine("¡El duelo comienza!");
        Console.WriteLine("===============================");
        Console.WriteLine("Tu enemigo y tu realizan una tirada de iniciativa con el dado D20");
        Console.WriteLine($"Iniciativa de {personajeUsuario.Datos.Nombre}: {iniciativaUsuario}");
        Console.WriteLine($"Iniciativa de {enemigo.Datos.Nombre}: {iniciativaEnemigo}");

        bool usuarioComienza = iniciativaUsuario >= iniciativaEnemigo;
        Console.WriteLine(usuarioComienza ? $"{personajeUsuario.Datos.Nombre} comienza el combate." : $"{enemigo.Datos.Nombre} comienza el combate.");

        Console.WriteLine("Presiona cualquier tecla para iniciar el combate...\n");
        Console.ReadKey(true);

        while (personajeUsuario.Datos.PuntosDeVida > 0 && enemigo.Datos.PuntosDeVida > 0)
        {
            Console.Clear();
            MostrarEstadoCombate(personajeUsuario, enemigo);

            if (usuarioComienza)
            {
                RealizarAtaque(personajeUsuario, enemigo);
                usuarioComienza = !usuarioComienza;
            }
            else
            {
                RealizarAtaque(enemigo, personajeUsuario, true); // true para indicar que es un ataque enemigo
                usuarioComienza = !usuarioComienza;
            }

            if (enemigo.Datos.PuntosDeVida <= 0)
            {
                Console.WriteLine($"{personajeUsuario.Datos.Nombre} ha derrotado a {enemigo.Datos.Nombre}!");
                return true;
            }
            else if (personajeUsuario.Datos.PuntosDeVida <= 0)
            {
                Console.WriteLine($"{enemigo.Datos.Nombre} ha derrotado a {personajeUsuario.Datos.Nombre}!");
                return false;
            }

            // Ocasionalmente curar al usuario
            if (random.Next(1, 11) <= 3) // 30% de probabilidad de curación en cada ronda
            {
                personajeUsuario.Datos.PuntosDeVida += CURACION_USUARIO;
                personajeUsuario.Datos.PuntosDeVida = Math.Min(MAX_VIDA, personajeUsuario.Datos.PuntosDeVida);
                Console.WriteLine($"{personajeUsuario.Datos.Nombre} se ha curado {CURACION_USUARIO} puntos de vida!");
            }

            Console.WriteLine("Presiona cualquier tecla para continuar...");
            Console.ReadKey(true);
        }

        return personajeUsuario.Datos.PuntosDeVida > 0;
    }

    private static void RealizarAtaque(Personaje atacante, Personaje defensor, bool esEnemigo = false)
    {
        Random random = new Random();
        int resultadoD20 = random.Next(1, 21);
        double multiplicador = esEnemigo ? REDUCCION_DAÑO_ENEMIGO : BONIFICACION_USUARIO;
        double poder = (atacante.Caracteristicas.Fuerza + atacante.Caracteristicas.Destreza + atacante.Caracteristicas.Velocidad) * (1 + (resultadoD20 / 100.0)) * multiplicador;

        Console.WriteLine($"{atacante.Datos.Nombre} ataca a {defensor.Datos.Nombre}!");
        Thread.Sleep(500); // Simula una pausa para el ataque
        defensor.Datos.PuntosDeVida -= (int)poder;
        defensor.Datos.PuntosDeVida = Math.Max(0, defensor.Datos.PuntosDeVida); // Evitar vida negativa
        Console.WriteLine($"{defensor.Datos.Nombre} tiene ahora {defensor.Datos.PuntosDeVida} puntos de vida");
        Console.WriteLine("  ==================  ");
    }

    private static void MostrarEstadoCombate(Personaje personajeUsuario, Personaje enemigo)
    {
        Console.WriteLine("=== Estado del Combate ===");
        Console.WriteLine($"{personajeUsuario.Datos.Nombre} - Vida: {GenerarBarraDeVida(personajeUsuario.Datos.PuntosDeVida)} {personajeUsuario.Datos.PuntosDeVida}/{MAX_VIDA}");
        Console.WriteLine($"{enemigo.Datos.Nombre} - Vida: {GenerarBarraDeVida(enemigo.Datos.PuntosDeVida)} {enemigo.Datos.PuntosDeVida}/{MAX_VIDA}");
        Console.WriteLine("===========================");
    }

    private static string GenerarBarraDeVida(int puntosDeVida)
    {
        int totalBarras = 20; // Número total de barras ASCII que representarán la vida
        int barrasLlenas = (puntosDeVida * totalBarras) / MAX_VIDA; // Proporción de barras llenas

        return "[" + new string('|', barrasLlenas) + new string(' ', totalBarras - barrasLlenas) + "]";
    }
}
