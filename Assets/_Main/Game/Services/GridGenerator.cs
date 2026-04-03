using System.Collections.Generic;
using Settings;
using UnityEngine;

namespace Game
{
    public class GridGenerator
    {
        public GridInfo Grid { get; private set; }

        private readonly int _radius;
        
        public GridGenerator(ProjectSettings projectSettings)
        {
            _radius = projectSettings.gridRadius;
        }
        
        public void GenerateGrid()
        {
            var hexes = new List<HexInfo>();

            var index = 0;
            for (var q = -_radius; q <= _radius; q++)
            {
                var rMin = Mathf.Max(-_radius, -q - _radius);
                var rMax = Mathf.Min(_radius, -q + _radius);

                for (var r = rMin; r <= rMax; r++)
                {
                    var center = PlaceHex(q, r);

                    var hexVerts = CreateHexVertices(center);
                    var tris = CreateHexTriangles(index);
                    hexes.Add(new HexInfo(hexVerts, tris));
                    
                    index += 7;
                }
            }

            Grid = new GridInfo(hexes);
        }

        private Vector3 PlaceHex(int q, int r)
        {
            const float size = Constant.Values.HEX_SIZE;
            var x = size * 1.5f * q;
            var z = size * Mathf.Sqrt(3) * (r + q * 0.5f);

            return new Vector3(x, 0, z);
        }

        private Vector3[] CreateHexVertices(Vector3 center)
        {
            const float size = Constant.Values.HEX_SIZE;
            var vertices = new Vector3[7];
            vertices[0] = center;
            
            for (var i = 0; i < 6; i++)
            {
                var angle = Mathf.Deg2Rad * (60 * i);
                vertices[i + 1] = new Vector3(
                    center.x + size * Mathf.Cos(angle),
                    center.y,
                    center.z + size * Mathf.Sin(angle)
                );
            }

            return vertices;
        }
        
        private int[] CreateHexTriangles(int startIndex)
        {
            var tris = new List<int>();

            for (var i = 0; i < 6; i++)
            {
                tris.Add(startIndex);
                tris.Add(startIndex + ((i + 1) % 6) + 1);
                tris.Add(startIndex + i + 1);
            }

            return tris.ToArray();
        }

    }
    public struct HexInfo
    {
        public Vector3[] Vertices { get; private set; }
        public int[] Triangles { get; private set; }
        
        public HexInfo(Vector3[] vertices, int[] triangles)
        {
            Vertices = vertices;
            Triangles = triangles;
        }
    }
    
    public struct GridInfo
    {
        public List<HexInfo> Hexes { get; }
        
        public GridInfo(List<HexInfo> hexes)
        {
            Hexes = hexes;
        }
    }
}