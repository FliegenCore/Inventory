using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Entitas
{
    public class Health
    {
        public event Action OnDead;
        public event Action<int> OnHealthChange;

        private int m_CurrentHealth;
        private int m_MaxHealth;

        public Health(int maxHealth)
        {
            m_MaxHealth = maxHealth;
            m_CurrentHealth = maxHealth;

            OnHealthChange?.Invoke(m_CurrentHealth);
        }

        public void Refresh()
        {
            m_CurrentHealth = m_MaxHealth;

            OnHealthChange?.Invoke(m_CurrentHealth);
        }

        public void TakeDamage(int damage)
        {
            if (m_CurrentHealth <= 0)
            {
                return;
            }

            m_CurrentHealth -= damage;

            if (m_CurrentHealth <= 0)
            {
                OnDead?.Invoke();
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
