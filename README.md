# volleyAI Unity3d

<p align="center">
  <img src="https://github.com/user-attachments/assets/37ce6fbc-ba6a-4245-a3eb-871f721805a3" width=700/>
</p>

## Introducción
El objetivo de este proyecto fue desarrollar un escenario de entrenamiento en el que dos agentes autónomos se enfrentan en un partido de volleyball, utilizando ML-Agents de Unity. A través de este entorno, se buscó entrenar a los agentes para que aprendieran a competir entre sí de manera progresiva.

Durante el proceso de desarrollo, surgieron diversos desafíos, como la tendencia de los agentes a maximizar recompensas explotando situaciones no previstas. Sin embargo, mediante la implementación de un set de recompensas más completo y ajustado, se logró finalmente el comportamiento esperado, logrando que los agentes aprendieran de forma efectiva.

## Aprendizaje por refuerzo: Algunos conceptos
El aprendizaje por refuerzo es una técnica de enseñanza que implica recompensar los comportamientos positivos y castigar los negativos. Consta de un aprendizaje empírico, por lo que el agente informático está en constante búsqueda de aquellas decisiones que le premien y a la par evita aquellos caminos que, por experiencia propia, son penalizados.

Antes de continuar, debemos poner en común ciertos conceptos:
- Agente: La entidad que aprende y toma decisiones.
- Entorno: El contexto en el que el agente interactúa y recibe retroalimentación.
- Observaciones: Los distintos elementos que componen el entorno, de los cuales el agente aprende. Se corresponden a la capa de entrada de la red neuronal.
- Acciones: Las opciones que el agente puede tomar en respuesta a las observaciones del entorno. Corresponde a la capa de salida de la red neuronal.
- Recompensas: La retroalimentación positiva o negativa que el agente recibe por sus acciones.

<p align="center">
  <img src="https://github.com/user-attachments/assets/80e44c79-5062-4b92-b1f4-52752ccc045c" width="650" />
</p>

## Diseño
### Entorno

<p align="center">
  <img src="https://github.com/user-attachments/assets/899668fe-2177-4154-aa1c-c7571f710016" width="700" />
</p>

### Observaciones
- Posiciones propias y del oponente
- Posición de la pelota
- Vector velocidad de la pelota
- Distancia a la red y su altura


<p align="center">
  <img src="https://github.com/user-attachments/assets/580d4835-412d-4f58-b864-04a06fe04538" width="800" />
</p>
  
### Acciones

### Feedback: Recompensas y penalizaciones
Recompensas:
- Tocar la pelota
- Pasar la pelota por encima de la red
- Ganar un punto (máxima recompensa)

Penalizaciones:
- Tocar las paredes
- Perder un punto (máxima penalización)

Además, en función de qué tan cerca o lejos de la pelota esté el agente, éste recibirá una recompensa o penalización respectivamente. Esto es útil para que el agente aprenda a manterse cerca de la pelota.

<p align="center">
  <img src="https://github.com/user-attachments/assets/2823b089-0520-419c-afe3-be84a522c01e" width="800" />
</p>

## Problemas surgidos y soluciones ideadas

### Problema #1: Retención de la pelota
Como el agente obtiene recompensas por tocar la pelota, trata de retenerla encima suyo para acumular recompensas.
Solución: Cuando el agente retiene la pelota, automáticamente pierde el punto que estaba en juego.

### Problema #2: Toques infinitos
Con el problema anterior solucionado, si bien los agentes ya no buscan retener la pelota dejándola apoyada encima suyo, sí intentan mantenerla en su dominio realizando toques infinitos, con el fin de acumular recompensas.
Solución: El agente pierde el punto tras 5 toques consecutivos.

### Problema #3: Agente no sabe acercarse a la pelota
Al inicio de cada punto, los agentes y la pelota estaban en una posición fija, pero esto provocaba que los agentes no aprendan a acercarse a la pelota, sino a esa posición en particular, ocasionándole dificultades de seguir a la pelota en otras posiciones.
Solución: Posición inicial random (dentro de los límites posibles) de agentes y pelota

### Problema #4: Aprendizaje lento
El entrenamiento de la red se volvió un proceso bastante extenso debido a que los agentes estaban aprendiendo muy lento
Solución: Para solucionarlo se empleó un entrenamiento de mútiples agentes en paralelo. También se utilizó la técnica de aprendizaje por imitación que permite a los agentes indicarles manualmente qué comportamiento deben realizar de modo que no tienen que hacer este descubrimiento desde cero por su cuenta, lo cual ahorra tiempo.

<p align="center">
  <img src="https://github.com/user-attachments/assets/d3361216-a8ad-4c8c-aaf9-8bc207c3c1b3" width="600" />
</p>

## Entrenamiento con distintas arquitecturas de red
De acuerdo a algunas arquitecturas probadas, se decidió utilizar una red de 10 capas ocultas con 128 neuronas cada una dado a que es la que mejor resultados arrojó.
<p align="center">
  <img src="https://github.com/user-attachments/assets/38e2bbab-b1b6-4a58-ab11-04b0aff88529" width="700" />
</p>

## Resultado final

Inicio             |  Resultado Final
:-------------------------:|:-------------------------:
|<p float=left align="middle"><img src="https://github.com/jmdieguez/unity-ai/raw/main/ml-agents/docs/images/volley_inicio.gif" width="380" /></p>  | <p float=left align="middle"><img src="https://github.com/jmdieguez/unity-ai/raw/main/ml-agents/docs/images/volley_final.gif" width="380" /></p>|

## Extra
Este proyecto forma parte de otro proyecto más grande donde se útilizó ML-Agents de Unity en cuatro distintos escenarios.
Para más información, el proyecto completo se encuentra [acá](https://github.com/jmdieguez/unity-ai)
