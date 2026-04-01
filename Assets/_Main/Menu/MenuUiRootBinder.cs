using UnityEngine;

namespace Menu
{
    public class MenuUiRootBinder : MonoBehaviour
    {
        private MenuUiRootVm _vm;
        
        public void Bind(MenuUiRootVm vm)
        {
            _vm = vm;
        }
    }
}