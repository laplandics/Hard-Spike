using UnityEngine;
using Utils;

namespace Game
{
    public class GameUiRootVm : IUiVm
    {
        private GameUiRootBinder _gameUi;

        public MonoBehaviour Binder => _gameUi;

        public void OnAdd(Transform root)
        {
            var gameUiPrefab = Resources.Load<GameUiRootBinder>(Constant.Paths.GAME_UI_ROOT_PREFAB_PATH);
            _gameUi = Object.Instantiate(gameUiPrefab, root, false);
            _gameUi.Bind(this);
        }
        
        public void OnRemove()
        {
            Object.Destroy(_gameUi.gameObject);
        }
    }
}