using UnityEngine;

namespace Quackmageddon
{
    /// <summary>
    /// Stores and manages game results like a number of killed enemies, number of shots fired, total damage dealt, etc.
    /// Extends MonoSingleton. Listens for gameplay events
    /// </summary>
    public class ResultManager : MonoSingleton<ResultManager>
    {
        #region Inspector's preview & public fields
        [SerializeField]
        public short numberOfFiredBullets;

        [SerializeField]
        public short numberOfHits;

        [SerializeField]
        public short numberOfDestroyedEnemies;
        #endregion

        #region Private properties
        private short NumberOfFiredBullets
        {
            get
            {
                return _numberOfFiredBullets;
            }
            set
            {
                numberOfFiredBullets = value;
                _numberOfFiredBullets = value;
            }
        }

        private short NumberOfHits
        {
            get
            {
                return _numberOfHits;
            }
            set
            {
                numberOfHits = value;
                _numberOfHits = value;
            }
        }

        private short NumberOfDestroyedEnemies
        {
            get
            {
                return _numberOfDestroyedEnemies;
            }
            set
            {
                numberOfDestroyedEnemies = value;
                _numberOfDestroyedEnemies = value;
            }

        }
        #endregion

        #region Private fields
        private short _numberOfFiredBullets = 0;
        private short _numberOfHits = 0;
        private short _numberOfDestroyedEnemies = 0;
        #endregion

        #region Life-cycle callbacks
        private void Start()
        {
            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.BulletFired, OnBulletFired);
            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.EnemyHit, OnEnemyHit);
            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.EnemyDestroyed, OnEnemyDestroyed);
        }

        private void OnDestroy()
        {
            GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.BulletFired, OnBulletFired);
            GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.EnemyHit, OnEnemyHit);
            GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.EnemyDestroyed, OnEnemyDestroyed);
        }
        #endregion

        #region Gameplay Events Listeners
        private void OnBulletFired(short foo)
        {
            this.NumberOfFiredBullets++;
        }

        private void OnEnemyHit(short foo)
        {
            this.NumberOfHits++;
        }
        private void OnEnemyDestroyed(short foo)
        {
            this.NumberOfDestroyedEnemies++;
        }
        #endregion
    }
}
