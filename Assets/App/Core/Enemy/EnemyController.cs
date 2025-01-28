using Common.Utils;
using Common;
using Core.UI;
using UnityEngine;

namespace Core.Entitas
{
    [Order(-1)]
    public class EnemyController : MonoBehaviour, IControllerEntity
    {
        [Inject] private PlayerController m_PlayerController;
        [Inject] private CoreUIController m_CoreUIController;
        [Inject] private SaveSystem m_SaveSystem;

        [SerializeField] private Unit m_Unit;
        [SerializeField] private HealthView m_HealthView;

        private HealthData m_HealthData;
        private int m_CurrentAttackIndex;

        public Unit Unit => m_Unit;

        public void PreInit()
        {
            Result<HealthData> healthDataRes = m_SaveSystem.Load<HealthData>("EnemyHealthData");

            if (healthDataRes.IsExist)
            {
                m_HealthData = healthDataRes.Object;
            }
            else
            {
                m_HealthData = new HealthData();
                m_HealthData.CurrentHp = 100;
                m_HealthData.MaxHp = 100;
            }

            m_Unit.Initialize();

            m_Unit.SubscribeOnHealthChanged(m_HealthView.UpdateHealth);
            m_Unit.SubscribeOnDead(Refresh);
        }

        public void Init()
        {
            m_Unit.SetHealthData(m_HealthData);

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

        private void OnApplicationQuit()
        {
            m_SaveSystem.Save<HealthData>(m_HealthData, "EnemyHealthData");
        }

        private void OnDestroy()
        {
            m_Unit.UnsubscribeOnHealthChanged(m_HealthView.UpdateHealth);
            m_Unit.UnsubscribeOnDead(Refresh);
            m_CoreUIController.CoreUI.BottomUI.ShootButton.OnClick -= ShootOnPlayer;
        }
    }
}
