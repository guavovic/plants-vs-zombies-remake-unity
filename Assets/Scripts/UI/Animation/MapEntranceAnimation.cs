using UnityEngine;

namespace PVZ.UI.AnimationScripts
{
    public sealed class MapEntranceAnimation : MonoBehaviour
    {
        public enum States
        {
            True,
            False
        }

        private GameManager _gameManager;
        private Animator _animator;
        private const string _initialAnimationTrigger = "InitialAnimation";
        private const string _finalAnimationTrigger = "FinalAnimation";

        public static MapEntranceAnimation Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            _animator = GetComponent<Animator>();
        }

        private void Start() => _gameManager = GameManager.Instance;

        private void OnEnable() => PlayInitialAnimation();

        public void PlayInitialAnimation() => _animator.SetTrigger(_initialAnimationTrigger);

        public void PlayFinalAnimation() => _animator.SetTrigger(_finalAnimationTrigger);

        public void SetChoosingCardsState(States state) => GameStateController.IsChoosingCards = state == States.True;

        public void SetPlayableState(States state) => GameStateController.IsPlayable = state == States.True;

        public void GenerateLawnMowers() => _gameManager.LevelManager.LawnMowersSpawner.StartLawnMowerGeneration();

        public void StartLevel() => _gameManager.LevelManager.StarNewtevel();

        public void InitCardSlots()
        {
            foreach (var controllers in _gameManager.CardSelectionSlotManager.CardSelectionSlotControllers)
                controllers.StartReloadingAnimtion();
        }
    }
}