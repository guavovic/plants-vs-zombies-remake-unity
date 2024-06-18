using PVZ.CardSlot;
using PVZ.Enemy;
using PVZ.Level;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PVZ.GeneralSettings
{
    [CreateAssetMenu(fileName = "GeneralSettingsData", menuName = "ScriptableObjects/GeneralSettings", order = 1)]
    public sealed class GeneralSettingsScriptableObject : ScriptableObject
    {
        [Header("=== LEVEL SETTINGS ===")]
        [SerializeField] private List<ScenarioSettings> scenarioSettings;

        [Header("=== ENEMY SETTINGS ===")]
        [SerializeField] private List<EnemyControlller> enemyControlllers;

        [Header("=== CARD SETTINGS ===")]
        [SerializeField] private List<Card> cards;

        public List<ScenarioSettings> ScenarioSettings => scenarioSettings;
        public List<EnemyControlller> EnemyControlllers => enemyControlllers;
        public List<Card> Cards => cards;
    }
}