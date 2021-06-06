using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class HealthBar : MonoBehaviour
{
    #region Inspector's fields
    [SerializeField]
    private Slider progressSlider;

    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private Gradient progressGradient;

    [SerializeField]
    private short maxHealth = 100;

    [SerializeField]
    private short currentHealth = 0;
    #endregion

    public void SetMaxHealth(int value)
    {
        progressSlider.maxValue = value;
        progressSlider.value = value;

        fillImage.color = progressGradient.Evaluate(1f);
    }

    public void SetCurrentHealth(int value)
    {
        progressSlider.value = value;
        fillImage.color = progressGradient.Evaluate(progressSlider.normalizedValue);
    }

}
