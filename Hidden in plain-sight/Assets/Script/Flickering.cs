using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private Light flickerLight;
    public float minIntensity = 175f;
    public float maxIntensity = 180f;
    public float flickerSpeed = 0.1f;

    void Start()
    {
        flickerLight = GetComponent<Light>();
    }

    void Update()
    {
        if (flickerLight != null)
        {
            flickerLight.intensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
