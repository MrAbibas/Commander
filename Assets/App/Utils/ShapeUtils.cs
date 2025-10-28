using System.Collections.Generic;
using App.Gameplay.Level;
using UnityEngine;

namespace App.Utils
{
    public static class ShapeUtils
    {
        public static bool IsPointInsidePolygon(Vector2 testPoint, List<Vector3> polygon)
        {
            int numVertices = polygon.Count;
            float x = testPoint.x;
            float y = testPoint.y;
            bool inside = false;

            Vector2 p1 = new Vector2(polygon[0].x, polygon[0].z);
            Vector2 p2;

            for (int i = 1; i <= numVertices; i++)
            {
                p2 = new Vector2(polygon[i % numVertices].x, 
                    polygon[i % numVertices].z);

                if (y > Mathf.Min(p1.y, p2.y))
                {
                    if (y <= Mathf.Max(p1.y, p2.y))
                    {
                        if (x <= Mathf.Max(p1.x, p2.x))
                        {
                            float xIntersection = (y - p1.y) * (p2.x - p1.x) / (p2.y - p1.y) + p1.x;

                            if (Mathf.Approximately(p1.x, p2.x) || x <= xIntersection)
                            {
                                inside = !inside;
                            }
                        }
                    }
                }

                p1 = p2;
            }

            return inside;
        }
    }
}