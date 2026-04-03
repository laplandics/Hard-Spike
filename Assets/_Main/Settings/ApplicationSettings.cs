using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "ApplicationSettings", menuName = "Configs/Settings/ApplicationSettings")]
    public class ApplicationSettings : ScriptableObject
    {
        public int vSync;
        public int fps;
    }
}