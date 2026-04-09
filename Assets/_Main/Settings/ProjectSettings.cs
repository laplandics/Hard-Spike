using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "ProjectSettings", menuName = "Configs/Settings/ProjectSettings")]
    public class ProjectSettings : ScriptableObject
    {
        public HexSettings initialHex;
    }
}