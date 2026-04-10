using Core;
using UnityEngine;

namespace Boot
{
    public class BootUiVm : IViewModel
    {
        private BootUiBinder _binder;
        
        public MonoBehaviour Binder => _binder;
        
        public void OnAdd(Transform root)
        {
            const string path = Constant.Paths.BOOT_UI_PREFAB_PATH;
            var prefab = Resources.Load<BootUiBinder>(path);
            var binder = Object.Instantiate(prefab, root, false);
            _binder = binder;
        }

        public void OnRemove()
        {
            Object.Destroy(_binder.gameObject);
        }
    }
}