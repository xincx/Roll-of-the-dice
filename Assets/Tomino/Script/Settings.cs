using UnityEngine;

namespace Tomino
{
    public static class Settings
    {
        public delegate void SettingsDelegate();
        public static SettingsDelegate ChangedEvent = delegate { };

        private static readonly string musicEnabledKey = "tomino.settings.musicEnabled";

        public static bool MusicEnabled
        {
            get
            {
                return PlayerPrefs.GetInt(musicEnabledKey, 1).BoolValue();
            }

            set
            {
                PlayerPrefs.SetInt(musicEnabledKey, value.IntValue());
                PlayerPrefs.Save();
                ChangedEvent.Invoke();
            }
        }
    }
}
