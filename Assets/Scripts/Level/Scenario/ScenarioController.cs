using System.Collections.Generic;

namespace PVZ.Level
{
    public sealed class ScenarioController
    {
        private readonly List<ScenarioSettings> _scenariosSettings;
        private ScenarioId _currentScenarioIndex = ScenarioId.Garden_Day;

        public ScenarioSettings CurrentScenarioSettings { get; private set; }

        public ScenarioController(List<ScenarioSettings> scenarios)
        {
            _scenariosSettings = scenarios;
            SetupCurrentScenario();
        }

        public void SetupNewScenario()
        {
            _currentScenarioIndex++;
            SetupCurrentScenario();
        }

        public void SetupCurrentScenario()
        {
            CurrentScenarioSettings = _scenariosSettings[(int)_currentScenarioIndex];
        }

        public bool HasCompletedAllScenarios() => (int)_currentScenarioIndex > _scenariosSettings.Count;
    }
}
