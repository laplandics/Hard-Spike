using R3;

namespace Game
{
    public class MapCreator
    {
        private readonly GridGenerator _gridGenerator;
        public GridInfo Grid { get; private set; }
        public ReactiveProperty<MapVm> MapVm { get; private set; }
        
        public MapCreator(GridGenerator gridGenerator)
        {
            _gridGenerator = gridGenerator;
            MapVm = new ReactiveProperty<MapVm>();
        }

        public void CreateMap()
        {
            _gridGenerator.GenerateGrid();
            Grid = _gridGenerator.Grid;
            MapVm.Value = new MapVm(Grid);
        }

        public void DestroyMap()
        {
            MapVm.Value = null;
        }
    }
}