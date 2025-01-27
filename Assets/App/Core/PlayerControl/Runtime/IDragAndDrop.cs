using UnityEngine;

namespace Core.UI
{
    public interface IDragAndDrop
    {
        void Drag(Vector2 position);
        void SetPosition(Vector2 position);
    }
}
