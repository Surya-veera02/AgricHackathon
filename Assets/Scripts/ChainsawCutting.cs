using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ChainsawCutting : MonoBehaviour
{
    [Header("XR Controller Settings")]
    public XRController rightController;            // Reference to your right hand XR Controller
    public InputHelpers.Button activationButton = InputHelpers.Button.Trigger;
    [Range(0, 1)] public float activationThreshold = 0.1f;

    [Header("Chainsaw Settings")]
    public AudioSource chainsawAudio;               // Reference to the AudioSource with the chainsaw sound
    public float cutTimeRequired = 5.0f;            // Time needed in contact to cut the tree

    private float contactTimer = 0.0f;              // How long we've been in contact with a tree while trigger is held
    private GameObject currentTree = null;          // The tree currently in contact
    private bool isTriggerHeld = false;             // Whether the trigger is being pressed

    void Update()
    {
        // 1. Check if the player is pulling the trigger
        if (rightController && rightController.inputDevice.isValid)
        {
            // Checks if the specified button (Trigger) is pressed beyond the activationThreshold
            bool isActivated = false;
            rightController.inputDevice.IsPressed(activationButton, out isActivated, activationThreshold);

            // Start/stop chainsaw sound based on trigger press
            if (isActivated && !chainsawAudio.isPlaying)
            {
                chainsawAudio.Play();
            }
            else if (!isActivated && chainsawAudio.isPlaying)
            {
                chainsawAudio.Stop();
            }

            isTriggerHeld = isActivated;
        }

        // 2. If the chainsaw is in contact with a tree and the trigger is held
        if (currentTree != null && isTriggerHeld)
        {
            contactTimer += Time.deltaTime;

            // 3. Check if we've been in contact long enough to cut the tree
            if (contactTimer >= cutTimeRequired)
            {
                CutDownTree(currentTree);
                // Reset timer to prevent re-cutting the same tree instantly
                contactTimer = 0f;
            }
        }
        else
        {
            // Reset the contact timer if not actively cutting
            contactTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If we collide with a tree, store a reference to it
        if (other.CompareTag("Tree"))
        {
            currentTree = other.gameObject;
            contactTimer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If we exit the tree collider, reset the reference
        if (other.CompareTag("Tree"))
        {
            if (other.gameObject == currentTree)
            {
                currentTree = null;
            }
            contactTimer = 0f;
        }
    }

    private void CutDownTree(GameObject tree)
    {
        // Example logic to "cut down" the tree:
        // 1. Disable or remove the tree. 
        // 2. Or apply physics for the tree to fall.
        // 3. Or call a function on a separate Tree script that handles falling.

        Debug.Log($"Tree {tree.name} has been cut down!");

        // Option A: If you have a Tree script with a Fall() method:
        // tree.GetComponent<Tree>().Fall();

        // Option B: Just deactivate the tree
        // tree.SetActive(false);

        // For a more realistic approach, add a Rigidbody and apply force to simulate a fall.
        Rigidbody rb = tree.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(Vector3.forward * 50.0f, ForceMode.Impulse);
        }

        // Clear reference so you don't keep cutting the same tree
        currentTree = null;
    }
}
