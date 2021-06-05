
using UnityEngine;

namespace Quackmageddon
{
    public class Gun : MonoBehaviour
    {
        #region Inspector's fields

        [SerializeField]
        private float damage = 10f;

        [SerializeField]
        private float autoFireFrequency = 1f / 15f;

        [SerializeField]
        private float range = 100f;

        [SerializeField]
        private Camera fpsCamera;

        [SerializeField]
        private ParticleSystem flashEffect;

        [SerializeField]
        private GameObject impactEffectPrefab;

        [SerializeField]
        private float impactIntensity = 500f;

        #endregion

        #region Private fields

        private float autofireCooldown = 0f;
        private float impactEffectLifespan = 2f;

        #endregion

        #region Life-cycle callbacks 
        void Update()
        {
            // if (Input.GetButtonDown("Fire1"))
            if (Input.GetButton("Fire1"))
            {
                if (Time.time >= autofireCooldown)
                {
                    Shoot();

                    autofireCooldown = Time.time + autoFireFrequency;
                }
            }
        }
        #endregion

        #region Private methods

        private void Shoot()
        {
            flashEffect.Play();

            RaycastHit hit;

            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
            {
                Enemy enemyController = hit.transform.GetComponent<Enemy>();

                if (enemyController != null)
                {
                    enemyController.TakeDamage(damage);
                    enemyController.Rigidbody.AddForce(-hit.normal * impactIntensity);
                }

                SpawnImpactEffect(hit);
            }
        }

        private void SpawnImpactEffect(RaycastHit hit)
        {
            GameObject impactEffect = Instantiate(impactEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactEffect, impactEffectLifespan);
        }

        #endregion
    }
}
