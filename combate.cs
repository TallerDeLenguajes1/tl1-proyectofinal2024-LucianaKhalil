using System;
using Proyecto;

public static class Combate{
    //metodo para simular el combate
    public static bool FormulaCombate(Personaje personajeUsuario, Personaje enemigo){
        Random random=new Random();
        int iniciativaUsuario = random.Next(1, 21);
        int iniciativaEnemigo = random.Next(1, 21);
        //tiradas de iniciativa
         Console.WriteLine("\n===============================");
        Console.WriteLine("¡El duelo comienza!");
        Console.WriteLine("===============================");
        Console.WriteLine("Tu enemigo y tu realizan una tirada de iniciativa con el dado D20");
        Console.WriteLine($"Iniciativa de {personajeUsuario.Datos.Nombre}: {iniciativaUsuario}");
        Console.WriteLine($"Iniciativa de {enemigo.Datos.Nombre}: {iniciativaEnemigo}");
        bool usuarioComienza = iniciativaUsuario > iniciativaEnemigo;
        Console.WriteLine(usuarioComienza ? $"{personajeUsuario.Datos.Nombre} comienza el combate." : $"{enemigo.Datos.Nombre} comienza el combate.");
         //Tiradas de combate
        int resultadoD20Usuario = random.Next(1, 21);
        int resultadoD20Enemigo = random.Next(1, 21);

        double poderUsuario = (personajeUsuario.Caracteristicas.Fuerza + personajeUsuario.Caracteristicas.Destreza + personajeUsuario.Caracteristicas.Velocidad) * (1 + (resultadoD20Usuario / 100.0));
        double poderEnemigo = (enemigo.Caracteristicas.Fuerza + enemigo.Caracteristicas.Destreza + enemigo.Caracteristicas.Velocidad) * (1 + (resultadoD20Enemigo / 100.0));

        Console.WriteLine("Presiona cualquier tecla para iniciar el combate...");
        Console.ReadKey();

        while(personajeUsuario.Datos.PuntosDeVida>0 && enemigo.Datos.PuntosDeVida>0){  
            if (usuarioComienza)
            {
                Console.WriteLine($"{personajeUsuario.Datos.Nombre} ataca a {enemigo.Datos.Nombre}");
                enemigo.Datos.PuntosDeVida-=(int)poderUsuario;//necesita casteo
                Console.WriteLine($"{enemigo.Datos.Nombre} tiene ahora {enemigo.Datos.PuntosDeVida} puntos de vida");
                 if (enemigo.Datos.PuntosDeVida <= 0)
                {
                    Console.WriteLine($"{personajeUsuario.Datos.Nombre} ha derrotado a {enemigo.Datos.Nombre}!");
                    return true;
                }

                Console.WriteLine("Presiona cualquier tecla para continuar...");
                Console.ReadKey();
                
            }else//turno del enemigo
            {
                Console.WriteLine($"{enemigo.Datos.Nombre} ataca a {personajeUsuario.Datos.Nombre}");
                personajeUsuario.Datos.PuntosDeVida-=(int)poderEnemigo;//necesita casteo
                Console.WriteLine($"{personajeUsuario.Datos.Nombre} tiene ahora {personajeUsuario.Datos.PuntosDeVida} puntos de vida");
                
                if (personajeUsuario.Datos.PuntosDeVida <= 0)
                {
                    Console.WriteLine($"{enemigo.Datos.Nombre} ha derrotado a {personajeUsuario.Datos.Nombre}!");
                    return false;
                }
            }
       
                 usuarioComienza = !usuarioComienza; // Cambiar turno
                Console.WriteLine("  ==================  ");
        }
        return personajeUsuario.Datos.PuntosDeVida > 0;
    }
}