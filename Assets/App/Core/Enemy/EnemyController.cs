using UnityEngine;

namespace Core.Entitas
{
    [Order(300)]
    public class EnemyController : MonoBehaviour, IControllerEntity
    {
        [Inject] private PlayerController m_PlayerController;

        [SerializeField] private Unit m_Unit;
        [SerializeField] private HealthView m_HealthView;

        public Unit Unit => m_Unit;

        public void PreInit()
        {
            m_Unit.Initialize();

            m_Unit.SubscribeOnHealthChanged(m_HealthView.UpdateHealth);
        }

        public void Init()
        {

        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                m_PlayerController.Unit.TakeDamage(20, 0);
            }
        }

        private void OnDestroy()
        {
            m_Unit.UnsubscribeOnHealthChanged(m_HealthView.UpdateHealth);
        }
    }
}
