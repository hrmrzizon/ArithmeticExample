namespace Example.Arithmetic.Transformation
{
    using Example.Arithmetic.Quaternion;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [ExecuteInEditMode]
    public class TransformationMake : MonoBehaviour
    {
        public Transform target;

        public Vector3 position;

        public Vector3 translate;        
        public Vector3 scale;

        public bool selectEulerAngle;
        public Vector3 eulerAngle;
        public float angle;
        public Vector3 direction;

        Quaternion ToQuaternion(Vector3 eulerAngle)
        {
            return new Quaternion();
        }

        Vector3 ToEuler(Quaternion quaternion)
        {
            return new Vector3();
        }

        void ApplyMatrix()
        {
            Matrix4x4 matrix = Matrix4x4.identity;

            if (!selectEulerAngle)
            {
                angle = angle + Mathf.Floor(Mathf.Abs(angle / 180f)) * -Mathf.Sign(angle) * 360f;
                direction.Normalize();

                MatrixUtility.SetTQRS(out matrix, translate, QuaternionAsAxisAngle.AngleAxis(angle, direction, false), scale);
            }
            else
            {
                eulerAngle = eulerAngle + new Vector3(
                    Mathf.Floor(Mathf.Abs(eulerAngle.x / 180f)) * -Mathf.Sign(eulerAngle.x) * 360f, 
                    Mathf.Floor(Mathf.Abs(eulerAngle.y / 180f)) * -Mathf.Sign(eulerAngle.y) * 360f, 
                    Mathf.Floor(Mathf.Abs(eulerAngle.z / 180f)) * -Mathf.Sign(eulerAngle.z) * 360f
                    );

                Debug.Log(eulerAngle);

                MatrixUtility.SetTERS(out matrix, translate, eulerAngle, scale);
            }

            target.position = matrix.ApplyPosition(position);
        }

        private void OnEnable()
        {
            ApplyMatrix();
        }

        private void Update()
        {
            ApplyMatrix();
        }
    }
}
