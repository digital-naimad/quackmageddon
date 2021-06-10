using UnityEngine;

namespace Quackmageddon
{
    /// <summary>
    /// Spawns enemy using ObjectPooler. Listens to Pause and Resume events
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        #region Inspector fields

        /// <summary>
        /// Preview in inspector
        /// </summary>
        public short NumberOfSpawnedEnemies
        {
            get
            {
                return numberOfSpawnedEnemies;
            }
        }

        [SerializeField]
        private ObjectPooler pooler;

        [SerializeField]
        private Camera cameraToLookAt;

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

        [SerializeField]
        private short numberOfEnemiesToSpawnBoss = 10;
        #endregion

        private Vector3 positionToFaceTo = new Vector3(0, 0, 0);
        private bool isPaused = false;
        private short numberOfSpawnedEnemies = 0;

        /// <summary>
        /// Helper getter
        /// </summary>
        private SpawnType NextEnemyType
        {
            get
            {
                return pooler.IsBossAvailable
                    && ResultManager.Instance.numberOfDestroyedEnemies > 0
                    && ResultManager.Instance.numberOfDestroyedEnemies % numberOfEnemiesToSpawnBoss == 0 
                    ? SpawnType.Boss
                    : SpawnType.RushingEnemy;
            }
        }

        #region Life-cycle callbacks
        private void Start()
        {
            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.PauseSpawning, (foo) => { OnPause(); });
            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.ResumeSpawning, (foo) => { OnResume(); });
            StartSpawning();
        }

        private void OnDestroy()
        {
            GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.ResumeSpawning, (foo) => { OnResume(); });
            GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.PauseSpawning, (foo) => { OnPause(); });
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
            if (isPaused)
            {
                return;
            }

            float randomAngleInDegrees = Random.Range(0f, 360f);
            float randomAngleInRadians = randomAngleInDegrees * Mathf.Deg2Rad;

            Vector3 spawnPosition = new Vector3(
                spawnHorizontalDistance * Mathf.Cos(randomAngleInRadians), 
                spawnAltitude,
                spawnHorizontalDistance * Mathf.Sin(randomAngleInRadians)
            );

            Vector3 direction = positionToFaceTo - spawnPosition;
            Vector3 initialForce = direction.normalized * Random.Range(minThrowPower, maxThrowPower);

            this.numberOfSpawnedEnemies++;

            GameObject spawnedEnemyObject = pooler.SpawnFromPool(
                NextEnemyType,
                spawnPosition,
                Quaternion.LookRotation(direction)
            );

            if (spawnedEnemyObject != null)
            {
                Enemy spawnedEnemyController = spawnedEnemyObject.GetComponent<Enemy>();
                spawnedEnemyController.Rigidbody.velocity = initialForce;
                spawnedEnemyController.billboard.cameraToLookAt = cameraToLookAt;
            }
        }
        #endregion

        #region Event listeners
        private void OnPause()
        {
            isPaused = true;
        }

        private void OnResume()
        {
            isPaused = false;
        }
        #endregion
    }
}