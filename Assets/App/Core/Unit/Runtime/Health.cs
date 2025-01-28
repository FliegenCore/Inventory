using System;

namespace Core.Entitas
{
    public class Health
    {
        public event Action OnDead;
        public event Action<int> OnHealthChange;

        private HealthData m_HealthData;

        public void SetData(HealthData healthData)
        {
            m_HealthData = healthData;

            OnHealthChange?.Invoke(m_HealthData.CurrentHp);
        }

        public void Refresh()
        {
            m_HealthData.CurrentHp = m_HealthData.MaxHp;

            OnHealthChange?.Invoke(m_HealthData.CurrentHp);
        }

        public void TakeDamage(int damage)
        {
            if (m_HealthData.CurrentHp <= 0)
            {
                return;
            }

            m_HealthData.CurrentHp -= damage;

            if (m_HealthData.CurrentHp <= 0)
            {
                m_HealthData.CurrentHp = 0;
                OnDead?.Invoke();
            }

            OnHealthChange?.Invoke(m_HealthData.CurrentHp);
        }

        public void RestorHealth(int restore)
        {
            m_HealthData.CurrentHp += restore;

            if (m_HealthData.CurrentHp >= m_HealthData.MaxHp)
            {
                m_HealthData.CurrentHp = m_HealthData.MaxHp;
            }

            OnHealthChange?.Invoke(m_HealthData.CurrentHp);
        }
    }
}
