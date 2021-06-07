using UnityEngine;
using UnityEngine.UI;

namespace Quackmageddon
{
    /// <summary>
    /// 
    /// </summary>
    public class Gun : MonoBehaviour
    {
        #region Inspector's fields
        [SerializeField]
        private short damage = 10;

        [SerializeField]
        private float autoFireFrequency = 1f / 15f;

        [SerializeField]
        private float range = 100f;

        [SerializeField]
        private Camera camera;

        [SerializeField]
        private ParticleSystem flashEffect;

        [SerializeField]
        private GameObject impactEffectPrefab;

        [SerializeField]
        private float impactIntensity = 500f;

        [SerializeField]
        private Image aimPointer;

        [SerializeField]
        private Color32 idleAimColor;

        [SerializeField]
        private Color32 enemyPointingColor;

        [SerializeField]
        private Color32 beakPointingColor;
        #endregion

        #region Private fields
        private float autofireCooldown = 0f;
        private float impactEffectLifespan = 2f;
        #endregion

        #region Life-cycle callbacks 
        private void Update()
        {
            RaycastHit hit;
            bool isHittingAnything = Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range);

            if (isHittingAnything)
            {
                if (hit.collider.CompareTag(Enemy.EnemyTag))
                {
                    aimPointer.color = enemyPointingColor;
                }
                else if (hit.collider.CompareTag(Enemy.BeakshotTag))
                {
                    aimPointer.color = beakPointingColor;
                }
                else
                {
                    aimPointer.color = idleAimColor;
                }
            }

            if (Input.GetButton("Fire1"))
            {
                if (Time.time >= autofireCooldown)
                {
                    Shoot(isHittingAnything, hit);

                    autofireCooldown = Time.time + autoFireFrequency;
                }
            }
        }
        #endregion

        #region Private methods

        private void Shoot(bool isHittingAnything, RaycastHit hit)
        {
            flashEffect.Play();

            //RaycastHit hit;

           // if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
           if (isHittingAnything)
            {
                Enemy enemyController = hit.transform.GetComponent<Enemy>();

                if (enemyController != null)
                {
                    if (hit.collider.CompareTag(Enemy.BeakshotTag))
                    {
                        enemyController.NotifyBeakshot();
                    }

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
