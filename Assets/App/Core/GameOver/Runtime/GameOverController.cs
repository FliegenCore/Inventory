using Assets;
using Common;
using Core.Common;
using Core.Entitas;
using Core.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Gameplay
{
    [Order(99999)]
    public class GameOverController : MonoBehaviour, IControllerEntity
    {
        [Inject] private PlayerController m_PlayerController;
        [Inject] private EventManager m_EventManager;
        [Inject] private AssetLoader m_AssetLoader;
        [Inject] private PopupController m_PopupController;
        [Inject] private SaveSystem m_SaveSystem;

        private GameOverView m_GameOverView;

        public void PreInit()
        {
            var gameOverAsset = m_AssetLoader.LoadGameObjectSync<GameOverView>("GameOverWindow");

            m_GameOverView = m_AssetLoader.InstantiateSync(gameOverAsset, m_PopupController.PopupTransform);
            m_GameOverView.gameObject.SetActive(false);
            m_PlayerController.Unit.SubscribeOnDead(ShowGameOver);
        }

        public void Init()
        {
            m_GameOverView.RestartButton.OnClick += RestartGame;
        }

        private void ShowGameOver()
        {
            m_EventManager.TriggerEvenet<EnableFadeSignal>();
            m_GameOverView.gameObject.SetActive(true);
            
        }

        private async void RestartGame()
        {
            await m_SaveSystem.DeleteSave();

            SceneManager.LoadScene(0);
        }

        private void OnDestroy()
        {
            m_PlayerController.Unit.UnsubscribeOnDead(ShowGameOver);
        }
    }
}
