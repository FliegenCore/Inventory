using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Entitas
{
    public class Unit : MonoBehaviour, IDamageble, IHealthRestorer
    {
        private Health m_Health;

        private List<BodyPart> m_BodyParts;

        private BodyPart m_Head;
        private BodyPart m_Body;

        public BodyPart Head => m_Head;
        public BodyPart Body => m_Body;

        public void Initialize()
        {
            m_Health = new Health(100);
            m_Head = new BodyPart();
            m_Body = new BodyPart();
            m_BodyParts = new List<BodyPart>
            {
                m_Head, m_Body
            };
        }

        public void RefreshHP()
        {
            m_Health.Refresh();
        }

        public void TakeDamage(int damage, int bodyIndex)
        {
            m_Health.TakeDamage(damage - m_BodyParts[bodyIndex].ArmorCount);
        }

        public void RestoreHealth(int restore)
        {
            m_Health.RestorHealth(restore);
        }

        public void SubscribeOnHealthChanged(Action<int> action)
        {
            m_Health.OnHealthChange += action;
        }

        public void UnsubscribeOnHealthChanged(Action<int> action)
        {
            m_Health.OnHealthChange -= action;
        }

        public void SubscribeOnDead(Action action)
        {
            m_Health.OnDead += action;
        }

        public void UnsubscribeOnDead(Action action)
        {
            m_Health.OnDead -= action;
        }
    }
}
