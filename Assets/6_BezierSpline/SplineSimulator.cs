namespace Example.Arithmetic.Spline
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [ExecuteInEditMode]
    public class SplineSimulator : MonoBehaviour
    {
        [Range(0, 1)]
        public float t;
        
        public Vector3 point0;
        public Vector3 point1;
        public Vector3 point2;
        public Vector3 point3;

        public Transform target0;
        public Transform target1;
        public Transform target2;
        public Transform target3;

        void ApplyCurve()
        {
            transform.position = CubicBezierCurve.GetPoint(t, point0, point1, point2, point3);

            target0.position = point0;
            target1.position = point1;
            target2.position = point2;
            target3.position = point3;
        }
        
        private void Update()
        {
            ApplyCurve();
        }
    }
}