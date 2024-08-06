# El archivo de las tormentas
En el mundo devastado de Roshar, el destino de la humanidad pende de un hilo. La guerra entre los Radiantes, antiguos guerreros imbuidos de poderes sagrados y temidos por su habilidad para manipular las tormentas, han regresado y las fuerzas de Odium ha alcanzado su punto crítico en una serie de batallas decisivas. En esta encrucijada decisiva, tú asumes el papel de un Radiante, dispuesto a luchar por la supervivencia de tu pueblo y el futuro de un mundo devastado por la guerra: un duelo contra el campeón de Odium y sus nueve despojos. La verdadera desolación se acerca.

## Descripción del Juego

En este juego de rol basado en combates por turnos, tu objetivo es luchar contra una serie de enemigos utilizando tiradas de dados y características únicas de cada personaje. El juego te permite elegir un personaje Radiante y luchar contra diez enemigos en una secuencia de batallas, donde la estrategia y la suerte juegan roles cruciales.

## Características del Juego


* Generación de personajes: Personajes aleatorios con diferentes clases, razas y características.
* Combates por turnos: Tiradas de dados que afectan el resultado de las peleas.
* Sistema de combate dinámico: Decisiones estratégicas pueden cambiar el curso de la batalla.
* Registro de ganadores: Guardado en un archivo de historial.
## Fórmula de Combate

La fórmula de combate diseñada utiliza una combinación de tiradas de dados y características de los personajes para determinar el resultado de un enfrentamiento. 

### Tiradas de Iniciativa

- Se realiza una tirada de dado D20 para determinar quién comienza el combate.
- `iniciativaUsuario` y `iniciativaEnemigo` se generan aleatoriamente entre 1 y 20.
- El personaje con la tirada más alta comienza el combate.

### Cálculo del Poder

- El poder de ataque de cada personaje se calcula sumando sus características (Fuerza, Destreza, Velocidad) y multiplicándolo por un factor que depende del resultado de una tirada de dado D20.
- La fórmula para calcular el poder de ataque es:

```csharp
double poderUsuario = (personajeUsuario.Caracteristicas.Fuerza + personajeUsuario.Caracteristicas.Destreza + personajeUsuario.Caracteristicas.Velocidad) * (1 + (resultadoD20Usuario / 100.0));
double poderEnemigo = (enemigo.Caracteristicas.Fuerza + enemigo.Caracteristicas.Destreza + enemigo.Caracteristicas.Velocidad) * (1 + (resultadoD20Enemigo / 100.0));
