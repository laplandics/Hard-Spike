using R3;

namespace Game
{
    public class MapCreator
    {
        private readonly GridGenerator _gridGenerator;
        public ReactiveProperty<GridInfo> Grid { get; }
        public ReactiveProperty<MapVm> MapVm { get; private set; }
        
        public MapCreator(GridGenerator gridGenerator)
        {
            _gridGenerator = gridGenerator;
            Grid = new ReactiveProperty<GridInfo>(_gridGenerator.Grid);
            MapVm = new ReactiveProperty<MapVm>();
        }

        public void CreateMap()
        {
            _gridGenerator.GenerateGrid();
            Grid.Value = _gridGenerator.Grid;
            MapVm.Value = new MapVm(Grid);
        }

        public void DestroyMap()
        {
            MapVm.Value = null;
        }
    }
}