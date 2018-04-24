namespace Example.Arithmetic.Transformation.HierarchicalMatrix
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [ExecuteInEditMode]
    public class HierarchicalMatrixCalculate : MonoBehaviour
    {
        private void OnEnable()
        {
            Matrix4x4 matrix = MatrixUtility.GetHierarchicalMatrix(transform), originMatrix = transform.localToWorldMatrix;
            Debug.Log(matrix);
            Debug.Log(originMatrix);

            Debug.LogFormat("Transform, Origin : {0}, Custom : {1}", transform.position, matrix.ApplyPosition(Vector3.zero));
        }
    }
}