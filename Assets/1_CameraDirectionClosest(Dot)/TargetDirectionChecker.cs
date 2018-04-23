namespace Example.Arithmetic.DirecitonCheck
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [ExecuteInEditMode]
    public class TargetDirectionChecker : MonoBehaviour
    {
        [SerializeField]
        public Transform target;

        void Update()
        {
            if (target == null) return;

            Vector3 distance = target.position - transform.position;
            float dot = Vector3.Dot(distance.normalized, transform.forward);

            Debug.LogFormat("Direction Similarity as Dot : {0}", dot);
            Debug.LogFormat("Direction Similarity as Linearized dot : {0}", Mathf.Sign(dot) * Mathf.Pow(Mathf.Abs(dot), 2f));
        }
    }
}
