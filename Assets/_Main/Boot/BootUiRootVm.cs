using Core;
using UnityEngine;

namespace Boot
{
    public class BootUiRootVm : IRootViewModel
    {
        private BootUiRootBinder _bootUi;

        public MonoBehaviour Binder => _bootUi;

        public void OnAdd(Transform root)
        {
            var bootUiPrefab = Resources.Load<BootUiRootBinder>(Constant.Paths.BOOT_UI_ROOT_PREFAB_PATH);
            _bootUi = Object.Instantiate(bootUiPrefab, root, false);
            _bootUi.Bind(this);
        }

        public void OnRemove()
        {
            Object.Destroy(_bootUi.gameObject);
        }
    }
}