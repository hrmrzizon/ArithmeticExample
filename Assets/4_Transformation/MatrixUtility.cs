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
        public static void SetColumns(ref Matrix4x4 matrix, Vector4 column1, Vector4 column2, Vector4 column3, Vector4 column4)
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

        public static void SetRows(ref Matrix4x4 matrix, Vector4 row1, Vector4 row2, Vector4 row3, Vector4 row4)
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

        public static void SetTQRS(ref Matrix4x4 matrix, Vector3 translate, Quaternion q, Vector3 scale)
        {
            float   qx2 = q.x * q.x, qy2 = q.y * q.y, qz2 = q.z * q.z, qw2 = q.w * q.w;

            matrix.SetRow(0, new Vector4(1 - 2 * (qy2 + qz2)            * scale.x,  2 * (q.x * q.y - q.z * q.w)    * scale.y,   2 * (q.x * q.z + q.y * q.w) * scale.z,  translate.x));
            matrix.SetRow(1, new Vector4(2 * (q.x * q.y + q.z * q.w)    * scale.x,  1 - 2 * (qx2 + qz2)            * scale.y,   2 * (q.y * q.z - q.x * q.w) * scale.z,  translate.y));
            matrix.SetRow(2, new Vector4(2 * (q.x * q.z - q.y * q.w)    * scale.x,  2 * (q.y * q.z + q.x * q.w)    * scale.y,   1 - 2 * (qx2 + qy2)         * scale.z,  translate.z));
            matrix.SetRow(3, new Vector4(0f,                                        0f,                                         0f,                                     1f         ));
        }

        public static void SetTERS(ref Matrix4x4 matrix, Vector3 translate, Vector3 eulerAngle, Vector3 scale)
        {
            float   cosx = Mathf.Cos(eulerAngle.x * Mathf.Deg2Rad), sinx = Mathf.Sin(eulerAngle.x * Mathf.Deg2Rad),
                    cosy = Mathf.Cos(eulerAngle.y * Mathf.Deg2Rad), siny = Mathf.Sin(eulerAngle.y * Mathf.Deg2Rad),
                    cosz = Mathf.Cos(eulerAngle.z * Mathf.Deg2Rad), sinz = Mathf.Sin(eulerAngle.z * Mathf.Deg2Rad);

            // y -> x -> z
            matrix.SetRow(0, new Vector4(cosy * cosz + sinx * siny * sinz,  sinx * siny * cosz - cosy * sinz,   cosx * siny,    translate.x));
            matrix.SetRow(1, new Vector4(cosx * sinz,                       cosx * cosz,                        -sinx,          translate.y));
            matrix.SetRow(2, new Vector4(sinx * cosy * sinz - siny * cosz,  siny * sinz + sinx * cosy * cosz,   cosx * cosy,    translate.z));
            matrix.SetRow(3, new Vector4(0f,                                0f,                                 0f,             1f));
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