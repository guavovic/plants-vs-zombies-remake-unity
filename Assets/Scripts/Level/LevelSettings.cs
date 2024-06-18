using System;
using System.Collections.Generic;
using UnityEngine;

namespace PVZ.Level
{
    [Serializable]
    public struct LevelSettings
    {
        [SerializeField] private string name;
        [SerializeField] private LevelId levelId;
        [SerializeField] private List<EnemyWave> enemyWaves;
        [SerializeField] private bool hasSunsFall;

        public readonly string Name => name;
        public readonly LevelId LevelId => levelId;
        public readonly List<EnemyWave> EnemyWaves => enemyWaves;
        public bool HasSunsFall => hasSunsFall;
    }
}