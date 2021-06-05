using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quackmageddon
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField]
        private short pointsForHittingEnemy = 50;

        [SerializeField]
        private short pointsForDestroyedEnemy = 250;

        private short currentScore = 0;

        #region Life-cycle callbacks

        void Start()
        {
            //GameplayEventsManager.Instance.RegisterListener(GameplayEventType.EnemyHit, (foo) => { OnEnemyHit(); });
            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.EnemyHit, OnEnemyHit);
            //GameplayEventsManager.Instance.RegisterListener(GameplayEventType.EnemyDestroyed, (foo) => { OnEnemyDestroyed(); });
            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.EnemyDestroyed, OnEnemyDestroyed);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
           // GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.EnemyHit, (foo) => { OnEnemyHit(); });
            GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.EnemyHit,  OnEnemyHit );
            //GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.EnemyDestroyed, (foo) => { OnEnemyDestroyed(); });
            GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.EnemyDestroyed, OnEnemyDestroyed);
        }
        #endregion

        #region Gameplay event listeners

        private void OnEnemyHit(short foo = 0)
        {
            Debug.Log("HIT");
            currentScore += pointsForHittingEnemy;

            GameplayEventsManager.Instance.DispatchEvent(GameplayEventType.ScoreUpdate, currentScore);
        }

        private void OnEnemyDestroyed(short foo = 0)
        {
            Debug.Log("DESTROY");
            currentScore += pointsForDestroyedEnemy;

            GameplayEventsManager.Instance.DispatchEvent(GameplayEventType.ScoreUpdate, currentScore);
        }
        #endregion

    }
}
