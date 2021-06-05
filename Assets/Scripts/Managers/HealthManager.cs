﻿using System.Collections;
using UnityEngine;

namespace Quackmageddon
{
    /// <summary>
    /// Manages  health points and also dispatches GameplayEventType.HealthUpdate event using GameplayEventManager. Includes auto-healing mechanism with cooldown
    /// </summary>
    public class HealthManager : MonoBehaviour
    {
        #region Inspector fields
        [SerializeField, Range(0, 100)]
        private short damagePointsForHit = 20;

        [SerializeField, Range(0, 100)]
        private short healPointsPerIteration = 1;

        [SerializeField]
        private float healingCooldownDuration = 1f;
        #endregion

        #region Private property & fields
        private short CurrentHealthPoints
        {
            get
            {
                return this.currentHealthPoints;
            }
            set
            {
                short previousHealthPoints = this.currentHealthPoints;
                this.currentHealthPoints = (short)Mathf.Clamp(value, 0, 100);

                if (previousHealthPoints != this.currentHealthPoints)
                {
                    GameplayEventsManager.Instance.DispatchEvent(GameplayEventType.HealthUpdate, currentHealthPoints);
                }
            }
        }

        private short currentHealthPoints;
        private float healingCooldownTimer = 0f;
        private bool isHealing = false;
        #endregion

        #region Life-cycle callbacks
        private void Start()
        {
            GameplayEventsManager.Instance.RegisterListener(GameplayEventType.PlayerHit, (foo) => { OnPlayerHit(); });

            CurrentHealthPoints = 100;
        }

        private void Update()
        {
            if (!isHealing)
            {
                if (healingCooldownTimer > 0f)
                {
                    healingCooldownTimer = Mathf.Max(0, healingCooldownTimer - Time.deltaTime);
                }

                if (healingCooldownTimer == 0f)
                {
                    isHealing = true;
                    StartCoroutine("HealPlayer");
                }
            }
        }

        private void OnDestroy()
        {
            GameplayEventsManager.Instance.UnregisterListener(GameplayEventType.PlayerHit, (foo) => { OnPlayerHit(); });
        }
        #endregion

        #region Private methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="foo">Unused, optional parameter placed here for compatibility with GameplayEventsManager</param>
        private void OnPlayerHit(short foo = 0)
        {
            CurrentHealthPoints -= damagePointsForHit;

            if (isHealing)
            {
                StopCoroutine("HealPlayer");
            }
            RestartHealingCooldown();
        }

        private void RestartHealingCooldown()
        {
            healingCooldownTimer = healingCooldownDuration;

            isHealing = false;
        }

        private IEnumerator HealPlayer()
        {
            for (short healthHelper = CurrentHealthPoints; healthHelper <= 100;  healthHelper += healPointsPerIteration)
            {
                CurrentHealthPoints = healthHelper;

                yield return new WaitForSeconds(.2f);
            }
            CurrentHealthPoints = 100;
            isHealing = false;
        }
        #endregion
    }
}