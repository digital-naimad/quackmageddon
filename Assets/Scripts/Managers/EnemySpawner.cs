using UnityEngine;

namespace Quackmageddon
{
    /// <summary>
    /// 
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        #region Inspector fields

        [SerializeField]
        private ObjectPooler pooler;

        [SerializeField]
        private float spawnIntervalInSeconds = 3f;

        [SerializeField]
        private float spawnHorizontalDistance = 10f;

        [SerializeField]
        private float spawnAltitude = 5f;

        [SerializeField]
        private float minThrowPower = 5f;

        [SerializeField]
        private float maxThrowPower = 10f;

        #endregion

        private Vector3 positionToFaceTo = new Vector3(0,0,0);

        #region Life-cycle callbacks

        private void Awake()
        {
            
        }

        void Start()
        {
            StartSpawning();
        }

        void Update()
        {
           
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delay">Optional delay time</param>
        public void StartSpawning(float delay = 0f)
        {
            InvokeRepeating("SpawnEnemy", delay, spawnIntervalInSeconds);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SpawnEnemy()
        {
            float randomAngleInDegrees = Random.Range(0f, 360f);
            float randomAngleInRadians = randomAngleInDegrees * Mathf.Deg2Rad;

            Vector3 spawnPosition = new Vector3(
                spawnHorizontalDistance * Mathf.Cos(randomAngleInRadians), 
                spawnAltitude,
                spawnHorizontalDistance * Mathf.Sin(randomAngleInRadians)
            );

            Vector3 direction = positionToFaceTo - spawnPosition;
            Vector3 initialForce = direction.normalized * Random.Range(minThrowPower, maxThrowPower);

            GameObject spawnedEnemyObject = pooler.SpawnFromPool(
                Enemy.TagName,
                spawnPosition,
                Quaternion.LookRotation(direction)
            );

            Enemy spawnedEnemyController = spawnedEnemyObject.GetComponent<Enemy>();
            spawnedEnemyController.Rigidbody.velocity = initialForce;
        }

        #endregion

    }
}