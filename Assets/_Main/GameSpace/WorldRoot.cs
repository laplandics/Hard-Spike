using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace GameSpace
{
    public class WorldRoot : IDisposable
    {
        private readonly WorldContainer _worldContainer;
        private readonly List<IViewModel> _worldVms = new();

        public WorldRoot()
        {
            var worldContainer = new GameObject("[WORLD]");
            _worldContainer = worldContainer.AddComponent<WorldContainer>();
        }
        
        public void AddWorld(IViewModel worldVm)
        {
            worldVm.OnAdd(_worldContainer.transform);
            _worldVms.Add(worldVm);
        }

        public void RemoveWorld(IViewModel worldVm)
        {
            worldVm.OnRemove();
            _worldVms.Remove(worldVm);
        }

        public void Dispose()
        {
            foreach (var worldVm in _worldVms) worldVm.OnRemove();
            _worldVms.Clear();
        }
        
        internal class WorldContainer : MonoBehaviour {}
    }
}