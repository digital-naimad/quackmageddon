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
        #endregion

        #region Life cycle callbacks
        private void Start()
        {
            playerHealthBar.SetMaxHealth(HealthManager.FullHealthValue);

            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.HealthUpdate, OnHealthUpdate);
            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.ScoreUpdate, OnScoreUpdate);
        }

        private void OnDestroy()
        {
            GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.HealthUpdate, OnHealthUpdate);
            GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.ScoreUpdate, OnScoreUpdate);
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
        #endregion

    }
}
