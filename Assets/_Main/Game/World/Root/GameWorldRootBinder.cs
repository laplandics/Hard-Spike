using System.Collections.Generic;
using ObservableCollections;
using R3;
using UnityEngine;

namespace Game
{
    public class GameWorldRootBinder : MonoBehaviour
    {
        private readonly Dictionary<string, HexBinder> _hexBindersMap = new();
        private readonly Dictionary<string, StructureBinder> _structureBindersMap = new();
        
        private Transform _hexContainer;
        private Transform _structureContainer;
        private CompositeDisposable _disposables = new();
        private GameWorldRootVm _vm;
        
        public void Bind(GameWorldRootVm vm)
        {
            _vm = vm;

            _structureContainer = new GameObject("Structures").transform;
            _hexContainer = new GameObject("Hexes").transform;
            
            _structureContainer.SetParent(transform, false);
            _hexContainer.SetParent(transform, false);
            
            foreach (var hexVm in _vm.HexVms) AddHex(hexVm);
            foreach (var structureVm in _vm.StructureVms) AddStructure(structureVm);
            
            _disposables.Add(_vm.HexVms.ObserveAdd().Subscribe(e => AddHex(e.Value)));
            _disposables.Add(_vm.HexVms.ObserveRemove().Subscribe(e => RemoveHex(e.Value)));
            
            _disposables.Add(_vm.StructureVms.ObserveAdd().Subscribe(e => AddStructure(e.Value)));
            _disposables.Add(_vm.StructureVms.ObserveRemove().Subscribe(e => RemoveStructure(e.Value)));
        }

        private void AddHex(HexVm hexVm)
        {
            hexVm.OnAdd(_hexContainer);
            var hexBinder = hexVm.Binder;
            _hexBindersMap.Add(hexVm.Id, hexBinder);
        }

        private void RemoveHex(HexVm hexVm)
        {
            hexVm.OnRemove();
            if (!_hexBindersMap.Remove(hexVm.Id))
            {Debug.LogError("Hex hasn't been added to hex binders map before removing");}
        }
        
        private void AddStructure(StructureVm structureVm)
        {
            structureVm.OnAdd(_structureContainer);
            var structureBinder = structureVm.Binder;
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