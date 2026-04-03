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
            
            var hexes = grid.Hexes;
            hexes.ForEach(hex =>
            {
                meshVertices.AddRange(hex.Vertices);
                meshTriangles.AddRange(hex.Triangles);
            });

            mesh.name = "Grid";
            mesh.vertices = meshVertices.ToArray();
            mesh.triangles = meshTriangles.ToArray();
            
            meshFilter.mesh = mesh;
            mesh.RecalculateNormals();
        }
    }
}