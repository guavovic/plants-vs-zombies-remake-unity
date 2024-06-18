using System.Collections.Generic;
using UnityEngine;

namespace PVZ.CardSlot
{
    public class CardSelectionGridSlotManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private SelectedCardSlotController selectedCardSlotcontrollerPrefab;
        [SerializeField] private Transform gridLayoutParent;
        [Space(10)][SerializeField] private List<SelectedCardSlotController> selectedCardSlotControllers;

        public List<SelectedCardSlotController> SelectedCardSlotControllers
        {
            get => selectedCardSlotControllers;
            private set => selectedCardSlotControllers = value;
        }

        public static CardSelectionGridSlotManager Instance { get; private set; }

        private void Awake() => Instance = this;

        private void Start() => InstantiateAvailableCards();

        public void InstantiateAvailableCards()
        {
            foreach (var card in gameManager.GeneralSettings.Cards)
            {
                var selectedCardSlotController = Instantiate(selectedCardSlotcontrollerPrefab, parent: gridLayoutParent.transform);
                selectedCardSlotController.ConfigureSlot(card);
                SelectedCardSlotControllers.Add(selectedCardSlotController);
            }
        }

        public SelectedCardSlotController GetSelectedController(Card card)
        {
            return selectedCardSlotControllers.Find(selectedCard => selectedCard.IsEqual(card));
        }
    }
}
