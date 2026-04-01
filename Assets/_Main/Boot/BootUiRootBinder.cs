using UnityEngine;

namespace Boot
{
    public class BootUiRootBinder : MonoBehaviour
    {
        private BootUiRootVm _vm;
        
        public void Bind(BootUiRootVm vm)
        {
            _vm = vm;
        }
    }
}