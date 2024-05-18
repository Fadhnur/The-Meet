using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource fireAudioSource;
    public Transform playerTransform;

    void Start()
    {
        if (fireAudioSource == null)
        {
            fireAudioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        float maxDistance = fireAudioSource.maxDistance;
        
        // Adjust volume based on distance (optional customization)
        fireAudioSource.volume = 1 - Mathf.Clamp01(distance / maxDistance);
    }
}
