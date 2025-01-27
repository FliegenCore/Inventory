using System;

namespace Core.Entitas
{
    public class Health
    {
        public event Action<int> OnHealthChange;

        private int m_CurrentHealth;
        private int m_MaxHealth;

        public int MaxHealth => m_CurrentHealth;

        public Health(int maxHealth)
        {
            m_MaxHealth = maxHealth;
            m_CurrentHealth = maxHealth;

            OnHealthChange?.Invoke(m_CurrentHealth);
        }

        public void TakeDamage(int damage)
        {
            m_CurrentHealth -= damage;

            if (m_CurrentHealth <= 0)
            {
                m_CurrentHealth = 0;
            }

            OnHealthChange?.Invoke(m_CurrentHealth);
        }

        public void RestorHealth(int restore)
        {
            m_CurrentHealth += restore;

            if (m_CurrentHealth >= m_MaxHealth)
            {
                m_CurrentHealth = m_MaxHealth;
            }

            OnHealthChange?.Invoke(m_CurrentHealth);
        }
    }
}
