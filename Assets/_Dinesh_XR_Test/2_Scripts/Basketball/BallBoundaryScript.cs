using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallBoundaryScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Ball"))
        {
            collider.GetComponent<XRGrabInteractable>().enabled = false;
        }

    }
}
