using UnityEngine;
using UnityEngine.UI;

namespace Quackmageddon
{
    /// <summary>
    /// Health bar prefab controller class
    /// </summary>
    public class HealthBar : MonoBehaviour
    {
        #region Inspector's fields
        [SerializeField]
        private Slider progressSlider;

        [SerializeField]
        private Image fillmentImage;

        [SerializeField]
        private Gradient progressGradient;

        [SerializeField]
        private float smoothing = 3f;
        #endregion

        private float targetValue = HealthManager.FullHealthValue;

        #region Life cycle callbacks
        private void Update()
        {
            if (progressSlider.value != targetValue)
            {
                RefreshSliderFillment();
            }
        }
        #endregion

        #region Public methods

        /// <param name="healthLimit">Defines value of fullfilled HP bar</param>
        public void SetMaxHealth(int healthLimit)
        {
            progressSlider.maxValue = healthLimit;
            progressSlider.value = healthLimit;

            fillmentImage.color = progressGradient.Evaluate(1f);
        }

        /// <param name="targetValue">Health points to set</param>
        public void SetCurrentHealth(short targetValue)
        {
            this.targetValue = targetValue;
        }
        #endregion

        private void RefreshSliderFillment()
        {
            progressSlider.value = Mathf.Lerp(progressSlider.value, targetValue, smoothing * Time.deltaTime);
            fillmentImage.color = progressGradient.Evaluate(progressSlider.normalizedValue);
        }
    }
}
