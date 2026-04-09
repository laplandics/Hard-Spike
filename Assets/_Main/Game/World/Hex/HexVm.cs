using Constant;
using UnityEngine;

namespace Game
{
    public class HexVm
    {
        private readonly Hexes _typeKey;
        
        public string Id { get; }
        public Vector3 Position { get; }
        public HexBinder Binder { get; private set; }
        
        public HexVm(Proxy.Hex hexProxy)
        {
            Id = hexProxy.Id;
            Position = hexProxy.Position;
            _typeKey = hexProxy.TypeKey;
        }

        public void OnAdd(Transform root)
        {
            var path = Paths.HEXES_DIRECTORY_PATH + _typeKey + "/Hex";
            var hexPrefab = Resources.Load<HexBinder>(path);
            Binder = Object.Instantiate(hexPrefab, root, false);
            Binder.Bind(this);
        }

        public void OnRemove()
        {
            Object.Destroy(Binder.gameObject);
        }
    }
}