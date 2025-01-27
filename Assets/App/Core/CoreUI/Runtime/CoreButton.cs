using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class CoreButton : MonoBehaviour
    {
        public event Action OnClick;

        [SerializeField] private Button m_Button;

        private void Awake()
        {
            m_Button.onClick.AddListener(Click);
        }

        protected virtual void Click()
        {
            OnClick?.Invoke();
        }
    }
}
