using UnityEngine;

public class CheckHMDCollision : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;

    [SerializeField]
    float sphereSize = 0.15f;

    // Update is called once per frame
    void Update()
    {
        if(Physics.CheckSphere(transform.position, sphereSize, layerMask, QueryTriggerInteraction.Ignore))
        {
            Debug.Log(transform.parent.name);
            transform.parent.position = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, sphereSize);
    }
}
