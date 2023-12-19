
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerBoundaryScript : MonoBehaviour
{
    [SerializeField]
    Transform player;

    Vector3 playerInitialPos;

    private void Awake()
    {
        playerInitialPos = player.position;
        //Debug.Log(playerInitialPos);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.tag);
        if (collider.CompareTag("Player"))
        {
            //Debug.Log(collider.transform.position);
            ChangePlayerPosition();
        }

        if(collider.CompareTag("Ball"))
        {
            collider.GetComponent<XRGrabInteractable>().enabled = false;  
        }

    }

    void ChangePlayerPosition()
    {
        player.position = new Vector3(1f, player.position.y, player.position.y);
    }
}
