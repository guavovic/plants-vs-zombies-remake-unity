using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace PVZ.CardSlot
{
    public class CardSelectionSlotManager : MonoBehaviour
    {
        [SerializeField] private CardSelectionSlotController cardSelectionSlotControllerPrefab;
        [SerializeField] private Transform selectableCardsGridParent;
        [SerializeField] private Transform disabledSelectableCardsParent;
        [Space(10)][SerializeField] private List<CardSelectionSlotController> cardSelectionSlotControllers;

        private const int _maximumNumberOfSlots = 7;
        private GameManager _gameManager;
        private CardSelectionMoveAnimations _cardSelectionMoveAnimations;
        private bool _selectedCard;

        public List<CardSelectionSlotController> CardSelectionSlotControllers
        {
            get => cardSelectionSlotControllers;
            private set => cardSelectionSlotControllers = value;
        }

        public static CardSelectionSlotManager Instance { get; private set; }

        private void Awake() => Instance = this;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            cardSelectionSlotControllers = new List<CardSelectionSlotController>(_maximumNumberOfSlots);
            _cardSelectionMoveAnimations = new CardSelectionMoveAnimations(_gameManager);
        }

        public async void AddCardInSlot(Card card, SelectedCardSlotController selectedCardSlotController)
        {
            _selectedCard = true;

            var slotToAdd = Instantiate(cardSelectionSlotControllerPrefab, parent: selectableCardsGridParent);
            cardSelectionSlotControllers.Add(slotToAdd);

            await Task.Delay(100);

            slotToAdd.ConfigureSlot(card);
            selectedCardSlotController.ToggleCardSlotState();

            await _cardSelectionMoveAnimations.MoveTo(slotToAdd, Target.Initial);
            _gameManager.UIManager.UICardSelectionController.PlayButton.interactable = !HasEmptySlots();

            _selectedCard = false;
        }

        public async void RemoveCardForSlot(Card card)
        {
            _selectedCard = true;

            var slotToRemove = cardSelectionSlotControllers.Find(slot => slot.IsEqual(card));

            if (slotToRemove != null)
            {
                slotToRemove.transform.SetParent(disabledSelectableCardsParent);
                await _cardSelectionMoveAnimations.MoveTo(slotToRemove, Target.Final);
                cardSelectionSlotControllers.Remove(slotToRemove);
                Destroy(slotToRemove.gameObject);
            }

            _gameManager.CardSelectionGridSlotManager.SelectedCardSlotControllers.Find(gridSlot => gridSlot.IsEqual(card)).ToggleCardSlotState();
            _gameManager.UIManager.UICardSelectionController.PlayButton.interactable = !HasEmptySlots();

            _selectedCard = false;
        }

        public bool HasEmptySlots() => cardSelectionSlotControllers.Count <= _maximumNumberOfSlots;

        public bool CanChooseNewCard() => !_selectedCard;

        public void ResetSlots() { foreach (var card in cardSelectionSlotControllers) card.ResetSlot(); }

        public CardSelectionSlotController GetSelectionSlot(Card card) => CardSelectionSlotControllers.Find(selectionSlot => selectionSlot.IsEqual(card));
    }
}