using Core;
using UnityEngine;

namespace Game
{
    public class GameUiVm : IViewModel
    {
        private GameUiBinder _binder;
        
        public MonoBehaviour Binder => _binder;
        
        public void OnAdd(Transform root)
        {
            const string path = Constant.Paths.GAME_UI_PREFAB_PATH;
            var prefab = Resources.Load<GameUiBinder>(path);
            var binder = Object.Instantiate(prefab, root, false);
            _binder = binder;
        }

        public void OnRemove()
        {
            Object.Destroy(_binder.gameObject);
        }
    }
}