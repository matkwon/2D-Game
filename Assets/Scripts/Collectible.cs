using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject audioGameObject;
    protected AudioSource audioSource;

    private float amplitude = 0.2f;
    private float frequency = 1f;
    Vector3 posOrigin = new Vector3();
    Vector3 tempPos = new Vector3();

    protected void Start()
    {
        posOrigin = transform.position;
        audioSource = audioGameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        tempPos = posOrigin;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
}
