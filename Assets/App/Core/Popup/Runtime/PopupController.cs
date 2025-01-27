using Core.Common;
using UnityEngine;

namespace Core.UI
{
    [Order(-100)]
    public class PopupController : MonoBehaviour, IControllerEntity
    {
        private Fade m_Fade;

        [Inject] private EventManager m_EventManager;

        public void PreInit()
        {
            m_Fade = FindObjectOfType<Fade>();
            
            m_Fade.Disable();
        }

        public void Init()
        {
            m_EventManager.Subscribe<EnableFadeSignal>(this, m_Fade.Enable);
            m_EventManager.Subscribe<DisableFadeSignal>(this, m_Fade.Disable);
        }

        private void OnDestroy()
        {
            m_EventManager.Unsubscribe<EnableFadeSignal>(this);
            m_EventManager.Unsubscribe<DisableFadeSignal>(this);
        }
    }
}
