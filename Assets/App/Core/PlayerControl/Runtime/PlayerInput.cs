using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.UI
{
    public class PlayerInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public event Action<IDragAndDrop> OnClick;
        public event Action<IDragAndDrop> OnStartDrag;
        public event Action OnEndDrag;
        public event Action OnDragging;

        private IDragAndDrop m_CurrentDragAndDrop;
        private Vector2 m_StartPositionCurrentObject;

        private bool m_WasDrag;

        public void OnPointerDown(PointerEventData eventData)
        {
            GameObject currentClickObject = eventData.pointerCurrentRaycast.gameObject;

            m_StartPositionCurrentObject = currentClickObject.transform.position;
            m_CurrentDragAndDrop = currentClickObject.GetComponent<IDragAndDrop>();
            OnStartDrag?.Invoke(m_CurrentDragAndDrop);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (m_CurrentDragAndDrop == null)
            {
                return;
            }

            m_CurrentDragAndDrop.SetPosition(m_StartPositionCurrentObject);
            OnEndDrag?.Invoke();

            if (!m_WasDrag)
            {
                OnClick?.Invoke(m_CurrentDragAndDrop);
            }

            m_WasDrag = false;
            m_CurrentDragAndDrop = null;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (m_CurrentDragAndDrop == null)
            {
                return;
            }

            if (!m_WasDrag)
            { 
                m_WasDrag = true;
            }

            OnDragging?.Invoke();
            m_CurrentDragAndDrop.Drag(eventData.position);
        }
    }
}
