using PVZ.CardSlot;
using UnityEngine;

namespace PVZ.GridSlot
{
    public class ObjectDragging : MonoBehaviour
    {
        public Object ObjecDragging { get; private set; }
        public CardType CardType { get; private set; }

        public void Setup(Object objecDragging, CardType cardType)
        {
            ObjecDragging = objecDragging;
            CardType = cardType;
        }
    }
}