using UnityEngine;

namespace Game
{
    public class MapBinder : MonoBehaviour
    {
        [SerializeField] private MapRenderer mapRenderer;

        private MapVm _vm;
        
        public void Bind(MapVm vm)
        {
            _vm = vm;
            mapRenderer.DrawMap(_vm.Grid.Value);
        }
    }
}