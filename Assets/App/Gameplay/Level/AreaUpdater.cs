using System;
using System.Collections.Generic;
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
        private List<AreaFormPoint> _sortedPoints = new();
        private List<AreaFormPoint> _insidePoints = new();

        private void Update()
        {
            foreach (var point in points)
            {
                if (Vector3.Distance(point.BakedPosition, point.transform.position) > pointPositionThreshold)
                {
                    UpdateArea();
                    return;
                }
            }
        }

        public void AddPoint(AreaFormPoint formPoint)
        {
            points.Add(formPoint);
            UpdateArea();
        }

        public void RemovePoint(AreaFormPoint formPoint)
        {
            points.Remove(formPoint);
            UpdateArea();
        }

        Vector3 GetAreaCenter()
        {
            Vector3 sum = Vector3.zero;
            foreach (var point in _insidePoints)
            {
                sum += point.transform.position;
            }

            return sum / _insidePoints.Count;
        }

        private void ComputeConvexHull(List<AreaFormPoint> inputPoints)
        {
            _insidePoints.Clear();
            if (inputPoints.Count < 3)
            {
                _insidePoints.AddRange(inputPoints);
                return;
            }
            
            _sortedPoints.AddRange(inputPoints);
            _sortedPoints.Sort((a, b) =>
            {
                Vector3 posA = a.transform.position;
                Vector3 posB = b.transform.position;

                if (Mathf.Approximately(posA.x, posB.x))
                    return posA.z.CompareTo(posB.z);
                return posA.x.CompareTo(posB.x);
            });

            _insidePoints.Clear();
            int lowerCount = 0;
            
            for (int i = 0; i < _sortedPoints.Count; i++)
            {
                while (lowerCount >= 2 && CrossProduct(
                           _insidePoints[lowerCount - 2].transform.position,
                           _insidePoints[lowerCount - 1].transform.position,
                           _sortedPoints[i].transform.position) <= 0)
                {
                    _insidePoints.RemoveAt(_insidePoints.Count - 1);
                    lowerCount--;
                }

                _insidePoints.Add(_sortedPoints[i]);
                lowerCount++;
            }
            
            int upperStart = _insidePoints.Count;
            for (int i = _sortedPoints.Count - 2; i >= 0; i--)
            {
                while (_insidePoints.Count - upperStart >= 1 && CrossProduct(
                           _insidePoints[^2].transform.position,
                           _insidePoints[^1].transform.position,
                           _sortedPoints[i].transform.position) <= 0)
                {
                    _insidePoints.RemoveAt(_insidePoints.Count - 1);
                }

                _insidePoints.Add(_sortedPoints[i]);
            }

            _insidePoints.RemoveAt(_insidePoints.Count - 1);

            return;
        }
        
        private float CrossProduct(Vector3 o, Vector3 a, Vector3 b)
        {
            return (a.x - o.x) * (b.z - o.z) - (a.z - o.z) * (b.x - o.x);
        }

        public void UpdateArea()
        {
            ComputeConvexHull(points);
            if (_insidePoints.Count == 0)
            {
                shapeController.spline.Clear();
                return;
            }

            shapeController.spline.Clear();
            
            Vector3 areaCenter = GetAreaCenter();

            for (int i = 0; i < _insidePoints.Count; i++)
            {
                Vector3 pos = _insidePoints[i].transform.position;
                _insidePoints[i].BakedPosition = pos;
                Vector3 dir = (pos - areaCenter).normalized;

                if (dir == Vector3.zero)
                    dir = Vector3.up;

                Vector3 offsetPos = pos + dir * offsetRadius;
                offsetPos.y = offsetPos.z;
                offsetPos.z = 0;
                shapeController.spline.InsertPointAt(i, offsetPos);
                shapeController.spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            }

            shapeController.BakeMesh();
            shapeController.RefreshSpriteShape();
        }

        [ContextMenu("Update Zone")]
        public void UpdateZoneEditor()
        {
            UpdateArea();
        }
    }
}