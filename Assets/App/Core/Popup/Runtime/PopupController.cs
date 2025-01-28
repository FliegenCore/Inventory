using Assets;
using Core.Common;
using UnityEngine;

namespace Core.UI
{
    [Order(-99999)]
    public class PopupController : MonoBehaviour, IControllerEntity
    {
        [Inject] private EventManager m_EventManager;
        [Inject] private AssetLoader m_AssetLoader;

        private PopupUI m_PopupUI;
        private Fade m_Fade;

        public Transform PopupTransform => m_PopupUI.transform;

        public void PreInit()
        {
            var popupAsset = m_AssetLoader.LoadGameObjectSync<PopupUI>("PopupUI");
            m_PopupUI = m_AssetLoader.InstantiateSync<PopupUI>(popupAsset, null);

            var fadeAsset = m_AssetLoader.LoadGameObjectSync<Fade>("Fade");
            m_Fade = m_AssetLoader.InstantiateSync<Fade>(fadeAsset, m_PopupUI.transform);
            
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
