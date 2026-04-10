using System;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "ProjectSettings", menuName = "Settings/Core/Project")]
    public class ProjectSettings : ScriptableObject
    {
        public StationSettings initialStation;
        public List<InitialResource> initialResources;
    }

    [Serializable]
    public class InitialResource
    {
        public int amount;
        public ResourceSettings resourceSettings;
    }
}