using UnityEngine;

namespace PVZ.GeneralSettings
{
    public static class GeneralSettingsHelper
    {
        private static GeneralSettingsScriptableObject _globalSettingsScriptableObject;
        private const string _fileNameInResources = "GeneralSettingsData";

        public static GeneralSettingsScriptableObject GetGeneralSettings()
        {
            return _globalSettingsScriptableObject ??= Resources.Load<GeneralSettingsScriptableObject>(_fileNameInResources);
        }
    }
}