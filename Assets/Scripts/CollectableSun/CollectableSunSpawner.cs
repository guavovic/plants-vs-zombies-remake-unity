using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace PVZ.Sun
{
    public sealed class CollectableSunSpawner
    {
        private readonly CollectableSunController _collectableSunPrefab;
        private readonly BoxCollider2D _boxCollider2D;
        private readonly Transform _collectableSunsSpawnParent;
        private readonly float _timeBetweenGenerationsInSeconds;
        private bool _canGenerate = true;
        private readonly List<CollectableSunController> _collectibleSunInstances = new List<CollectableSunController>();

        public CollectableSunSpawner(CollectableSunController collectableSunPrefab, BoxCollider2D boxCollider2D, Transform collectableSunSpawnParent, float timeBetweenGenerationsInSeconds)
        {
            _collectableSunPrefab = collectableSunPrefab;
            _boxCollider2D = boxCollider2D;
            _collectableSunsSpawnParent = collectableSunSpawnParent;
            _timeBetweenGenerationsInSeconds = timeBetweenGenerationsInSeconds;
            _timeBetweenGenerationsInSeconds = timeBetweenGenerationsInSeconds;
        }

        public void Run()
        {
            if (!_canGenerate)
                return;

            StartSpawnCollectableSuns();
        }

        public void ClearCollectibleSunInstances()
        {
            foreach (var collectibleSunInstance in _collectibleSunInstances)
            {
                if (collectibleSunInstance != null)
                    Object.Destroy(collectibleSunInstance.gameObject);
            }

            _collectibleSunInstances.Clear();

            Debug.Log("Lista de sóis coletáveis instanciados limpa.");
        }

        private async void StartSpawnCollectableSuns()
        {
            try
            {
                await SpawnCollectableSunsAsync();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Erro ao gerar sóis coletáveis: {e.Message}");
            }
        }
        private async Task SpawnCollectableSunsAsync()
        {
            _canGenerate = false;
            SpawnCollectableSun();
            await Task.Delay(System.TimeSpan.FromSeconds(_timeBetweenGenerationsInSeconds));
            _canGenerate = true;
        }

        private void SpawnCollectableSun()
        {
            var randomSpawnPosition = GetRandomAreaInBoxCollider2D();
            var collectibleSunInstancied = Object.Instantiate(_collectableSunPrefab, randomSpawnPosition, Quaternion.identity, _collectableSunsSpawnParent);
            _collectibleSunInstances.Add(collectibleSunInstancied);
        }

        private Vector2 GetRandomAreaInBoxCollider2D()
        {
            return new Vector2(
                Random.Range(_boxCollider2D.bounds.min.x, _boxCollider2D.bounds.max.x),
                Random.Range(_boxCollider2D.bounds.min.y, _boxCollider2D.bounds.max.y)
         );
        }
    }
}