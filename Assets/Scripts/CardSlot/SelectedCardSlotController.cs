using UnityEngine.EventSystems;

namespace PVZ.CardSlot
{
    public class SelectedCardSlotController : CardSlot, IPointerClickHandler
    {
        private GameManager _gameManager;

        private void Start() => _gameManager = GameManager.Instance;

        public void OnPointerClick(PointerEventData eventData) => HandleOnClick();

        private void HandleOnClick()
        {
            if (GameStateController.IsChoosingCards && _gameManager.CardSelectionSlotManager.CanChooseNewCard())
            {
                if (_gameManager.CardSelectionSlotManager.HasEmptySlots() && !base.HasSelected)
                    _gameManager.CardSelectionSlotManager.AddCardInSlot(base.Card, this);
                else if (base.HasSelected)
                    _gameManager.CardSelectionSlotManager.RemoveCardForSlot(base.Card);
            }
        }
    }
}