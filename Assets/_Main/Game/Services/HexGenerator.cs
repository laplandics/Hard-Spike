using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class HexGenerator
    {
        public HexInfo GenerateHex(int x, int z, int index)
        {
            var hexCenter = PlaceHex(x, z);
            var hexUVs = CreateHexUVs();
            var hexVerts = CreateHexVertices(hexCenter);
            var hexTris = CreateHexTriangles(index);
            
            var hex = new HexInfo(hexVerts, hexTris, hexUVs, hexCenter);
            
            return hex;
        }
        
        private Vector3 PlaceHex(int x, int z)
        {
            const float size = Constant.Values.GRID_HEX_SIZE;
            var shouldOffset = Mathf.Abs(z) % 2 == 1;
            var width = Mathf.Sqrt(3) * size;
            var height = 2f * size;

            var hDist = width;
            var vDist = height * (3f / 4f);

            var offset = shouldOffset ? width / 2 : 0;
            
            var xPos = x * hDist + offset;
            var zPos = z * vDist;
            
            return new Vector3(xPos, 0, zPos);
        }

        private Vector3[] CreateHexVertices(Vector3 center)
        {
            const float size = Constant.Values.GRID_HEX_SIZE;
            var vertices = new Vector3[7];
            vertices[0] = center;
            
            for (var i = 0; i < 6; i++)
            {
                var angle = Mathf.Deg2Rad * (60 * i + 30);
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
                tris.Add(startIndex + (i + 1) % 6 + 1);
                tris.Add(startIndex + i + 1);
            }

            return tris.ToArray();
        }

        private (Vector2[] visibleUVs, Vector2[] transparentUVs, Vector2[] OutlineUVs) CreateHexUVs()
        {
            var outlineUVs = new Vector2[7];
            outlineUVs[0] = new Vector2(0.5f, 0.5f);
            outlineUVs[1] = new Vector2(1f, 0.75f);
            outlineUVs[2] = new Vector2(0.5f, 1f);
            outlineUVs[3] = new Vector2(0, 0.75f);
            outlineUVs[4] = new Vector2(0, 0.25f);
            outlineUVs[5] = new Vector2(0.5f, 0);
            outlineUVs[6] = new Vector2(1, 0.25f);
            
            var visibleUVs = new Vector2[7];
            visibleUVs[0] = new Vector2(0.5f, 0.5f) - Vector2.right * 0.5f;
            visibleUVs[1] = new Vector2(1f, 0.75f) - Vector2.right * 0.5f;
            visibleUVs[2] = new Vector2(0.5f, 1f) - Vector2.right * 0.5f;
            visibleUVs[3] = new Vector2(0, 0.75f) - Vector2.right * 0.5f;
            visibleUVs[4] = new Vector2(0, 0.25f) - Vector2.right * 0.5f;
            visibleUVs[5] = new Vector2(0.5f, 0) - Vector2.right * 0.5f;
            visibleUVs[6] = new Vector2(1, 0.25f) - Vector2.right * 0.5f;
            
            var transparentUVs = new Vector2[7];
            transparentUVs[0] = new Vector2(0.5f, 0.5f) + Vector2.right * 0.5f;
            transparentUVs[1] = new Vector2(1f, 0.75f) + Vector2.right * 0.5f;
            transparentUVs[2] = new Vector2(0.5f, 1f) + Vector2.right * 0.5f;
            transparentUVs[3] = new Vector2(0, 0.75f) + Vector2.right * 0.5f;
            transparentUVs[4] = new Vector2(0, 0.25f) + Vector2.right * 0.5f;
            transparentUVs[5] = new Vector2(0.5f, 0) + Vector2.right * 0.5f;
            transparentUVs[6] = new Vector2(1, 0.25f) + Vector2.right * 0.5f;

            return (visibleUVs, transparentUVs, outlineUVs);
        }
    }
    
    public struct HexInfo
    {
        public Vector3[] Vertices { get; }
        public int[] Triangles { get; }
        public Vector2[] OutlineUVs { get; }
        public Vector2[] VisibleUVs { get; }
        public Vector2[] TransparentUVs { get; }
        public Vector3 Center { get; }
        
        public HexInfo
        (
            Vector3[] vertices,
            int[] triangles,
            (Vector2[] visibleUVs, 
            Vector2[] transparentUVs,
            Vector2[] outlineUVs) uvs,
            Vector3 center
        )
        {
            Vertices = vertices;
            Triangles = triangles;
            OutlineUVs = uvs.outlineUVs;
            VisibleUVs = uvs.visibleUVs;
            TransparentUVs = uvs.transparentUVs;
            Center = center;
        }
    }
}