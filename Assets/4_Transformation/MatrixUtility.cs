namespace Example.Arithmetic.Transformation
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Unity's matrix is column-major matrix.
    /// </summary>
    public static class MatrixUtility 
    {
        public static void GetTranslateFromMatrix(ref Matrix4x4 transformMatrix, out Vector3 translate)
        {
            translate = new Vector3(transformMatrix.m03, transformMatrix.m13, transformMatrix.m23);
        }

        public static void GetScaleFromMatrix(ref Matrix4x4 transformMatrix, out Vector3 scale)
        {
            scale = new Vector3(
                Mathf.Sqrt(transformMatrix.m00 * transformMatrix.m00 + transformMatrix.m01 * transformMatrix.m01 + transformMatrix.m02 * transformMatrix.m02), 
                Mathf.Sqrt(transformMatrix.m10 * transformMatrix.m10 + transformMatrix.m11 * transformMatrix.m11 + transformMatrix.m12 * transformMatrix.m12), 
                Mathf.Sqrt(transformMatrix.m20 * transformMatrix.m20 + transformMatrix.m21 * transformMatrix.m21 + transformMatrix.m22 * transformMatrix.m22)
                );
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="rot"></param>
        public static void GetQuaternionFromMatrix(ref Matrix4x4 mat, out Quaternion rot)
        {
            float tr = mat.m00 + mat.m11 + mat.m22;

            if (tr > 0)
            {
                float s = Mathf.Sqrt(tr + 1f);
                rot.w = 0.5f * s;
                s = 0.5f / s;
                rot.x = (mat.m21 - mat.m12) * s;
                rot.y = (mat.m02 - mat.m20) * s;
                rot.z = (mat.m10 - mat.m01) * s;
            }
            else if ((mat.m00 > mat.m11) && (mat.m00 > mat.m22))
            {
                float s = Mathf.Sqrt(1f + mat.m00 - mat.m11 - mat.m22) * 2;
                rot.w = (mat.m21 - mat.m12) / s;
                rot.x = 0.25f * s;
                rot.y = (mat.m01 + mat.m10) / s;
                rot.z = (mat.m02 + mat.m20) / s; 
            }
            else if (mat.m11 > mat.m22)
            {
                float s = Mathf.Sqrt(1f + mat.m11 - mat.m00 - mat.m22) * 2;
                rot.w = (mat.m02 - mat.m20) / s;
                rot.x = (mat.m01 + mat.m10) / s;
                rot.y = 0.25f * s; 
                rot.z = (mat.m12 + mat.m21) / s;
            }
            else
            {
                float s = Mathf.Sqrt(1f + mat.m22 - mat.m00 - mat.m11 ) * 2;
                rot.w = (mat.m10 - mat.m01) / s;
                rot.x = (mat.m02 + mat.m20) / s;
                rot.y = (mat.m12 + mat.m21) / s;
                rot.z = 0.25f * s; 
            }
        }

        public static Matrix4x4 Multiply(Matrix4x4 mat1, Matrix4x4 mat2)
        {
            return new Matrix4x4(
                new Vector4(Vector4.Dot(mat1.GetRow(0), mat2.GetColumn(0)), Vector4.Dot(mat1.GetRow(1), mat2.GetColumn(0)), Vector4.Dot(mat1.GetRow(2), mat2.GetColumn(0)), Vector4.Dot(mat1.GetRow(3), mat2.GetColumn(0))),
                new Vector4(Vector4.Dot(mat1.GetRow(0), mat2.GetColumn(1)), Vector4.Dot(mat1.GetRow(1), mat2.GetColumn(1)), Vector4.Dot(mat1.GetRow(2), mat2.GetColumn(1)), Vector4.Dot(mat1.GetRow(3), mat2.GetColumn(1))),
                new Vector4(Vector4.Dot(mat1.GetRow(0), mat2.GetColumn(2)), Vector4.Dot(mat1.GetRow(1), mat2.GetColumn(2)), Vector4.Dot(mat1.GetRow(2), mat2.GetColumn(2)), Vector4.Dot(mat1.GetRow(3), mat2.GetColumn(2))),
                new Vector4(Vector4.Dot(mat1.GetRow(0), mat2.GetColumn(3)), Vector4.Dot(mat1.GetRow(1), mat2.GetColumn(3)), Vector4.Dot(mat1.GetRow(2), mat2.GetColumn(3)), Vector4.Dot(mat1.GetRow(3), mat2.GetColumn(3)))
                );
        }

        public static Matrix4x4 GetHierarchicalMatrix(Transform transform)
        {
            Matrix4x4 matrix;
            SetTQRS(out matrix, transform.localPosition, transform.localRotation, transform.localScale);
            //SetTERS(out matrix, transform.localPosition, transform.localEulerAngles, transform.localScale);

            if (transform.parent == null)
                return matrix;
            else
                return Multiply(GetHierarchicalMatrix(transform.parent), matrix);
        }

        public static void SetColumns(out Matrix4x4 matrix, Vector4 column1, Vector4 column2, Vector4 column3, Vector4 column4)
        {
            matrix.m00 = column1.x;
            matrix.m10 = column1.y;
            matrix.m20 = column1.z;
            matrix.m30 = column1.w;

            matrix.m01 = column2.x;
            matrix.m11 = column2.y;
            matrix.m21 = column2.z;
            matrix.m31 = column2.w;

            matrix.m02 = column3.x;
            matrix.m12 = column3.y;
            matrix.m22 = column3.z;
            matrix.m32 = column3.w;

            matrix.m03 = column4.x;
            matrix.m13 = column4.y;
            matrix.m23 = column4.z;
            matrix.m33 = column4.w;
        }

        public static void SetRows(out Matrix4x4 matrix, Vector4 row1, Vector4 row2, Vector4 row3, Vector4 row4)
        {        
            matrix.m00 = row1.x;
            matrix.m01 = row1.y;
            matrix.m02 = row1.z;
            matrix.m03 = row1.w;

            matrix.m10 = row2.x;
            matrix.m11 = row2.y;
            matrix.m12 = row2.z;
            matrix.m13 = row2.w;

            matrix.m20 = row3.x;
            matrix.m21 = row3.y;
            matrix.m22 = row3.z;
            matrix.m23 = row3.w;

            matrix.m30 = row4.x;
            matrix.m31 = row4.y;
            matrix.m32 = row4.z;
            matrix.m33 = row4.w;
        }

        public static void SetTQRS(out Matrix4x4 outMatrix, Vector3 translate, Quaternion q, Vector3 scale)
        {
            Matrix4x4 matrix = new Matrix4x4();

            float   qx2 = q.x * q.x, qy2 = q.y * q.y, qz2 = q.z * q.z/*, qw2 = q.w * q.w*/;

            matrix.SetRow(0, new Vector4((1 - 2 * (qy2 + qz2))          * scale.x,  (2 * (q.x * q.y - q.z * q.w))   * scale.y,  (2 * (q.x * q.z + q.y * q.w))   * scale.z,  translate.x));
            matrix.SetRow(1, new Vector4((2 * (q.x * q.y + q.z * q.w))  * scale.x,  (1 - 2 * (qx2 + qz2))           * scale.y,  (2 * (q.y * q.z - q.x * q.w))   * scale.z,  translate.y));
            matrix.SetRow(2, new Vector4((2 * (q.x * q.z - q.y * q.w))  * scale.x,  (2 * (q.y * q.z + q.x * q.w))   * scale.y,  (1 - 2 * (qx2 + qy2))           * scale.z,  translate.z));
            matrix.SetRow(3, new Vector4(0f,                                        0f,                                         0f,                                         1f         ));

            outMatrix = matrix;
        }

        public static void SetTERS(out Matrix4x4 outMatrix, Vector3 translate, Vector3 eulerAngle, Vector3 scale)
        {
            Matrix4x4 matrix = new Matrix4x4();

            float   cosx = Mathf.Cos(eulerAngle.x * Mathf.Deg2Rad), sinx = Mathf.Sin(eulerAngle.x * Mathf.Deg2Rad),
                    cosy = Mathf.Cos(eulerAngle.y * Mathf.Deg2Rad), siny = Mathf.Sin(eulerAngle.y * Mathf.Deg2Rad),
                    cosz = Mathf.Cos(eulerAngle.z * Mathf.Deg2Rad), sinz = Mathf.Sin(eulerAngle.z * Mathf.Deg2Rad);

            // y -> x -> z
            matrix.SetRow(0, new Vector4(cosy * cosz + sinx * siny * sinz   * scale.x,  sinx * siny * cosz - cosy * sinz    * scale.x,  cosx * siny * scale.x,  translate.x));
            matrix.SetRow(1, new Vector4(cosx * sinz                        * scale.y,  cosx * cosz                         * scale.y,  -sinx       * scale.y,  translate.y));
            matrix.SetRow(2, new Vector4(sinx * cosy * sinz - siny * cosz   * scale.z,  siny * sinz + sinx * cosy * cosz    * scale.z,  cosx * cosy * scale.z,  translate.z));
            matrix.SetRow(3, new Vector4(0f,                                            0f,                                             0f,                     1f));

            outMatrix = matrix;
        }

        public static Vector4 ApplyVector(this Matrix4x4 matrix, Vector4 vector)
        {
            return new Vector4(
                Vector4.Dot(matrix.GetRow(0), vector), 
                Vector4.Dot(matrix.GetRow(1), vector), 
                Vector4.Dot(matrix.GetRow(2), vector), 
                Vector4.Dot(matrix.GetRow(3), vector)
                );
        }

        public static Vector3 ApplyDirection(this Matrix4x4 matrix, Vector3 direction)
        {
            return ApplyVector(matrix, new Vector4(direction.x, direction.y, direction.z, 0));
        }

        public static Vector3 ApplyPosition(this Matrix4x4 matrix, Vector3 position)
        {
            return ApplyVector(matrix, new Vector4(position.x, position.y, position.z, 1));
        }
    }
}