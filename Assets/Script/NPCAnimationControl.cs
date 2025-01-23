using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationControl : MonoBehaviour
{
    public Animator npcAnimator; // Assign the NPC's Animator component
    public AudioSource npcAudioSource; // Assign the NPC's Audio Source

    private bool isIdle = false; // Track the current state

    private void Start()
    {
        if (npcAnimator != null)
        {
            // Start with the chopping wood animation by default
            npcAnimator.SetBool("IsIdle", false);
        }
        else
        {
            Debug.LogWarning("Animator is not assigned to the NPCAnimationControl script.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set IsIdle to true when the player enters
            if (npcAnimator != null)
            {
                isIdle = true;
                npcAnimator.SetBool("IsIdle", isIdle);
                Debug.Log("Player entered NPC trigger area. Switching to idle state.");
            }

            // Play the audio narration
            if (npcAudioSource != null)
            {
                npcAudioSource.Play();
                Debug.Log("Playing NPC narration ");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set IsIdle to false when the player exits
            if (npcAnimator != null)
            {
                isIdle = false;
                npcAnimator.SetBool("IsIdle", isIdle);
                Debug.Log("Player left NPC trigger area. Resuming chopping animation.");
            }

            // Stop the audio narration if it's playing
            if (npcAudioSource != null && npcAudioSource.isPlaying)
            {
                npcAudioSource.Stop();
                Debug.Log("Stopping NPC narration.");
            }
        }
    }
}
