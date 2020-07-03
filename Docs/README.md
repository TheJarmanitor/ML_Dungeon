# Documentación ML dungeon
**Tabla de contenido**
- [Introducción y Metas](#introducción-y-metas)
  - [IA en los videojuegos](#ia-en-los-videojuegos)
  - [Aprendizaje Reforzado](#aprendizaje-reforzado)
  - [Unity](#unity)
  - [Metas](#metas)
- [Creación del juego](#creación-del-juego)
  - [Base del juego](#base-del-juego)
  - [Implementación de Redes Neuronales](#implementación-de-redes-neuronales)
## Introducción y Metas
La intención de este proyecto es el conseguir un entendimiento de tanto el Software de desarrollo de videojuegos "Unity" y de su paquete de Agentes de aprendizaje automátizado "ML Agents", el cuál es potenciado por Python y Tensorflow. Esto con el fín de expandir este uso a productos tanto como en el campo de los videojuegos, como la simulación.


### IA en los videojuegos
- El desarrollo de la inteligencia artificial en los videojuegos existe casi desde la incepción de Este.

- Hay exponentes modernos del buen uso de la inteligencia artifical, como el mal uso

![](Media/grunt.gif)
- cuando se usa correctamente, puede ayudar a que la experiencia de juego sea más memorable
![](Media/Xeno.gif)
- También puede contribuir a la mala acogida de un título
![](Media/ACM.gif)
- O puede ser el resultado de métodos no muy bien percibidos(Cheating-AI)
![](Media/Akuma.gif)
![](Media/StarCraft.gif)

- El estándar del la inteligencia artifical ahora es el que todo Sea "Hard coded".
![](Media/Crunch.png)

- Con las tecnicas mostradas aquí, es posible conseguir Inteligencia artificial con buenas respuestas, y con menos esfuerzo por parte del programador.


### Aprendizaje reforzado
#### Breve explicación
- Al agente se le asigna un sistema de recompensas y es trabajo de este experimentar y maximimizar sus recompensas.
- El aprendizaje reforzado tiene un talón de aquiles: **El problema de recompensas escasas/sparce rewards** 
![](Media/Sparce.png)
#### Exploración por curiosidad


![](Media/montezuma.png)

- Es posible hacer aprendizaje reforzado incluso sin que el programador asigne recompensas extrinsicas
- Se aplica una segunda red neuronal que hace predicciones con base en lo hecho por el mismo agente.

### Unity
#### Introducción al software
- Motor de videojuegos sacado en 2005 exclusivo para Machintosh. ahora disponible de múltiples plataformas
- Altamente usado tanto por desarrolladores independientes, empresas pequeñas y grandes compañias
- Tiene versión gratis y distintos modelos de suscripción
![](Media/Unity.png)

#### Introducción al paquete ML Agents
- **Unity Machine Learning Agents Toolkit**
- En desarrollo desde 2017. Salida oficial en abril del 2020
- Herramienta para el desarrollo de agentes inteligentes para videojuegos y simulación
- Basado en tensorflow
- Usa archivos YAML para la creación de las redes
### Metas
Lo que se quiere lograr en este proyecto:
- Diseñar un terreno de aprendizaje
  - Diseñar uno o más terrenos para el agente haga su entrenamiento
  - Diseñar un agente Enemigo con un sistema de vida, control de ataque y movimiento
  - Diseñar un objetivo con control de vida y ataque.
- El entrenamiento del agente Enemigo
  - Lograr que el agente busque un objetivo y le haga daño
  - Lograr que el agente huya del objetivo en caso de que se encuentre en baja vida

## Creación del juego

### Base del Juego
#### Terreno de entrenamiento:
  - Uso de Baldosas para armar el campo
  ![](Media/baldosas.png)
  - Implementación de múltiples capas para hacer límites de juego
  ![](Media/walls.png)
  ![](Media/tilecollider.png)
#### Creación del agente:
  - Uso de animación en sprites
  
  <img src="Media/SlimeA.png" width="1024" height="64">
  
  ![](Media/slime_anim.png)
  
  - Sistema de Combate y movimiento
  
  ![](Media/target_prefab.png)
  ![](Media/slime_prefab.png)

### Implementación de Redes Neuronales
#### Variables extra utilizadas
  - Distancia entre objetos
  - grabador de vida actual
  - **Peligro**
#### Estados de aprendizaje:
  - Initialize
    - función que se llama al inicio de la escena/nivel
    - Se usa aquí para llamar a los scripts de Movimiento, combate y los sistemas de vida de cada elemento
  - Episode Begin
    - función que se llama al inicio de cada episodio
    - Reposicionamos a los objetos 
    - Reiniciamos la vida de los objetos, la distancia y el grabadaror de vida
  - Collect Observations
    - Función que obtiene y guarda los datos para la red neuronal.
    - Se toma en cuenta la posición tridimensional de cada objeto, más si se está en peligro o no
  - Action Received
    - Las acciones que el agente va a llevar a cabo y las recompensas que se van a dar
    - Movimiento del agente y ataque del agente
    - Si no está en peligro:
      - Si acorta distancia entre este y el objetivo: Recompensa pequeña
      - Si alarga distancia: Castigo pequeño
      - Si reduce la vida del objetivo: Recompensa
    - Si está en peligro:
      - Si alarga las distancias entre este y el objetivo: Recompensa pequeña
      - Si acorta distancia: Castigo pequeño
      - Si ataca: Castigo pequeño
      - Si Se aleja una cantidad en específico: Recompensa
  - Heuristic
    - Función para que el programador controle el agente. 
    - Necesario para detección de bugs
    - Control de movimiento y ataque
#### proceso de aprendizaje
![](Media/Dungeon_train.gif)

```sh
behaviors:
    Slime:
        trainer_type: ppo
        hyperparameters:
          batch_size: 128
          buffer_size: 2048
          learning_rate: 0.0003
          beta: 0.01
          epsilon: 0.2
          lambd: 0.95
          num_epoch: 4
          learning_rate_schedule: linear
        network_settings:
          normalize: false
          hidden_units: 128
          num_layers: 2
          vis_encode_type: simple
        reward_signals:
          extrinsic:
            gamma: 0.99
            strength: 1.0
          curiosity:
            gamma: 0.99
            strength: 0.02
            encoding_size: 256
            learning_rate: 0.0003
        keep_checkpoints: 5
        max_steps: 1000000
        time_horizon: 128
        summary_freq: 50000
        threaded: true
```
 ![](Graphs/Environment_Cumulative_Reward_(1).svg)
 ![](Graphs/Losses_Value_Loss.svg)

## Evaluación
- [Demostración](https://youtu.be/I7xTxLGFerk)
