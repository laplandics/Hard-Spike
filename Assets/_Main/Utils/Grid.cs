using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class Grid
    {
        public List<Vector2Int> GetIndexesInRadius(int radius, Vector2Int center)
        {
            var results = new List<Vector2Int>();

            for (var q = -radius; q <= radius; q++)
            {
                var r1 = Mathf.Max(-radius, -q - radius);
                var r2 = Mathf.Min(radius, -q + radius);

                for (var r = r1; r <= r2; r++)
                {
                    var x = center.x + q;
                    var y = center.y + r;
                    results.Add(new Vector2Int(x, y));
                }
            }
            
            return results;
        }
        
        public Vector3 IndexToHex(Vector2Int index)
        {
            const int size = Constant.Values.HEX_SIZE;
            
            var x = size * (Mathf.Sqrt(3f) * index.x + Mathf.Sqrt(3f) / 2f * index.y);
            var z = size * (3f / 2f * index.y);

            return new Vector3(x, 0, z);
        }

        public Vector2Int WorldToIndex(Vector3 pos)
        {
            const int size = Constant.Values.HEX_SIZE;
            
            var q = (Mathf.Sqrt(3f)/3f * pos.x - 1f/3f * pos.z) / size;
            var r = (2f/3f * pos.z) / size;
            
            var x = q;
            var z = r;
            var y = -x - z;

            var rx = Mathf.RoundToInt(x);
            var ry = Mathf.RoundToInt(y);
            var rz = Mathf.RoundToInt(z);

            var xDiff = Mathf.Abs(rx - x);
            var yDiff = Mathf.Abs(ry - y);
            var zDiff = Mathf.Abs(rz - z);

            if (xDiff > yDiff && xDiff > zDiff) rx = -ry - rz;
            else if (yDiff > zDiff) ry = -rx - rz;
            else rz = -rx - ry;

            return new Vector2Int(rx, rz);
        }
    }
}