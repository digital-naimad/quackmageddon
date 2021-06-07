using UnityEngine;
using TMPro;

namespace Quackmageddon
{
    /// <summary>
    /// 
    /// </summary>
    public class HudManager : MonoBehaviour
    {
        #region Inspector fields
        [SerializeField]
        private HealthBar playerHealthBar;

        [SerializeField]
        private Animator healthIconAnimator;

        [SerializeField]
        private TMP_Text scoreLabel;

        [SerializeField]
        private Animator scoreLabelAnimator;

        [SerializeField]
        private Animator popUpAnimator;
        #endregion

        #region Life cycle callbacks
        private void Start()
        {
            playerHealthBar.SetMaxHealth(HealthManager.FullHealthValue);

            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.HealthUpdate, OnHealthUpdate);
            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.ScoreUpdate, OnScoreUpdate);
            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.PauseSpawning, OnSpawningPaused);
        }

        private void OnDestroy()
        {
            GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.HealthUpdate, OnHealthUpdate);
            GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.ScoreUpdate, OnScoreUpdate);
            GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.PauseSpawning, OnSpawningPaused);
        }
        #endregion

        #region Event listeners
        private void OnHealthUpdate(short currentHealth)
        {
            this.playerHealthBar.SetCurrentHealth(currentHealth);

            this.healthIconAnimator.Play("Heartbeat", -1, 0f);
        }

        private void OnScoreUpdate(short currentScore)
        {
            this.scoreLabel.text = currentScore.ToString().PadLeft(7, '0');

            this.scoreLabelAnimator.Play("ScoreLabelBump", -1, 0f);
        }

        private void OnSpawningPaused(short foo)
        {
            this.popUpAnimator.Play("popUpInOut", -1, 0f);
        }
        #endregion

    }
}
