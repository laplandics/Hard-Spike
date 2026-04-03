using System.Collections.Generic;
using ObservableCollections;
using R3;
using UnityEngine;

namespace Game
{
    public class GameWorldRootBinder : MonoBehaviour
    {
        private readonly Dictionary<string, StructureBinder> _structureBindersMap = new();
        private CompositeDisposable _disposables = new();
        private GameWorldRootVm _vm;
        
        public void Bind(GameWorldRootVm vm)
        {
            _vm = vm;

            foreach (var structureVm in _vm.StructureVms) AddStructure(structureVm);
            _disposables.Add(_vm.StructureVms.ObserveAdd().Subscribe(e => AddStructure(e.Value)));
            _disposables.Add(_vm.StructureVms.ObserveRemove().Subscribe(e => RemoveStructure(e.Value)));
        }

        private void AddStructure(StructureVm structureVm)
        {
            structureVm.OnAdd(transform);
            var structureBinder = structureVm.Binder as StructureBinder;
            _structureBindersMap.Add(structureVm.Id, structureBinder);
        }

        private void RemoveStructure(StructureVm structureVm)
        {
            structureVm.OnRemove();
            if (!_structureBindersMap.Remove(structureVm.Id))
            {Debug.LogError("Structure hasn't been added to structure binders map before removing");}
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}