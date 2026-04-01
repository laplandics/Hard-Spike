using UnityEngine;
using Utils;

namespace Menu
{
    public class MenuUiRootVm : IUiVm
    {
        private MenuUiRootBinder _menuUi;

        public MonoBehaviour Binder => _menuUi;

        public void OnAdd(Transform root)
        {
            var menuUiPrefab = Resources.Load<MenuUiRootBinder>(Constant.Paths.MENU_UI_ROOT_PREFAB_PATH);
            _menuUi = Object.Instantiate(menuUiPrefab, root, false);
            _menuUi.Bind(this);
        }

        public void OnRemove()
        {
            Object.Destroy(_menuUi.gameObject);
        }
    }
}