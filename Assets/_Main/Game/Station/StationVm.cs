using Core;
using Proxy;
using UnityEngine;

namespace Game
{
    public class StationVm : IViewModel
    {
        private readonly Station _data;
        private StationBinder _binder;
        
        public MonoBehaviour Binder => _binder;

        public StationVm(Station data)
        {
            _data = data;
        }
        
        public void OnAdd(Transform root)
        {
            const string directory = Constant.Paths.STATION_DIRECTORY_PATH;
            var path = directory + _data.TypeKey + "/Station";
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