using Core.UI;
using System;

namespace Core.Weapons
{
    public class WeaponCoreButton : CoreButton
    {
        public new event Action<int> OnClick;

        private int m_Index;

        public void Initialize(int index)
        {
            m_Index = index;
        }

        protected override void Click()
        {
            OnClick?.Invoke(m_Index);
        }
    }
}
