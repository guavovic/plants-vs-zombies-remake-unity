using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace PVZ.Enemy
{
    public sealed class EnemySpawner
    {
        private readonly List<EnemyWave> _waves;
        private readonly List<Transform> _enemySpawnPoints;
        private readonly List<EnemyControlller> _enemyControlllers;
        private EnemyWave _currentWaveData;
        private List<GameObject> _instantiatedEnemies;
        private readonly float _timeInSecondsBetweenWaves = 3f;
        private int _currentWaveIndex;
        private bool _isSpawning;
        private bool _isWaveFinalState;
        public bool AllWavesCompleted { get; private set; }

        public EnemySpawner(List<EnemyWave> waves, List<Transform> enemySpawnPoints, List<EnemyControlller> enemyControlllers)
        {
            _waves = waves;
            _enemySpawnPoints = enemySpawnPoints;
            _enemyControlllers = enemyControlllers;
        }

        public void Run()
        {
            if (AllWavesCompleted || _isSpawning)
                return;

            if (AllEnemiesDestroyed())
            {
                if (_waves.Count == 0)
                {
                    Debug.LogError("Não foram encontradas ondas neste nivel! \n Verifique se o nivel foi corretamente configurado.");
                    return;
                }

                _currentWaveIndex++;

                if (_currentWaveIndex < _waves.Count)
                {
                    StartNextWaveAsync();
                    AllWavesCompleted = false;
                }
                else
                {
                    Debug.Log("Todas as ondas foram completadas.\n Nivel atual completado!");
                    AllWavesCompleted = true;
                }
            }
        }

        public async void StartNextWaveAsync()
        {
            try
            {
                await SpawnEnemiesAsync(_isWaveFinalState);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Erro ao iniciar a próxima onda: {e.Message}");
            }
        }

        public async Task SpawnEnemiesAsync(bool isDuring)
        {
            _currentWaveData = _waves[_currentWaveIndex];

            _isSpawning = true;

            int numberEnemies = isDuring ? _currentWaveData.MaxEnemiesDuring : _currentWaveData.MaxEnemiesFinal;
            float timeBetweenSpawns = isDuring ? _currentWaveData.TimeBetweenSpawnsDuring : _currentWaveData.TimeBetweenSpawnsFinal;

            int currentWaveIndexDebug = _currentWaveIndex + 1;
            int zombiesSpawned = 0;

            Debug.Log($"Iniciando onda {currentWaveIndexDebug}.");

            await Task.Delay(System.TimeSpan.FromSeconds(_timeInSecondsBetweenWaves));

            Debug.Log($"Onda {currentWaveIndexDebug} iniciada!");
            _instantiatedEnemies = new List<GameObject>();

            while (zombiesSpawned < numberEnemies)
            {
                EnemyControlller enemyPrefab = GetRandomEnemyPrefab();
                SpawnEnemy(enemyPrefab);
                zombiesSpawned++;
                await Task.Delay(System.TimeSpan.FromSeconds(timeBetweenSpawns));
            }

            _isSpawning = false;
            _isWaveFinalState = _currentWaveIndex == _waves.Count;
        }

        private void SpawnEnemy(EnemyControlller enemyPrefab)
        {
            var transform = GetRandomEnemySpawnPoint();
            var newEnemy = Object.Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform).gameObject;
            _instantiatedEnemies.Add(newEnemy);
        }

        private Transform GetRandomEnemySpawnPoint()
        {
            return _enemySpawnPoints[Random.Range(0, _enemySpawnPoints.Count)];
        }

        private EnemyControlller GetRandomEnemyPrefab()
        {
            return _enemyControlllers[Random.Range(0, _currentWaveData.EnemyTypes.Count)];
        }

        private bool AllEnemiesDestroyed()
        {
            if (_instantiatedEnemies == null)
                return false;

            return _instantiatedEnemies.TrueForAll(enemy => enemy == null);
        }
    }
}