using System.Collections.Generic;
using UnityEngine;

namespace Quackmageddon
{
    /// <summary>
    /// Universal object pooler. Implements Pool Pattern. Calls OnSpawn method of IPooledObject interface. Listens for Pause event to disable all pooled objects
    /// </summary>
    public class ObjectPooler : MonoBehaviour
    {
        #region Inspector fields
        [SerializeField]
        private List<Pool> poolsList;
        #endregion

        private Dictionary<SpawnType, Queue<GameObject>> poolDictionary;

        #region Life-cycle callbacks
        private void Start()
        {
            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.PauseSpawning, (foo) => { OnPause(); });

            poolDictionary = new Dictionary<SpawnType, Queue<GameObject>>();

            foreach (Pool pool in poolsList)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.poolSize; i++)
                {
                    GameObject newObject = Instantiate(pool.prefab);
                    newObject.SetActive(false);
                    objectPool.Enqueue(newObject);
                }

                poolDictionary.Add(pool.spawnType, objectPool);
            }
        }

        private void OnDestroy()
        {
            GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.PauseSpawning, (foo) => { OnPause(); });
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spawnType">string</param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public GameObject SpawnFromPool(SpawnType spawnType, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(spawnType))
            {
                Debug.Log("Warning: Pool with tag " + spawnType + " does not exist");
                return null;
            }

            GameObject objectToSpawn = poolDictionary[spawnType].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

            if (pooledObject != null)
            {
                pooledObject.OnSpawn();
            }

            poolDictionary[spawnType].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        /// <summary>
        /// Deactivates all pooled objects by iterating dictionary and then queue of a GameObjects
        /// </summary>
        private void OnPause()
        {
            foreach (Pool pool in poolsList)
            {
                Queue<GameObject>.Enumerator enumerator = poolDictionary[pool.spawnType].GetEnumerator();
                while (enumerator.MoveNext())
                {
                    enumerator.Current.SetActive(false);
                }
            }
        }

    }
}
