using TMPro;
using UnityEngine;

namespace Core.UI
{
    public class ArmorSlotView : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_ArmorText;

        public void SetArmorText(int count)
        { 
            m_ArmorText.text = $"{count}";
        }
    }
}
