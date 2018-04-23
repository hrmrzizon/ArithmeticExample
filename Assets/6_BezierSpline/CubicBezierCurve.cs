namespace Example.Arithmetic.Spline
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class CubicBezierCurve 
    {
        public static Vector3 GetPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            t = Mathf.Clamp01(t);
            float mt = 1 - t;

            return mt * mt * mt * p0 + 3 * mt * mt * t * p1 + 3 * mt * t * t * p2 + t * t * t * p3;
        }

        public static Vector3 GetDirection(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            t = Mathf.Clamp01(t);
            float mt = 1 - t;

            return 3 * mt * mt * (p1 - p0) + 6 * mt * t * (p2 - p1) + 3 * t * t * (p3 - p2);
        }
    }
}
