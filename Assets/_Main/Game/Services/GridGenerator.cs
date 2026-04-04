using System.Collections.Generic;
using System.Linq;
using Settings;
using UnityEngine;

namespace Game
{
    public class GridGenerator
    {
        private readonly HexGenerator _hexGenerator;
        public GridInfo Grid { get; private set; }
        
        private readonly int _width;
        private readonly int _height;
        
        public GridGenerator(MapSettings mapSettings, HexGenerator hexGenerator)
        {
            _hexGenerator = hexGenerator;
            _width = mapSettings.gridSize.x;
            _height = mapSettings.gridSize.y;
        }
        
        public void GenerateGrid()
        {
            var hexes = new List<HexInfo>();
            
            var index = 0;
            for (var x = -_width/2; x < _width/2; x++)
            {
                for (var z = -_height/2; z < _height/2; z++)
                {
                    var hex = _hexGenerator.GenerateHex(x, z, index);
                    hexes.Add(hex);
                    index += 7;
                }
            }

            var uvs = new List<Vector2>();
            foreach (var hex in hexes)
            { uvs.AddRange(hex.Vertices.Select(CreateGridUV)); }
            Grid = new GridInfo(hexes, uvs);
        }

        private Vector2 CreateGridUV(Vector3 vertex)
        {
            const float scale = Constant.Values.GRID_UV_SCALE;
            var uv = new Vector2(vertex.x * scale, vertex.z * scale);
            return uv;
        }
    }
    
    public struct GridInfo
    {
        public Dictionary<Vector3, HexInfo> Hexes { get; }
        public List<Vector2> UVs { get; }
        
        public GridInfo(List<HexInfo> hexes, List<Vector2> uvs)
        {
            Hexes = new Dictionary<Vector3, HexInfo>();
            foreach (var hex in hexes) Hexes.Add(hex.Center, hex);
            UVs = uvs;
        }
    }
}