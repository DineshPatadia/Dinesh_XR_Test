using UnityEngine;

public class ScreenFaderScript : MonoBehaviour
{
    [SerializeField]
    LayerMask collisionLayers;

    [SerializeField]
    float fadingSpeed;

    [SerializeField]
    float sphereSize = 0.15f;

    Material faderMat;

    bool isFaded = false;

    private void Awake()
    {
        faderMat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if (Physics.CheckSphere(transform.position, sphereSize,collisionLayers, QueryTriggerInteraction.Ignore))
        {
            FadeScreen(1f);
            isFaded = true;
        }
        else
        {
            if (!isFaded)
                return;
            FadeScreen(0);
        }
    }


    void FadeScreen(float target)
    {
        var fadeValue = Mathf.MoveTowards(faderMat.color.a, target, Time.deltaTime * fadingSpeed);
        faderMat.color = new Color(0, 0, 0, fadeValue);

        if (fadeValue <= 0.01f)
            isFaded = false;
    }
}
