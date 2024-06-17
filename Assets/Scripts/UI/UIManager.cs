using UnityEngine;

namespace PVZ.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("--- PANELS ---")]
        [SerializeField] private UIPanelsController panelsController;

        [Header("--- CARD SELECTION CONTROLLER ---")]
        [SerializeField] private UICardSelectionController cardSelectionController;

        public Transform ProjetelParent;

        public UIPanelsController UIPanelsController { get => panelsController; set => panelsController = value; }
        public UICardSelectionController UICardSelectionController { get => cardSelectionController; set => cardSelectionController = value; }
        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}