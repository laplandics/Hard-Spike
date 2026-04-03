using Core;
using ObservableCollections;
using UnityEngine;

namespace Game
{
    public class GameWorldRootVm : IRootViewModel
    {
        private GameWorldRootBinder _gameWorld;

        public IObservableCollection<StructureVm> StructureVms { get; }
        public MonoBehaviour Binder => _gameWorld;

        public GameWorldRootVm(StructureConstructor constructor)
        {
            StructureVms = constructor.StructureVms;
        }
        
        public void OnAdd(Transform root)
        {
            var gameWorldPrefab = Resources.Load<GameWorldRootBinder>(Constant.Paths.GAME_WORLD_PREFAB_PATH);
            _gameWorld = Object.Instantiate(gameWorldPrefab, root, false);
            _gameWorld.Bind(this);
        }

        public void OnRemove()
        {
           Object.Destroy(_gameWorld.gameObject);
        }
    }
}