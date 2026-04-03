using R3;
using UnityEngine;

namespace Game
{
    public class MapVm
    {
        public ReactiveProperty<GridInfo> Grid { get; }
        public MapBinder Binder { get; private set; }

        public MapVm(ReactiveProperty<GridInfo> grid)
        {
            Grid = grid;
        }

        public void OnAdd(Transform root)
        {
            const string path = Constant.Paths.MAP_DIRECTORY_PATH + "Map";
            var mapPrefab = Resources.Load<MapBinder>(path);
            Binder = Object.Instantiate(mapPrefab, root, false);
            Binder.Bind(this);
        }
    }
}