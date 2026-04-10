using Constant;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Settings/Entity/Station")]
    public class StationSettings : ScriptableObject
    {
        public Stations typeKey;
        
    }
}