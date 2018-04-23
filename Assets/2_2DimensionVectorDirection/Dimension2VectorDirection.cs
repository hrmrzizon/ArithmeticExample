namespace Example.Arithmetic.Dimension2ClockWiseCheck
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [ExecuteInEditMode]
    public class Dimension2VectorDirection : MonoBehaviour
    {
        public Transform lhs, rhs;
        
        public static string JudgeAsString(float zValue)
        {
            return  zValue == 0 ? "Same direction" :
                    zValue > 0 ? "Clockwise" : "CounterClockwise";
        }

        private void Update()
        {
            if (lhs == null || rhs == null) return;

            Debug.LogFormat("Vector : {0}, {1}", Vector3.Cross(lhs.position, rhs.position), JudgeAsString(Vector3.Cross(lhs.position, rhs.position).z));
        }
    }
}
