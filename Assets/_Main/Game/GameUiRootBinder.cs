using UnityEngine;

namespace Game
{
    public class GameUiRootBinder : MonoBehaviour
    {
        private GameUiRootVm _vm;
        
        public void Bind(GameUiRootVm vm)
        {
            _vm = vm;
        }
    }
}