namespace Example.Arithmetic.Quaternion
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// https://www.essentialmath.com/GDC2013/GDC13_quaternions_final.pdf
    /// </summary>
    [ExecuteInEditMode]
    public class QuaternionAsAxisAngle : MonoBehaviour
    {
        [Header("Make options")][Space]
        public bool makeQuaternionDirectly;
        public bool conjugate;

        public float degree;
        public Vector3 direction;

        [Header("Apply position options")][Space]
        public bool rotatePosition;
        public bool setRotationDirectly;
        public Vector3 offset;

        public static Quaternion AngleAxis(float angleDegree, Vector3 direction, bool conjugate)
        {
            float sin = Mathf.Sin(angleDegree * Mathf.Deg2Rad * 0.5f) * (conjugate ? -1f : 1f), cos = Mathf.Cos(angleDegree * Mathf.Deg2Rad * 0.5f);
            return new Quaternion(sin * direction.x, sin * direction.y, sin * direction.z, cos);
        }

        public static Vector3 RotationToPosition(Quaternion rotation, Vector3 position)
        {
            Vector3 vectorPart = new Vector3(rotation.x, rotation.y, rotation.z);
            return
                position +
                2 * rotation.w * Vector3.Cross(vectorPart, position) +
                2 * Vector3.Cross(vectorPart, Vector3.Cross(vectorPart, position));
        }

        [ContextMenu("Apply")]
        private void Check()
        {
            direction = direction.normalized;

            Quaternion quat = transform.rotation;

            if (!makeQuaternionDirectly)
                quat = Quaternion.AngleAxis(degree, direction);
            else
                quat = AngleAxis(degree, direction, conjugate);

            transform.rotation = quat;

            if (rotatePosition)
            {
                if (!setRotationDirectly)
                    transform.position = quat * offset;
                else
                {
                    transform.position = RotationToPosition(quat, offset);
                }
            }
            else
                transform.position = offset;
        }

        private void OnEnable()
        {
            Check();
        }
    }
}
