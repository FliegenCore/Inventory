using Core.Entitas;
using System;

namespace Core.Items
{
    public class Medkit : IItem
    {
        private const int m_HealthRestoreCount = 50;

        private Action<int> m_Callback;
        private IHealthRestorer m_HealthRestorer;

        public Medkit(IHealthRestorer healthRestorer, Action<int> callback)
        {
            m_Callback = callback;
            m_HealthRestorer = healthRestorer;
        }

        public string GetAddedInfo()
        {
            return $"{m_HealthRestoreCount}";
        }

        public void Use()
        {
            m_HealthRestorer.RestoreHealth(m_HealthRestoreCount);
            m_Callback?.Invoke(1);
        }
    }
}
