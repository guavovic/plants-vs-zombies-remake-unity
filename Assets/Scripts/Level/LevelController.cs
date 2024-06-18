using System.Collections.Generic;
using UnityEngine;

namespace PVZ.Level
{
    public sealed class LevelController
    {
        private List<LevelSettings> _currentLevelsSettings = new List<LevelSettings>();
        public LevelId CurrentLevelId { get; private set; } = LevelId.Unknown;
        public LevelSettings CurrentLevelSettings { get; private set; }

        public LevelController(List<LevelSettings> levels)
        {
            SetupLevelController(levels);
        }

        public void SetupLevelController(List<LevelSettings> levelSettings) => _currentLevelsSettings = levelSettings;

        public void SetupNewLevelSettings()
        {
            CurrentLevelId++;
            SetCurrentLevelSettings();
        }

        public bool HasCompletedAllPhases() => (int)CurrentLevelId > _currentLevelsSettings.Count;

        private void SetCurrentLevelSettings()
        {
            if (EqualityComparer<LevelSettings>.Default.Equals(_currentLevelsSettings[(int)CurrentLevelId], default) && CurrentLevelId != LevelId.None)
            {
                Debug.LogError("A configuração atual do nivel não existe.");
                return;
            }

            CurrentLevelSettings = _currentLevelsSettings[(int)CurrentLevelId];
        }
    }
}
