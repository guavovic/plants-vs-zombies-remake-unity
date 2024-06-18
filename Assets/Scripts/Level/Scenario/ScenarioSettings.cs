using System;
using System.Collections.Generic;
using UnityEngine;

namespace PVZ.Level
{
    [Serializable]
    public struct ScenarioSettings
    {
        [SerializeField] private string name;
        [SerializeField] private ScenarioId scenarioId;
        [SerializeField] private List<LevelSettings> levelsSettings;

        public readonly string Name => name;
        public readonly ScenarioId ScenarioId => scenarioId;
        public readonly List<LevelSettings> LevelsSettings => levelsSettings;
    }
}