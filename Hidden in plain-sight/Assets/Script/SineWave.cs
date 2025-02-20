using UnityEngine;

public class SineWaveMovement : MonoBehaviour
{
    public float amplitude = 1f; // Height of the sine wave
    public float frequency = 1f; // Speed of the oscillation
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPos + new Vector3(0, yOffset, 0);
    }
}