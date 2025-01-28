using Core.Inventory;
using UnityEditor.Search;
using UnityEngine;

namespace Core.UI
{
    public class CoreUI : MonoBehaviour
    {
        [SerializeField] private InventoryView m_InventoryView;
        [SerializeField] private PlayerInput m_PlayerInput;
        [SerializeField] private BottomUI m_BottomUI;
        [SerializeField] private TopUI m_TopUI;

        public InventoryView InventoryView => m_InventoryView;
        public PlayerInput PlayerInput => m_PlayerInput;
        public BottomUI BottomUI => m_BottomUI;
        public TopUI TopUI => m_TopUI;
    }
}
