using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Entitas
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_HealthText;
        [SerializeField] private Slider m_Slider;

        public void Initialize(int maxHealth)
        {
            m_Slider.maxValue = maxHealth;
        }

        public void UpdateHealth(int health)
        {
            m_HealthText.text = $"{health}";

            m_Slider.value = health;
        }
    }
}
