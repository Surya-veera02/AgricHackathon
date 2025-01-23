using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationTrigger : MonoBehaviour
{
    public AudioSource narrationAudio;    // Audio source for the narration
    public bool hasPlayed = false;        // Prevent multiple triggers

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            // Play narration audio
            if (narrationAudio != null)
            {
                narrationAudio.pitch = 1.2f;
                narrationAudio.Play();
                Debug.Log("Playing narration audio.");
                hasPlayed = true; // Ensure the narration plays only once
            }
            else
            {
                Debug.LogWarning("Narration AudioSource is not assigned.");
            }
        }
    }
}

