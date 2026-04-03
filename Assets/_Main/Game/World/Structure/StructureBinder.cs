using UnityEngine;

namespace Game
{
    public class StructureBinder : MonoBehaviour
    {
        private StructureVm _vm;
        
        public void Bind(StructureVm vm)
        {
            _vm = vm;
            transform.position = _vm.Position.CurrentValue;
        }
    }
}