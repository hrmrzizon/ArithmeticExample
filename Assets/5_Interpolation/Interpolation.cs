namespace Example.Arithmetic.Interpolation
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [ExecuteInEditMode]
    public class Interpolation : MonoBehaviour
    {
        public bool sphericalInterpolcation;

        public Vector3 position1;
        public Vector3 position2;

        [Range(0, 1)]
        public float t;

        public static Vector3 LinearInterpolation(Vector3 lhs, Vector3 rhs, float t)
        {
            t = Mathf.Clamp01(t);

            return lhs * (1 - t) + rhs * t;
        }

        public static Vector3 SphericalLinearInterpolation(Vector3 lhs, Vector3 rhs, float t)
        {
            t = Mathf.Clamp01(t);

            float  wholeRad = Mathf.Acos(Vector3.Dot(lhs.normalized, rhs.normalized)),
                   deltaRad = wholeRad * t;
            float sin = Mathf.Sin(wholeRad);

            return lhs * Mathf.Sin(wholeRad - deltaRad) / sin + rhs * Mathf.Sin(deltaRad) / sin;
        }

        void ApplyInterpolation()
        {
            if (!sphericalInterpolcation)
            {
                transform.position = LinearInterpolation(position1, position2, t);
            }
            else
            {
                transform.position = SphericalLinearInterpolation(position1, position2, t);
            }
        }

        private void Update()
        {
            ApplyInterpolation();
        }
    }
}