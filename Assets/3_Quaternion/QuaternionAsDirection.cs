namespace Example.Arithmetic.Quaternion
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// https://www.essentialmath.com/GDC2013/GDC13_quaternions_final.pdf
    /// </summary>
    [ExecuteInEditMode]
    public class QuaternionAsDirection : MonoBehaviour
    {
        [Header("Calculate options")] [Space]
        public bool calculateDirectly;
        public Vector3 srcDirection = new Vector3(1, 0, 0);
        public Vector3 dstDirection = new Vector3(1, 0, 0);

        public static Quaternion NormalizeQuaternion(Quaternion quat)
        {
            float len = Mathf.Sqrt(quat.x * quat.x + quat.y * quat.y + quat.z * quat.z + quat.w * quat.w);

            quat.x /= len;
            quat.y /= len;
            quat.z /= len;
            quat.w /= len;

            return quat;
        }

        void Rotate()
        {
            srcDirection.Normalize();
            dstDirection.Normalize();

            if (!calculateDirectly)
                transform.rotation = Quaternion.FromToRotation(srcDirection, dstDirection);
            else
            {
                Quaternion quat;

                float   dot = Vector3.Dot(srcDirection, dstDirection), 
                        scalar = Mathf.Sqrt((1 + dot) * 2f);

                Vector3 cross = Vector3.Cross(srcDirection, dstDirection);

                quat.x = cross.x / scalar;
                quat.y = cross.y / scalar;
                quat.z = cross.z / scalar;
                quat.w = scalar * 0.5f;

                quat = NormalizeQuaternion(quat);

                transform.rotation = quat;
            }
        }

        private void OnEnable()
        {
            Rotate();
        }
    }
}