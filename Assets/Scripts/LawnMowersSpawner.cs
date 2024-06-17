using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace PVZ.LawnMower
{
    [System.Serializable]
    public sealed class LawnMowersSpawner
    {
        [SerializeField] private LawnMowerController lawnMowerController;
        [SerializeField] private List<Transform> lawnMowersSpawnPoints;
        [SerializeField] private Transform lawnMowerSpawnParent;
        [SerializeField] private double timeBetweenEachGenerationInSeconds;

        public async void StartLawnMowerGeneration() => await GenerateLawnMowersTask();

        public async Task GenerateLawnMowersTask()
        {
            foreach (var spawnPoint in lawnMowersSpawnPoints)
            {
                Object.Instantiate(lawnMowerController, spawnPoint.position, Quaternion.identity, lawnMowerSpawnParent);
                await Task.Delay(System.TimeSpan.FromSeconds(timeBetweenEachGenerationInSeconds));
            }
        }
    }
}
