using System.Collections;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class ExplosionController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem fireParticles = null;

    [SerializeField]
    private ParticleSystem debrisParticles = null;

    [SerializeField]
    private ParticleSystem sparksParticles = null;

    public float animationLifespan = 3f;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="delay"></param>
    public void LaunchAnimations(float delay = 0f)
    {
        gameObject.SetActive(true);

        if (fireParticles != null)
        {
            fireParticles.Play();
        }

        if (debrisParticles != null)
        {
            debrisParticles.Play();
        }

        if (sparksParticles != null)
        {
            sparksParticles.Play();
        }

        if (gameObject.active)
        {
            StartCoroutine(HideWithDelay(animationLifespan));
        }
    }

    IEnumerator HideWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        gameObject.SetActive(false);
    }
}
