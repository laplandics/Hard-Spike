using UnityEngine;

namespace Game
{
    public class HexBinder : MonoBehaviour
    {
        private HexVm _vm;
        
        public void Bind(HexVm vm)
        {
            _vm = vm;
            transform.position = _vm.Position;
        }
    }
}