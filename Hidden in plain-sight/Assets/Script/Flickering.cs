using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private Light flickerLight;
    public float minIntensity = 175f;
    public float maxIntensity = 180f;
    public float frequency = 1f; // How fast the sine wave oscillates
    private float elapsedTime = 0f;

    void Start()
    {
        flickerLight = GetComponent<Light>();
    }

    void Update()
    {
        if (flickerLight != null)
        {
            elapsedTime += Time.deltaTime;
            float sinValue = Mathf.Sin(elapsedTime * frequency * Mathf.PI * 2) * 0.5f + 0.5f; // Normalize sin wave to 0-1
            flickerLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, sinValue);
        }
    }
}