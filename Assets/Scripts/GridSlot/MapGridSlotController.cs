using PVZ.CardSlot;
using UnityEngine;

namespace PVZ.GridSlot
{
    public class MapGridSlotController : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasRect;
        [SerializeField] private Transform cardParent;
        public RectTransform CanvasRect { get => canvasRect; set => canvasRect = value; }
        public Transform CardParent { get => cardParent; set => cardParent = value; }
        public ObjectContainer CurrentGridSlot { get; set; }
        public static MapGridSlotController Instance { get; set; }

        private void Awake() => Instance = this;

        public void PlaceObjectInGridSlot(Card card)
        {
            CurrentGridSlot.ObjectSet = Instantiate(card.ObjectInGame, CurrentGridSlot.transform);
            CurrentGridSlot.IsOccupied = true;
        }

        public void RemoveObjectFromGridSlot()
        {
            Destroy(CurrentGridSlot.ObjectSet);
            CurrentGridSlot.IsOccupied = false;
        }
    }
}
