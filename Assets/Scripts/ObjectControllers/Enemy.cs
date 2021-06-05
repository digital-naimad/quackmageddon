using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quackmageddon
{
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

        #region Life-cycle callbacks



        void Update()
        {

        }

        #endregion

        #region IPooledObject interface's methods implementation

        /// <summary>
        /// 
        /// </summary>
        public void OnSpawn()
        {
            currentHealthAmount = initialHealthAmount;

            meshesContainer.SetActive(true);

            this.Rigidbody.useGravity = false;
        }

        #endregion

        #region Public methods

        public void OnCollisionEnter(Collision collision)
        {
           this.Rigidbody.useGravity = true;
        }

        public void TakeDamage(float amount)
        {
            currentHealthAmount -= amount;

            if (currentHealthAmount <= 0f)
            {
                DoExplode();
            }
            else
            {
                this.Rigidbody.useGravity = true;
            }
        }

        private void DoExplode()
        {
            if (explosionEffect != null)
            {
                explosionEffect.LaunchAnimations();
            }

            meshesContainer.SetActive(false);
        }

        #endregion
    }
}
