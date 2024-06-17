using PVZ.Sun;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace PVZ
{
    [Serializable]
    public sealed class CollectableSunManager : MonoBehaviour
    {
        [Header("--- COLLECTED SUNS ---")]
        [SerializeField] private TextMeshProUGUI collectedSunsTextMeshProUGUI;

        [Header("--- COLLECTABLE SUN INFOS ---")]
        [SerializeField] private CollectableSunController sunPrefab;
        [SerializeField] private BoxCollider2D sunSpawnAreaBoxCollider2D;
        [SerializeField] private Transform sunsSpawnParent;
        [SerializeField] private float timeBetweenGenerationsInSeconds;

        private CollectableSunSpawner _sunSpawner;

        public static int CollectedSuns { get; private set; } = 0;
        public static CollectableSunManager Instance { get; private set; }

        private void Awake() => Instance = this;

        private void Start()
        {
            _sunSpawner = new CollectableSunSpawner(sunPrefab, sunSpawnAreaBoxCollider2D, sunsSpawnParent, timeBetweenGenerationsInSeconds);
            UpdateCollectedSunsText();
        }

        public void SpawnCollectableSuns() => _sunSpawner?.Run();

        public void UseCollectedSuns(int value)
        {
            CollectedSuns -= value;
            UpdateCollectedSunsText();
        }

        public void SetCollectedSuns(int value)
        {
            CollectedSuns += value;
            UpdateCollectedSunsText();
        }

        public void ResetValues()
        {
            CollectedSuns = 0;
            UpdateCollectedSunsText();
            ClearGeneratedCollectibleSuns();
        }

        private void ClearGeneratedCollectibleSuns() => _sunSpawner.ClearCollectibleSunInstances();

        private void UpdateCollectedSunsText() => collectedSunsTextMeshProUGUI.text = CollectedSuns.ToString();
    }
}
