using Core;
using Proxy;
using UnityEngine;

namespace Game
{
    public class StationVm : IViewModel
    {
        private readonly Station _state;
        private StationBinder _binder;
        
        public MonoBehaviour Binder => _binder;

        public StationVm(Station state)
        {
            _state = state;
        }
        
        public void OnAdd(Transform root)
        {
            const string directory = Constant.Paths.STATION_DIRECTORY_PATH;
            var path = directory + _state.TypeKey + "/Station";
            var prefab = Resources.Load<StationBinder>(path);
            var binder = Object.Instantiate(prefab, root, false);
            binder.Bind(this);
            _binder = binder;
        }

        public void OnRemove()
        {
            Object.Destroy(_binder.gameObject);
        }
    }
}