using TMPro;
using UnityEngine;

namespace Core.Weapons
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_BulletText;

        public void SetBulletText(string text)
        {
            m_BulletText.text = text;  
        }
    }
}

