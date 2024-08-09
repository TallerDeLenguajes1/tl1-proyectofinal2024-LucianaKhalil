# El archivo de las tormentas
En el devastado mundo de Roshar, donde la guerra y las tormentas han moldeado el destino de la humanidad, una batalla decisiva se avecina. Los Radiantes, antiguos guerreros venerados por su capacidad de canalizar el poder de las tormentas, han vuelto a levantarse, enfrentando a las implacables fuerzas de Odium. En este momento crucial, tú encarnas a un Radiante, el último baluarte de esperanza en un mundo desgarrado por la guerra. Tu misión es clara: enfrentarte al campeón de Odium y sus nueve temibles seguidores en un duelo épico. La verdadera desolación está a punto de desatarse. El destino de Roshar está en tus manos.

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

### Sistema de Combate

El sistema de combate en "El Archivo de las Tormentas" está diseñado para enfrentar al personaje del jugador contra un enemigo en un duelo dinámico. A continuación, se detallan los aspectos clave del sistema de combate:

#### Mecánica de Combate

1. **Iniciativa:**
   - Cada combate comienza con una tirada de iniciativa usando un dado de 20 caras (D20). La iniciativa del jugador se incrementa en un 10% para reflejar la bonificación del usuario, mientras que la iniciativa del enemigo se calcula normalmente.
   - El personaje con mayor iniciativa comienza el combate.

2. **Turnos de Combate:**
   - Los personajes se alternan en sus turnos para atacar. Si el jugador empieza, atacará primero; de lo contrario, el enemigo atacará primero.
   - Cada ataque se basa en una tirada de D20 y se aplica un multiplicador al daño según si el atacante es el jugador o el enemigo. El daño recibido se reduce en un 10% para el jugador.

3. **Ataques y Daño:**
   - Los ataques se calculan sumando la Fuerza, Destreza y Velocidad del atacante, ajustados por el resultado de la tirada del dado.
   - El daño se aplica al personaje defensor, reduciendo sus puntos de vida.

4. **Curación:**
   - Existe una probabilidad del 30% en cada ronda de combate de que uno de los combatientes reciba una curación aleatoria. La curación puede beneficiar tanto al jugador como al enemigo, con una cantidad que varía entre 10 y 30 puntos de vida.

5. **Estado del Combate:**
   - Se muestra el estado actual de ambos combatientes, incluyendo sus puntos de vida en una barra gráfica visual.

6. **Resultado del Combate:**
   - El combate continúa hasta que uno de los combatientes se queda sin puntos de vida. El combatiente con puntos de vida restantes es el ganador.
   - Si el jugador gana, se cura completamente y se restaura su fuerza original. Si el jugador pierde, el combate termina con la derrota del jugador.

### Ejemplo de Código

```csharp
public static bool FormulaCombate(Personaje personajeUsuario, Personaje enemigo)
{
    // Código de combate...
}
