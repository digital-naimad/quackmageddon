# Quackmageddon: The Duckie Hunting Game
> The main goal of the project is to develop a shooting survival game prototype using Unity Engine and the Cardboard SDK provided by Google company.

![popUpShort](https://user-images.githubusercontent.com/1534654/121075266-4389d500-c7d5-11eb-9998-3153f64728de.gif)

## Table of contents
* [Technologies Used](#technologies-used)
* [Features](#features)
* [Setup](#setup)
* [Inspirations](#inspirations)
* [Contact](#contact)

## Technologies Used
Project is developed with:
* Google VR SDK
* Unity Engine version 2018.4.35f1
* Low weight render pipeline
* Unity Shader Graph
* Unity UI & Animator
* Adobe Photoshop CS

## Features

* ### Custom Water Surface Shader
![image](https://user-images.githubusercontent.com/1534654/121080011-2b1cb900-c7db-11eb-8096-20703bc46f7f.png)

     - Implemented using Unity Shader Graph


* ### Custom HP Bar with his simplified variant
![Health Bar](https://user-images.githubusercontent.com/1534654/121071427-39b1a300-c7d0-11eb-8737-0c4da76c286b.gif)

    - To find in Assets/Prefabs/UI directory

![Health Bar prefabs](https://user-images.githubusercontent.com/1534654/121075941-138f0180-c7d6-11eb-8fb6-1ad3391fef11.png)

* ### Universal Object Pooler
![image](https://user-images.githubusercontent.com/1534654/121073987-8b0f6180-c7d3-11eb-80b2-7079f553a98b.png)

    - Implements **Pool Pattern**. 
    - Calls OnSpawn method of IPooledObject interface. 
    - Listening for Pause event to disable all pooled objects.

* ### Enemy Spawner
![Enemy Spawner Inspector](https://user-images.githubusercontent.com/1534654/121076435-b778ad00-c7d6-11eb-8126-ac3f14d4b647.png)

    - Spawns enemies using ObjectPooler. 
    - Listens to Pause and Resume events.

* ### Beakshot mechanic
![Beakshot score!](https://user-images.githubusercontent.com/1534654/121077544-083cd580-c7d8-11eb-9457-6921a8536db2.png)

    - The equivalent of a headshot for duckies, but much harder to achieve. 
    - Its indicating by change color of an aiming pointer, and also rewarded with extra score.

* ### MonoSingleton
    - Thread-safe implementation of Singleton Pattern for MonoBehaviour.
    - Based on dictionary instead of using FindObjectsOfType or creating GameObject during the game, which are very inefficient
   
```cs
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    #region Static fields
    protected static bool Quitting { get; private set; }

    private static readonly object Lock = new object();
    private static Dictionary<System.Type, MonoSingleton<T>> instancesDictionary;
    #endregion

    #region Static instance getter
    public static T Instance
    {
        get
        {
            lock (Lock)
            {
                if (instancesDictionary == null)
                {
                    instancesDictionary = new Dictionary<System.Type, MonoSingleton<T>>();
                }

                if (instancesDictionary.ContainsKey(typeof(T)))
                {
                    return (T)instancesDictionary[typeof(T)];
                }
                else
                {
                    return null;
                }
            }
        }
    }
    #endregion

    #region MonoBehaviour's inherited methods
    private void OnEnable()
    {
        lock (Lock)
        {
            if (instancesDictionary == null)
            {
                instancesDictionary = new Dictionary<System.Type, MonoSingleton<T>>();
            }

            if (instancesDictionary.ContainsKey(this.GetType()))
            {
                Destroy(this.gameObject);
            }
            else
            {
                instancesDictionary.Add(this.GetType(), this);

                DontDestroyOnLoad(gameObject);
            }
        }
    }
    #endregion
}
```

* ### Gameplay Event Manager
    - Implements **Observer Pattern**.
    - Using implementation of Singleton Pattern extending MonoBehaviour.
    - Based Dictionary containint list of Actions.
```cs 
public class GameplayEventsManager : MonoSingleton<GameplayEventsManager>
    {
        private Dictionary<string, List<Action<short>>> listenersDictionary;
```

* ### Gun controller script
![Gun controllers inspector](https://user-images.githubusercontent.com/1534654/121089059-df700c80-c7e6-11eb-8e64-1622dd4064be.png)

*Inspector's fields*

      * Uses raycasting technique to aiming targets and shooting
      
      ```cs private void Update()
        {
            RaycastHit hit;
            bool isHittingAnything = Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range);

            if (isHittingAnything)
            {
                if (hit.collider.CompareTag(Enemy.EnemyTag))
                {
                    aimPointer.color = enemyPointingColor;
                }
                
                (..)
                ```
                
                

* ### Health Manager

![Health manager](https://user-images.githubusercontent.com/1534654/121090763-54dcdc80-c7e9-11eb-93ad-cfc28d2e762b.png)

    - Manages  health points and also dispatches GameplayEventType.HealthUpdate event using GameplayEventManager. 
    - Includes auto-healing mechanism with cooldown.


![Health Manager in hierarchy](https://user-images.githubusercontent.com/1534654/121090824-6a520680-c7e9-11eb-93b5-ef8e6c2fab6d.png)

* ### Custom Particle Systems
    

## Setup
To run the game in the Unity Editor just go to Assets/Scenes directory and launch the Gameplay scene.

![Assets/Scenes/Gameplay.unity](https://user-images.githubusercontent.com/1534654/121062616-952a6380-c7c5-11eb-9881-2a5bb5898dab.png)

![Gameplay Scene Hierarchy](https://user-images.githubusercontent.com/1534654/121090426-e13acf80-c7e8-11eb-9a62-457e79c29046.png)

## Inspirations 
The idea for a gameplay setting is based on a real event...

> *On 10 January 1992, during a storm in the North Pacific Ocean close to the International Date Line twelve 40-foot (12-m) intermodal containers were washed overboard. One of these containers held 28,800 Floatees - that is a plastic rubber ducks. Some of the toys landed along Pacific Ocean shores, such as Hawaii.*

...but also on the typical programmer's everyday experience. 

> *In software engineering, <b>rubber duck debugging</b> is a method of debugging code. The name is a reference to a story in the book The Pragmatic Programmer in which a programmer would carry around a rubber duck and debug their code by forcing themselves to explain it, line-by-line, to the duck.*

So the idea for a backstory was that, rubber ducklings, driven to the extreme due to fact that for long years they had to listen about the problems of software developers. Developers, who did not even expect that they understood the words addressed to them. So it was a form of torture, as a result of which they gained some kind of self-awareness, but also began to cause them a desire to pay back for those decades of suffering of their species. 

And the high seas accident involving thousands of duckies was not an accident at all ... but only the beginning of the Quackmageddon!

Sources: [Friendly Floatees - Wikipedia](https://en.wikipedia.org/wiki/Friendly_Floatees), [Rubber duck debugging - Wikipedia]()

## Contact
*Created by* **Damian Śremski** - <d.sremski@gmail.com> - [linkedin.com/in/damianśremski](https://linkedin.com/in/damianśremski)

