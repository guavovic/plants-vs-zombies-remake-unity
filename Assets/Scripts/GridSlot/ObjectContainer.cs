using UnityEngine;

namespace PVZ.GridSlot
{
    public class ObjectContainer : MonoBehaviour
    {
        private MapGridSlotController _gridSlotController;

        public bool IsOccupied { get; set; } = false;
        public Object ObjectSet { get; set; }

        private void Start() => _gridSlotController = MapGridSlotController.Instance;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            _gridSlotController.CurrentGridSlot = this;
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            _gridSlotController.CurrentGridSlot = null;
        }
    }
}