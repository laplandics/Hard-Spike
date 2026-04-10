using UnityEngine;

namespace Game
{
    public class StationBinder : MonoBehaviour
    {
        private StationVm _vm;
        
        public void Bind(StationVm vm)
        {
            _vm = vm;
        }
    }
}