using PVZ.Assets.Scripts.CardSlot;
using PVZ.GridSlot;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PVZ.CardSlot
{
    public sealed class CardSelectionSlotController : CardSlot, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        private GameManager _gameManager;
        private ObjectDragging _objectDragInstance;
        private CollisionHandler _collisionHandler;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _collisionHandler = new CollisionHandler();
        }

        private void FixedUpdate() => ControlVisibilityOpacity(GameStateController.IsPlayable);

        public void OnPointerClick(PointerEventData eventData) => HandlerOnPointerClick();

        public void OnDrag(PointerEventData eventData) => HandlerOnDrag(eventData);

        public void OnPointerDown(PointerEventData eventData) => HandlerOnPointerDown(eventData);

        public void OnPointerUp(PointerEventData eventData) => HandlerOnPointerUp();

        private void HandlerOnPointerClick()
        {
            if (GameStateController.IsChoosingCards && _gameManager.CardSelectionSlotManager.CanChooseNewCard())
                _gameManager.CardSelectionSlotManager.RemoveCardForSlot(Card);
        }

        private void HandlerOnDrag(PointerEventData eventData)
        {
            if (GameStateController.IsPlayable && base.CanBuy())
            {
                _objectDragInstance.transform.localPosition = InputTouchUtilities.GetTouchPosition(_gameManager.MapGridSlotController.CanvasRect, eventData);
                _collisionHandler.Ray(_objectDragInstance);
            }
        }

        private void HandlerOnPointerDown(PointerEventData eventData)
        {
            if (GameStateController.IsPlayable && base.CanBuy())
            {
                var objectDragging = Instantiate(base.Card.ObjectDrag, Vector3.zero, Quaternion.identity, _gameManager.MapGridSlotController.CardParent);
                _objectDragInstance = objectDragging.GetComponent<ObjectDragging>();
                _objectDragInstance.Setup(this, base.Card.CardType);
                _objectDragInstance.transform.localPosition = InputTouchUtilities.GetTouchPosition(_gameManager.MapGridSlotController.CanvasRect, eventData);
            }
        }

        private void HandlerOnPointerUp()
        {
            if (GameStateController.IsPlayable && base.CanBuy())
            {
                HandlerCurrentGridSlot();
                _collisionHandler.DesactivePreviouslyActivatedObjects();
                Destroy(_objectDragInstance.gameObject);
            }
        }

        private void HandlerCurrentGridSlot()
        {
            if (_gameManager.MapGridSlotController.CurrentGridSlot != null)
            {
                switch (base.Card.CardType)
                {
                    case CardType.Seed:
                        if (_gameManager.MapGridSlotController.CurrentGridSlot.IsOccupied)
                            return;

                        _gameManager.MapGridSlotController.PlaceObjectInGridSlot(base.Card);
                        _gameManager.CollectableSunManager.UseCollectedSuns(base.Card.Price);
                        StartReloadingAnimtion();
                        break;

                    case CardType.Shovel:
                        _gameManager.MapGridSlotController.RemoveObjectFromGridSlot();
                        break;
                }
            }
        }
    }
}