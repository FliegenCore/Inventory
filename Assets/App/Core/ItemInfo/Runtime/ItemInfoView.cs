using Core.Common;
using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class ItemInfoView : MonoBehaviour
    {
        [SerializeField] private CoreButton m_UseButton;
        [SerializeField] private CoreButton m_DeleteButton;
        [SerializeField] private Image m_ItemImage;
        [SerializeField] private Image m_Icon;
        [SerializeField] private TMP_Text m_UseText;
        [SerializeField] private TMP_Text m_WeightText;
        [SerializeField] private TMP_Text m_ItemName;
        [SerializeField] private TMP_Text m_StatText;

        private EventManager m_EventManager;

        public void Initialize(EventManager eventManager)
        {
            m_EventManager = eventManager;
        }

        public void SubscribeOnUseButton(Action action)
        {
            m_UseButton.OnClick += action;
        }

        public void UnsubscribeOnUseButton(Action action)
        {
            m_UseButton.OnClick -= action;
        }

        public void SubscribeOnDeleteButton(Action action)
        {
            m_DeleteButton.OnClick += action;
        }

        public void UnsubscribeOnDeleteButton(Action action)
        {
            m_DeleteButton.OnClick -= action;
        }

        public ItemInfoView SetStatText(string text)
        {
            m_StatText.text = text;

            return this;
        }

        public ItemInfoView SetItemName(string nme)
        { 
            m_ItemName.text = nme;
            return this;
        }

        public ItemInfoView SetUseText(string useText)
        {
            m_UseText.text = useText;
            return this;
        }

        public ItemInfoView SetItemSprite(Sprite sprite)
        {
            m_ItemImage.sprite = sprite;

            return this;
        }

        public ItemInfoView SetIcon(Sprite sprite)
        {
            m_Icon.sprite = sprite;

            return this;
        }

        public ItemInfoView SetWeight(float weight)
        {
            m_WeightText.text = $"{weight} สร";

            return this;
        }

        public ItemInfoView Enable()
        {
            gameObject.SetActive(true);
            return this;
        }

        public ItemInfoView Disable()
        {
            gameObject.SetActive(false);
            return this;
        }

      
    }
}
