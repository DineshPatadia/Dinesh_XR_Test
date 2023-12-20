
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerBoundaryScript : MonoBehaviour
{
    [SerializeField]
    Transform player;

    private void OnTriggerEnter(Collider collider)
    {
        //Debug.Log(collider.tag);
        if (collider.CompareTag("Player"))
        {
            //Debug.Log(collider.transform.position);
            ChangePlayerPosition();
        }
    }

    void ChangePlayerPosition()
    {
        player.position = new Vector3(1f, player.position.y, player.position.y);
    }
}
