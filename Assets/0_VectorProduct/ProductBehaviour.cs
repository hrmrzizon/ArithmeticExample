namespace Example.Arithmetic.VectorProduct
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [ExecuteInEditMode]
    public class ProductBehaviour : MonoBehaviour
    {
        [Header("Product options")][Space]
        public bool productDirectly;

        [SerializeField]
        public Vector3 direction = Vector3.one;

        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }

        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            return 
                new Vector3(
                    lhs.y * rhs.z - rhs.y * lhs.z, 
                    rhs.x * lhs.z - lhs.x * rhs.z, 
                    lhs.x * rhs.y - rhs.x * lhs.y
                    );
        }

        void Update()
        {
            direction = direction.normalized;
            Vector3 forward = transform.forward;

            if (!productDirectly)
                Debug.LogFormat("DOT: {0}ㆍ{1} = {2}", transform.forward, direction, Vector3.Dot(transform.forward, direction));
            else
                Debug.LogFormat("DOT: {0}ㆍ{1} = {2}", forward, direction, Dot(forward, direction));

            if (!productDirectly)
                Debug.LogFormat("CROSS: {0}ㆍ{1} = {2}, Size : {3}", transform.forward, direction, Vector3.Cross(transform.forward, direction), Vector3.Cross(transform.forward, direction).sqrMagnitude);
            else
                Debug.LogFormat("CROSS: {0}ㆍ{1} = {2}, Size : {3}", transform.forward, direction, Cross(transform.forward, direction), Cross(transform.forward, direction).sqrMagnitude);
        }
    }
}
