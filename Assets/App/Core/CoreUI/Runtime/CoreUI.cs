using Core.Inventory;
using UnityEngine;

namespace Core.UI
{
    public class CoreUI : MonoBehaviour
    {
        [SerializeField] private InventoryView m_InventoryView;
        [SerializeField] private PlayerInput m_PlayerInput;
        [SerializeField] private BottomUI m_BottomUI;

        public InventoryView InventoryView => m_InventoryView;
        public PlayerInput PlayerInput => m_PlayerInput;
        public BottomUI BottomUI => m_BottomUI;
    }
}
