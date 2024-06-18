using PVZ.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PVZ
{
    [Serializable]
    public struct EnemyWave
    {
        [SerializeField] private List<EnemyType> enemyTypes;
        [SerializeField] private float timeBetweenSpawnsDuring;
        [SerializeField] private float timeBetweenSpawnsFinal;
        [SerializeField] private int maxEnemiesDuring;
        [SerializeField] private int maxEnemiesFinal;

        public readonly List<EnemyType> EnemyTypes => enemyTypes;
        public readonly float TimeBetweenSpawnsDuring => timeBetweenSpawnsDuring;
        public readonly float TimeBetweenSpawnsFinal => timeBetweenSpawnsFinal;
        public readonly int MaxEnemiesDuring => maxEnemiesDuring;
        public readonly int MaxEnemiesFinal => maxEnemiesFinal;
    }
}