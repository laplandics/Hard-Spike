using System.Collections.Generic;
using ObservableCollections;
using R3;
using UnityEngine;

namespace Game
{
    public class GameWorldRootBinder : MonoBehaviour
    {
        private MapBinder _mapBinder;
        private readonly Dictionary<string, StructureBinder> _structureBindersMap = new();
        
        private CompositeDisposable _disposables = new();
        private GameWorldRootVm _vm;
        
        public void Bind(GameWorldRootVm vm)
        {
            _vm = vm;

            _disposables.Add(_vm.MapVm.Where(map => map != null).Subscribe(CreateMap));
            _disposables.Add(_vm.MapVm.Where(map => map == null).Subscribe(DestroyMap));
            
            foreach (var structureVm in _vm.StructureVms) AddStructure(structureVm);
            _disposables.Add(_vm.StructureVms.ObserveAdd().Subscribe(e => AddStructure(e.Value)));
            _disposables.Add(_vm.StructureVms.ObserveRemove().Subscribe(e => RemoveStructure(e.Value)));
        }

        private void CreateMap(MapVm mapVm)
        {
            mapVm.OnAdd(transform);
            _mapBinder = mapVm.Binder;
        }

        private void DestroyMap(MapVm mapVm)
        {
            if (_mapBinder == null) return;
            Destroy(_mapBinder.gameObject);
        }
        
        private void AddStructure(StructureVm structureVm)
        {
            structureVm.OnAdd(transform);
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