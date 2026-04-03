using Core;
using UnityEngine;

namespace Utils
{
    public class WorldSceneRoot
    {
        private World _ui = new GameObject("[WORLD]").AddComponent<World>();

        public void AddWorld(IRootViewModel uiVm)
        {
            uiVm.OnAdd(_ui.transform);
        }

        public void RemoveWorld(IRootViewModel uiVm)
        {
            uiVm.OnRemove();
        }
        
        internal class World : MonoBehaviour {}
    }
}