using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MapRenderer : MonoBehaviour
    {
        [SerializeField] private MeshFilter meshFilter;

        public void DrawMap(GridInfo grid)
        {
            var mesh = new Mesh();
            var meshVertices = new List<Vector3>();
            var meshTriangles = new List<int>();
            var hexMaskUVs = new List<Vector2>();
            var hexOutlineUVs = new List<Vector2>();
            
            var hexes = grid.Hexes;
            foreach (var hex in hexes)
            {
                meshVertices.AddRange(hex.Value.Vertices);
                meshTriangles.AddRange(hex.Value.Triangles);
                hexMaskUVs.AddRange(hex.Value.VisibleUVs);
                hexOutlineUVs.AddRange(hex.Value.OutlineUVs);
            }

            mesh.name = "Grid";
            mesh.vertices = meshVertices.ToArray();
            mesh.triangles = meshTriangles.ToArray();
            mesh.uv = grid.UVs.ToArray();
            mesh.uv2 = hexOutlineUVs.ToArray();
            mesh.uv3 = hexMaskUVs.ToArray();
            
            meshFilter.mesh = mesh;
            mesh.RecalculateNormals();
        }
    }
}