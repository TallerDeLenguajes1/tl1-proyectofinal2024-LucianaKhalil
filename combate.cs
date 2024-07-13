using System;
using Proyecto;

public static class Combate{
    //metodo para simular el combate
    public static bool formulaCombate(Personaje personajeUsuario, Personaje enemigo){
        Random random=new Random();
         int iniciativaUsuario = random.Next(1, 21);
        int iniciativaEnemigo = random.Next(1, 21);
        Console.WriteLine("Tu enemigo y tu realizan una tirada de iniciativa con el dado D20");
        Console.WriteLine($"Iniciativa de {personajeUsuario.Datos.Nombre}: {iniciativaUsuario}");
        Console.WriteLine($"Iniciativa de {enemigo.Datos.Nombre}: {iniciativaEnemigo}");
        bool usuarioComienza = iniciativaUsuario < iniciativaEnemigo;
        Console.WriteLine(usuarioComienza ? $"{personajeUsuario.Datos.Nombre} comienza el combate." : $"{enemigo.Datos.Nombre} comienza el combate.");
         //Tiradas de combate
        int resultadoD20Usuario = random.Next(1, 21);
        int resultadoD20Enemigo = random.Next(1, 21);

        double poderUsuario = (personajeUsuario.Caracteristicas.Fuerza + personajeUsuario.Caracteristicas.Destreza + personajeUsuario.Caracteristicas.Velocidad) * (1 + (resultadoD20Usuario / 100.0));
        double poderEnemigo = (enemigo.Caracteristicas.Fuerza + enemigo.Caracteristicas.Destreza + enemigo.Caracteristicas.Velocidad) * (1 + (resultadoD20Enemigo / 100.0));

         if (usuarioComienza)
        {
            return poderUsuario > poderEnemigo;
        }
        else
        {
            return poderEnemigo > poderUsuario;
        }
    }
}