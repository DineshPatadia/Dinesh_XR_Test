using UnityEngine;

public class BasketScoreScript : MonoBehaviour
{
    public delegate void IncrementScore();
    public static event IncrementScore incrementScore;

    private void OnTriggerEnter(Collider other)
    {
        if(incrementScore!=null)
        {
            incrementScore();
        }

        
    }
}
