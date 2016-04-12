using System.Configuration;

namespace AzureWSBridge.Lib
{
    /// <summary>
    /// Reads configuration properties from project .config files.
    /// </summary>
    internal static class Config
    {
        private const string SectionName = "appSettings";
        private static KeyValueConfigurationCollection _settings;

        private static void ReadSettings()
        {
            // Don't read all settings again if they are already loaded
            if (_settings != null)
                return;

            // Fetch the KeyValueConfigurationCollection with all the settings in SectionName
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = (AppSettingsSection) config.GetSection(SectionName);
            _settings = section.Settings;
        }

        /// <summary>
        /// Read a given setting from the .config file.
        /// </summary>
        /// 
        /// <param name="key">Key to read.</param>
        /// <returns>Null if the given key does not exist, its value otherwise.</returns>
        public static string ReadSetting(string key)
        {
            ReadSettings();
            return (_settings[key] == null) ? null : _settings[key].Value;
        }
    }
}
