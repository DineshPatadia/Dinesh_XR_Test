using Deform;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField]
    Deformable deformable;

    [SerializeField]
    GameObject deformer;

    [SerializeField]
    Transform sparkle;

    bool isPlayed;

   
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name.Equals("MetalSheet"))
        {
            //Debug.Log(collision.gameObject.name);
            if (!isPlayed)
            {
                isPlayed = true;
                Vector3 CollisionPoint = collision.GetContact(0).point;

                GameObject instantiateDeformer = Instantiate(deformer, deformable.transform);
                instantiateDeformer.transform.position = CollisionPoint;
                sparkle.position = CollisionPoint;

                sparkle.GetComponent<ParticleSystem>().Play();

                deformable.AddDeformer(instantiateDeformer.GetComponent<Deformer>());
            }



        }
        else
        {
            Debug.Log("Exit");
            isPlayed = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject.name.Equals("MetalSheet"))
        //{
            //Debug.Log(collision.gameObject.name);
            //isPlayed = false;
        //}
    }




}
