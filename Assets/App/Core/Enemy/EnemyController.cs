using Core.UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.Entitas
{
    [Order(-1)]
    public class EnemyController : MonoBehaviour, IControllerEntity
    {
        [Inject] private PlayerController m_PlayerController;
        [Inject] private CoreUIController m_CoreUIController;

        [SerializeField] private Unit m_Unit;
        [SerializeField] private HealthView m_HealthView;

        private int m_CurrentAttackIndex;

        public Unit Unit => m_Unit;

        public void PreInit()
        {
            m_Unit.Initialize();

            m_Unit.SubscribeOnHealthChanged(m_HealthView.UpdateHealth);
            m_Unit.SubscribeOnDead(Refresh);
        }

        public void Init()
        {
            m_CoreUIController.CoreUI.BottomUI.ShootButton.OnClick += ShootOnPlayer;
        }

        private void Refresh()
        {
            m_Unit.RefreshHP();
        }

        private void ShootOnPlayer()
        {
            if (m_CurrentAttackIndex > 0)
            {
                m_CurrentAttackIndex--;
            }
            else
            {
                m_CurrentAttackIndex++;
            }

            m_PlayerController.Unit.TakeDamage(15, m_CurrentAttackIndex);
        }

        private void OnDestroy()
        {
            m_Unit.UnsubscribeOnHealthChanged(m_HealthView.UpdateHealth);
            m_Unit.UnsubscribeOnDead(Refresh);
            m_CoreUIController.CoreUI.BottomUI.ShootButton.OnClick -= ShootOnPlayer;
        }
    }
}
