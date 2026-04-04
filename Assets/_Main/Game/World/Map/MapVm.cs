using UnityEngine;

namespace Game
{
    public class MapVm
    {
        public GridInfo Grid { get; }
        public MapBinder Binder { get; private set; }

        public MapVm(GridInfo grid)
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