using UnityEngine.EventSystems;
using UnityEngine;

namespace PVZ.CardSlot
{
    public static class InputTouchUtilities
    {
        public static Vector3 GetTouchPosition(RectTransform canvasRect, PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, eventData.enterEventCamera, out Vector2 localMousePos);
            return localMousePos;
        }
    }
}