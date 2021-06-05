using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quackmageddon
{
    /// <summary>
    /// 
    /// </summary>
    public class Enemy : MonoBehaviour, IPooledObject
    {
        #region Static members

        public static readonly string TagName = "Duckie";

        #endregion

        #region properties & fields

        [SerializeField]
        private float initialHealthAmount = 100f;

        [SerializeField]
        private GameObject meshesContainer;

        [SerializeField]
        private ExplosionController explosionEffect;
       
        /// <summary>
        /// Property.
        /// Note: caches rigid body component in private field
        /// </summary>
        public Rigidbody Rigidbody
        {
            get
            {
                if (this._rigidBody == null)
                {
                    this._rigidBody = GetComponent<Rigidbody>();
                }
                return this._rigidBody;
            }
        }


        private float currentHealthAmount = 0f;

        private Rigidbody _rigidBody = null;

        #endregion

        #region IPooledObject interface's methods implementation

        /// <summary>
        /// 
        /// </summary>
        public void OnSpawn()
        {
            currentHealthAmount = initialHealthAmount;

            this.Rigidbody.angularVelocity = Vector3.zero;

            meshesContainer.SetActive(true);

            if (explosionEffect != null)
            {
                explosionEffect.gameObject.SetActive(false);
            }

            this.Rigidbody.useGravity = false;
        }

        #endregion

        #region Public methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(float amount)
        {
            currentHealthAmount -= amount;

            if (currentHealthAmount > 0f)
            {
                this.Rigidbody.useGravity = true;

                GameplayEventsManager.Instance.DispatchEvent(GameplayEventType.EnemyHit);
            }
            else
            {
                currentHealthAmount = 0f;
                DoExplode();

                GameplayEventsManager.Instance.DispatchEvent(GameplayEventType.EnemyDestroyed);
            }
        }
        #endregion

        #region Private methods
        private void DoExplode()
        {
            if (explosionEffect != null)
            {
                explosionEffect.gameObject.SetActive(true);
                explosionEffect.LaunchAnimations();
            }

            meshesContainer.SetActive(false);
            this.Rigidbody.useGravity = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            this.Rigidbody.useGravity = true;

            if (collision.collider.CompareTag("Base"))
            {
                DoExplode();

                GameplayEventsManager.Instance.DispatchEvent(GameplayEventType.PlayerHit);
            }
        }
        #endregion
    }
}
