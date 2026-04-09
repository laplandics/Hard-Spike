using Core;
using ObservableCollections;
using UnityEngine;

namespace Game
{
    public class GameWorldRootVm : IRootViewModel
    {
        private GameWorldRootBinder _gameWorld;
        
        public IObservableCollection<StructureVm> StructureVms { get; }
        public IObservableCollection<HexVm> HexVms { get; }
        public MonoBehaviour Binder => _gameWorld;

        public GameWorldRootVm(HexCreator hexCreator, StructureCreator structureCreator)
        {
            HexVms = hexCreator.HexVms;
            StructureVms = structureCreator.StructureVms;
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