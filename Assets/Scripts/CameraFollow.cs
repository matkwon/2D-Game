using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.5f;
    public Vector3 offset;
    public float shakeAmplitude = 0.01f;
    public float shakeFrequency = 1f;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);

        float shakeOffsetX = Mathf.PerlinNoise(Time.time * shakeFrequency, 0) * shakeAmplitude - (shakeAmplitude / 2);
        float shakeOffsetY = Mathf.PerlinNoise(0, Time.time * shakeFrequency) * shakeAmplitude - (shakeAmplitude / 2);
        Vector3 shakeOffset = new Vector3(shakeOffsetX, shakeOffsetY, 0);

        transform.position = smoothedPosition + shakeOffset;
    }
}