using Proxy;
using R3;
using UnityEngine;

namespace Game
{
    public class StructureVm
    {
        public readonly string Id;
        public readonly string TypeKey;
        public readonly ReactiveProperty<Vector3> Position;
        
        private readonly Structure _structureProxy;
        private StructureBinder _structure;

        public MonoBehaviour Binder => _structure;
        
        public StructureVm(Structure structureProxy)
        {
            _structureProxy = structureProxy;
            Position = _structureProxy.Position;
            Id = _structureProxy.Id;
            TypeKey = _structureProxy.TypeKey;
        }

        public void OnAdd(Transform root)
        {
            var path = Constant.Paths.STRUCTURE_PREFAB_PATH + TypeKey + "/Structure";
            var structurePrefab = Resources.Load<StructureBinder>(path);
            _structure = Object.Instantiate(structurePrefab, root, false);
            _structure.Bind(this);
        }

        public void OnRemove()
        {
            Object.Destroy(_structure.gameObject);
        }
    }
}