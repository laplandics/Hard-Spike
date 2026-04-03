using Constant;
using Proxy;
using R3;
using Settings;
using UnityEngine;

namespace Game
{
    public class StructureVm
    {
        private StructureSettings _settings;
        private readonly Structure _structureProxy;

        public string Id => _structureProxy.Id;
        public ReactiveProperty<Vector3> Position => _structureProxy.Position;
        public StructureBinder Binder { get; private set; }
        
        public StructureVm(Structure structureProxy)
        {
            _structureProxy = structureProxy;
        }

        public void OnAdd(Transform root)
        {
            var path = Paths.STRUCTURES_DIRECTORY_PATH + _structureProxy.TypeKey;
            
            var prefabPath = path + "/Structure";
            var structurePrefab = Resources.Load<StructureBinder>(prefabPath);
            
            var settingsPath = path + "/Settings";
            var structureSettings = Resources.Load<StructureSettings>(settingsPath);
            _settings = structureSettings;
            
            Binder = Object.Instantiate(structurePrefab, root, false);
            Binder.Bind(this);
        }

        public void OnRemove()
        {
            Object.Destroy(Binder.gameObject);
        }
    }
}