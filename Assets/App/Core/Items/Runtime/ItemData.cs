using System;
using UnityEngine;

namespace Core.Inventory
{
    [Serializable]
    public class ItemData 
    {
        [SerializeField] private string m_Id;
        [SerializeField] private string m_Type;
        [SerializeField] private int m_StuckCount;
        [SerializeField] private float m_Weight;

        public string Id => m_Id;
        public string Type => m_Type;
        public int StuckCount => m_StuckCount;
        public float Weight => m_Weight;
    }
}
