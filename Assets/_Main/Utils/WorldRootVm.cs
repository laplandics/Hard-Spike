using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public class WorldRootVm : IDisposable
    {
        internal class WorldRootBinder : MonoBehaviour {}

        private WorldRootBinder _binder;
        
        public WorldRootVm()
        {
            _binder = new GameObject("[WORLD]").AddComponent<WorldRootBinder>();
        }

        public void Dispose()
        {
            for (var child = _binder.transform.childCount - 1; child >= 0; child--)
            {
                Object.Destroy(_binder.transform.GetChild(child).gameObject);
            }
        }
    }
}