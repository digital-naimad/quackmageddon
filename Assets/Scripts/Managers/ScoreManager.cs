using UnityEngine;

namespace Quackmageddon
{
    /// <summary>
    /// Communicates changes in current score value by dispatching GameplayEventType.ScoreUpdate and also listening to EnemyHit and Destroyed events
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        #region Inscpector fields
        [SerializeField]
        private short pointsForHittingEnemy = 50;

        [SerializeField]
        private short pointsForDestroyedEnemy = 250;

        [SerializeField]
        private short pointsForBeakshot = 1000;
        #endregion

        #region Private properties and fields
        private short CurrentScore
        {
            get
            {
                return currentScore;
            }
            set
            {
                currentScore = (short)Mathf.Max(value, 0);
                
                GameplayEventsManager.Instance.DispatchEvent(GameplayEventType.ScoreUpdate, currentScore);
            }
        }
        private short currentScore = 0;
        #endregion

        #region Life-cycle callbacks
        private void Start()
        {
            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.EnemyHit, OnEnemyHit);
            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.EnemyDestroyed, OnEnemyDestroyed);
            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.EnemyBeakshot, OnEnemyHeadshot);
        }

        private void OnDestroy()
        {
           GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.EnemyHit, OnEnemyHit );
           GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.EnemyDestroyed, OnEnemyDestroyed);
           GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.EnemyBeakshot, OnEnemyHeadshot);
        }
        #endregion

        #region Gameplay event listeners
        private void OnEnemyHit(short foo = 0)
        {
            CurrentScore += pointsForHittingEnemy;
        }

        private void OnEnemyDestroyed(short foo = 0)
        {
            CurrentScore += pointsForDestroyedEnemy;
        }

        private void OnEnemyHeadshot(short foo = 0)
        {
            CurrentScore += pointsForBeakshot;
        }
        #endregion
    }
}
