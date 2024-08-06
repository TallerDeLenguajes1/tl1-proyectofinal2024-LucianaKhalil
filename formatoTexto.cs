using System;
using System.Threading.Tasks;

public static class ConsolaFormato
{
    // Método para mostrar texto con efecto de escritura
    public static async Task EscribirConEfecto(string texto, int velocidadMs = 20)
    {
        foreach (char c in texto)
        {
            Console.Write(c);
            await Task.Delay(velocidadMs); // Espera entre cada carácter
        }
        Console.WriteLine(); // Salto de línea al final del texto
    }

    // Método para mostrar texto con efecto de escritura en color
    public static async Task EscribirConEfecto(string texto, ConsoleColor color, int velocidadMs = 10)
    {
        Console.ForegroundColor = color;
        await EscribirConEfecto(texto, velocidadMs);
        Console.ResetColor();
    }
}