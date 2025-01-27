using Core.Common;
using UnityEngine;

namespace Core.UI
{
    [Order(-999)]
    public class CoreUIController : MonoBehaviour, IControllerEntity
    {
        [Inject] private EventManager m_EventManager;

        private CoreUI m_CoreUI;

        public CoreUI CoreUI => m_CoreUI;

        public void PreInit()
        {
            m_CoreUI = FindObjectOfType<CoreUI>();
        }

        public void Init()
        {

        }

        private void OnDestroy()
        {
            
        }
    }
}
