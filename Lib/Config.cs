using System.Configuration;

namespace AzureWSBridge.Lib
{
    internal static class Config
    {
        private static readonly string _sectionName = "appSettings";
        private static KeyValueConfigurationCollection _settings;

        private static void ReadSettings()
        {
            if (_settings != null)
                return;

            var section = (AppSettingsSection) ConfigurationManager.GetSection(_sectionName);
            _settings = section.Settings;
        }

        public static string ReadSetting(string key)
        {
            ReadSettings();
            return _settings[key].Value;
        }
    }
}
