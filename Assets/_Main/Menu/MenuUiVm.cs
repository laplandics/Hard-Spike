using Core;
using UnityEngine;

namespace Menu
{
    public class MenuUiVm : IViewModel
    {
        private MenuUiBinder _binder;
        
        public MonoBehaviour Binder => _binder;
        
        public void OnAdd(Transform root)
        {
            var path = Constant.Paths.MENU_UI_PREFAB_PATH;
            var prefab = Resources.Load<MenuUiBinder>(path);
            var binder = Object.Instantiate(prefab, root, false);
            _binder = binder;
        }

        public void OnRemove()
        {
            Object.Destroy(_binder.gameObject);
        }
    }
}