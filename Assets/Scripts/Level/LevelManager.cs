using PVZ.Enemy;
using PVZ.LawnMower;
using PVZ.UI.AnimationScripts;
using System.Collections.Generic;
using UnityEngine;

namespace PVZ.Level
{
    public sealed class LevelManager : MonoBehaviour
    {
        [Header("--- ENEMY SPAWN POINTS ---")]
        [SerializeField] private List<Transform> enemySpawnPoints;

        [Header("--- LAWNMOWERS ---")]
        [SerializeField] private LawnMowersSpawner lawnMowersSpawner;

        private GameManager _gameManager;
        private ScenarioController _scenarioController;
        private LevelController _levelController;
        private EnemySpawner _enemySpawner;
        private MapEntranceAnimation _entranceAnimation;
        public LawnMowersSpawner LawnMowersSpawner => lawnMowersSpawner;
        public static LevelManager Instance { get; private set; }

        private void Awake() => Instance = this;

        private void Start()
        {
            _gameManager = GameManager.Instance;
            _scenarioController = new ScenarioController(_gameManager.GeneralSettings.ScenarioSettings);
            _levelController = new LevelController(_scenarioController.CurrentScenarioSettings.LevelsSettings);
            _entranceAnimation = MapEntranceAnimation.Instance;
        }

        private void Update()
        {
            if (GameStateController.IsPlayable && _enemySpawner != null)
            {
                if (_enemySpawner.AllWavesCompleted)
                {
                    HandleLevelCompleted();
                    return;
                }

                _enemySpawner.Run();

                if (_levelController.CurrentLevelSettings.HasSunsFall)
                    _gameManager.CollectableSunManager.SpawnCollectableSuns();
            }
        }

        private void HandleLevelCompleted()
        {
            _gameManager.CollectableSunManager.ResetValues();
            _gameManager.CardSelectionSlotManager.ResetSlots();
            _gameManager.LevelManager.SetupNewLevelController();
            _entranceAnimation.PlayInitialAnimation();
            GameStateController.IsPlayable = false;
        }

        public void StarNewtevel() => HandleNewLevel();

        public void HandleNewLevel()
        {
            if (_levelController.HasCompletedAllPhases())
            {
                Debug.Log("Todos os niveis do cenario atual foram concluidos!");

                if (_scenarioController.HasCompletedAllScenarios())
                {
                    Debug.Log("Completou todos os cenarios.");
                    return;
                }

                _scenarioController.SetupNewScenario();
                Debug.Log("Novas configurações de cenario setadas.");
            }

            SetupNewLevelController();
            SetupEnemySpawner();

            Debug.Log("Nivel atual: " + _levelController.CurrentLevelId);
        }

        private void SetupNewLevelController()
        {
            _levelController.SetupLevelController(_scenarioController.CurrentScenarioSettings.LevelsSettings);
            _levelController.SetupNewLevelSettings();
        }

        private void SetupEnemySpawner()
        {
            _enemySpawner = new EnemySpawner(_levelController.CurrentLevelSettings.EnemyWaves, enemySpawnPoints, _gameManager.GeneralSettings.EnemyControlllers);
            _enemySpawner.StartNextWaveAsync();
        }
    }
}