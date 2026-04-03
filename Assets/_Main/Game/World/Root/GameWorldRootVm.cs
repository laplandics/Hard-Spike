using Core;
using ObservableCollections;
using R3;
using UnityEngine;

namespace Game
{
    public class GameWorldRootVm : IRootViewModel
    {
        private GameWorldRootBinder _gameWorld;
        
        public ReactiveProperty<MapVm> MapVm { get; private set; }
        public IObservableCollection<StructureVm> StructureVms { get; }
        public MonoBehaviour Binder => _gameWorld;

        public GameWorldRootVm(MapCreator creator, StructureConstructor constructor)
        {
            MapVm = creator.MapVm;
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