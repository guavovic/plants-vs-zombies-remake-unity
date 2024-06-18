using PVZ.CardSlot;
using PVZ.GeneralSettings;
using PVZ.GridSlot;
using PVZ.Level;
using PVZ.UI;
using UnityEngine;

namespace PVZ
{
    public sealed class GameManager : MonoBehaviour
    {
        public GeneralSettingsScriptableObject GeneralSettings { get; private set; }
        public LevelManager LevelManager { get; private set; }
        public UIManager UIManager { get; private set; }
        public CardSelectionGridSlotManager CardSelectionGridSlotManager { get; private set; }
        public CardSelectionSlotManager CardSelectionSlotManager { get; private set; }
        public CollectableSunManager CollectableSunManager { get; private set; }
        public MapGridSlotController MapGridSlotController { get; private set; }

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            GeneralSettings = GeneralSettingsHelper.GetGeneralSettings();
        }

        private void Start() => SetInstances();

        private void SetInstances()
        {
            LevelManager = LevelManager.Instance;
            UIManager = UIManager.Instance;
            CardSelectionGridSlotManager = CardSelectionGridSlotManager.Instance;
            CardSelectionSlotManager = CardSelectionSlotManager.Instance;
            CollectableSunManager = CollectableSunManager.Instance;
            MapGridSlotController = MapGridSlotController.Instance;
        }

        public void StartGame()
        {
            UIManager.UIPanelsController.TogglePanelsState(PanelType.MainMenu, PanelType.Gameplay);
        }
    }
}