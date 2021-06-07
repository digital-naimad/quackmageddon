using UnityEngine;

namespace Quackmageddon
{
    /// <summary>
    /// Enemy prefab controller implementing IPooledObject interface. 
    /// Controls enemy health value.  Contains own Canvas to display HP bar. Launches explosion effect and SFX.
    /// </summary>
    public class Enemy : MonoBehaviour, IPooledObject
    {
        #region Static members
        public static readonly string TagName = "Duckie";
        public static readonly string EnemyTag = "Enemy";
        public static readonly string BeakshotTag = "EnemyBeak";
        #endregion

        #region Inspector fields
        [SerializeField]
        private short initialHealthAmount = HealthManager.FullHealthValue;

        [SerializeField]
        private GameObject meshesContainer;

        [SerializeField]
        private Canvas ownedCanvas;

        [SerializeField]
        public BillboardView billboard;

        [SerializeField]
        public HealthBar healthBar;

        [SerializeField]
        private ExplosionController explosionEffect;

        [SerializeField]
        private AudioSource audioSource = null;
        #endregion

        #region Properties and fields
        /// <summary>
        /// Property.
        /// Note: caches rigid body component in private field
        /// </summary>
        public Rigidbody Rigidbody
        {
            get
            {
                if (this.rigidBody == null)
                {
                    this.rigidBody = GetComponent<Rigidbody>();
                }
                return this.rigidBody;
            }
        }

        private short CurrentHealthAmount
        {
            get
            {
                return currentHealthAmount;
            }
            set
            {
                currentHealthAmount = value;
                healthBar.SetCurrentHealth(value);
            }
        }

        private short currentHealthAmount = 0;
        private Rigidbody rigidBody = null;
        #endregion

        #region IPooledObject interface's methods implementation

        /// <summary>
        /// 
        /// </summary>
        public void OnSpawn()
        {
            healthBar.SetMaxHealth(initialHealthAmount);

            CurrentHealthAmount = initialHealthAmount;

            if (explosionEffect != null)
            {
                explosionEffect.gameObject.SetActive(false);
            }

            meshesContainer.SetActive(true);
            ownedCanvas.gameObject.SetActive(true);
            this.Rigidbody.angularVelocity = Vector3.zero;
            this.Rigidbody.useGravity = false;
        }

        #endregion

        #region Public methods
        /// <param name="healthPointsAmount">short</param>
        public void TakeDamage(short healthPointsAmount)
        {
            CurrentHealthAmount -= healthPointsAmount;

            if (currentHealthAmount > 0)
            {
                this.Rigidbody.useGravity = true;
                
                GameplayEventsManager.Instance.DispatchEvent(GameplayEventType.EnemyHit);
                SoundManager.Instance.PlayRandomSqueeze();
            }
            else
            {
                CurrentHealthAmount = 0;
                DoExplode();

                GameplayEventsManager.Instance.DispatchEvent(GameplayEventType.EnemyDestroyed);
            }
        }

        /// <summary>
        /// Dispatches EnemyBeakshot GameplayEvent and also triggers enemy explosion
        /// </summary>
        public void NotifyBeakshot()
        {
            GameplayEventsManager.Instance.DispatchEvent(GameplayEventType.EnemyBeakshot);

            DoExplode();
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
            ownedCanvas.gameObject.SetActive(false);
            this.Rigidbody.useGravity = false;

            SoundManager.Instance.PlayQuack(audioSource);
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
