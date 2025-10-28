using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

namespace App.Gameplay.Level
{
    public class AreaUpdater : MonoBehaviour
    {
        [SerializeField] private SpriteShapeController shapeController;
        [SerializeField] private List<AreaFormPoint> points = new();
        [SerializeField] private float offsetRadius = 2.0f;
        [SerializeField] private float pointPositionThreshold = 0.3f;
        
        private readonly List<Transform> _hullPoints = new();
        private readonly List<Vector3> _sortedPositions = new();
        private Vector3 _areaCenter;
        private bool _isDirty = false;

        private void Update()
        {
            if (!_isDirty)
            {
                CheckPointsMovement();
            }
        }
        
        private void CheckPointsMovement()
        {
            int count = points.Count;
            for (int i = 0; i < count; i++)
            {
                var point = points[i];
                float sqrDistance = (point.BakedPosition - point.transform.position).sqrMagnitude;
                if (sqrDistance > pointPositionThreshold * pointPositionThreshold)
                {
                    _isDirty = true;
                    UpdateArea();
                    return;
                }
            }
        }

        public void AddPoint(AreaFormPoint formPoint)
        {
            points.Add(formPoint);
            _isDirty = true;
            UpdateArea();
        }

        public void RemovePoint(AreaFormPoint formPoint)
        {
            points.Remove(formPoint);
            _isDirty = true;
            UpdateArea();
        }

        private void CalculateAreaCenter()
        {
            if (_hullPoints.Count == 0)
            {
                _areaCenter = Vector3.zero;
                return;
            }

            Vector3 sum = Vector3.zero;
            int count = _hullPoints.Count;
            for (int i = 0; i < count; i++)
            {
                sum += _hullPoints[i].position;
            }
            _areaCenter = sum / count;
        }

        private void ComputeConvexHull()
        {
            _hullPoints.Clear();
            
            if (points.Count < 3)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    _hullPoints.Add(points[i].transform);
                }
                return;
            }
            
            _sortedPositions.Clear();
            int pointCount = points.Count;
            for (int i = 0; i < pointCount; i++)
            {
                _sortedPositions.Add(points[i].transform.position);
            }
            
            var indices = Enumerable.Range(0, pointCount).ToList();
            indices.Sort((a, b) =>
            {
                Vector3 posA = _sortedPositions[a];
                Vector3 posB = _sortedPositions[b];

                if (Mathf.Approximately(posA.x, posB.x))
                    return posA.z.CompareTo(posB.z);
                return posA.x.CompareTo(posB.x);
            });
            
            var hullIndices = new List<int>();
            int lowerCount = 0;
            
            for (int i = 0; i < indices.Count; i++)
            {
                while (lowerCount >= 2 && CrossProduct(
                           _sortedPositions[hullIndices[lowerCount - 2]],
                           _sortedPositions[hullIndices[lowerCount - 1]],
                           _sortedPositions[indices[i]]) <= 0)
                {
                    hullIndices.RemoveAt(hullIndices.Count - 1);
                    lowerCount--;
                }

                hullIndices.Add(indices[i]);
                lowerCount++;
            }
            
            int upperStart = hullIndices.Count;
            for (int i = indices.Count - 2; i >= 0; i--)
            {
                while (hullIndices.Count - upperStart >= 1 && CrossProduct(
                           _sortedPositions[hullIndices[^2]],
                           _sortedPositions[hullIndices[^1]],
                           _sortedPositions[indices[i]]) <= 0)
                {
                    hullIndices.RemoveAt(hullIndices.Count - 1);
                }

                hullIndices.Add(indices[i]);
            }

            hullIndices.RemoveAt(hullIndices.Count - 1);
            
            for (int i = 0; i < hullIndices.Count; i++)
            {
                _hullPoints.Add(points[hullIndices[i]].transform);
            }
        }
        
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private float CrossProduct(Vector3 o, Vector3 a, Vector3 b)
        {
            return (a.x - o.x) * (b.z - o.z) - (a.z - o.z) * (b.x - o.x);
        }

        public void UpdateArea()
        {
            ComputeConvexHull();
            
            if (_hullPoints.Count == 0)
            {
                shapeController.spline.Clear();
                _isDirty = false;
                return;
            }

            shapeController.spline.Clear();
            CalculateAreaCenter();

            int count = _hullPoints.Count;
            for (int i = 0; i < count; i++)
            {
                var hullPoint = _hullPoints[i];
                Vector3 pos = hullPoint.position;
                
                var areaFormPoint = hullPoint.GetComponent<AreaFormPoint>();
                if (areaFormPoint != null)
                {
                    areaFormPoint.BakedPosition = pos;
                }

                Vector3 dir = (pos - _areaCenter).normalized;
                if (dir.sqrMagnitude < 0.0001f)
                    dir = Vector3.up;

                Vector3 offsetPos = pos + dir * offsetRadius;
                offsetPos.y = offsetPos.z;
                offsetPos.z = 0;
                
                shapeController.spline.InsertPointAt(i, offsetPos);
                shapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            }

            shapeController.BakeMesh();
            shapeController.RefreshSpriteShape();
            _isDirty = false;
        }

        [ContextMenu("Update Zone")]
        public void UpdateZoneEditor()
        {
            UpdateArea();
        }
    }
}
