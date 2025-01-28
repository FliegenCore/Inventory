using UnityEngine;

namespace Core.UI
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private CoreButton m_RestartButton;

        public CoreButton RestartButton => m_RestartButton; 
    }
}
