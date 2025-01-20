//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.XR.Interaction.Toolkit;

//public class ChainsawCutting : MonoBehaviour
//{
//    [Header("Chainsaw Input")]
//    public InputActionReference chainsawTriggerReference; // Action-based input for the right trigger

//    [Header("Chainsaw Settings")]
//    public AudioSource chainsawAudio;               // AudioSource for chainsaw sound
//    public float cutTimeRequired = 5.0f;            // Time required to cut a tree

//    private float contactTimer = 0.0f;              // Tracks time in contact with tree
//    private GameObject currentTree = null;          // Current tree being cut

//    void Update()
//    {
//        // Read the trigger value from the InputActionReference
//        float triggerValue = chainsawTriggerReference.action.ReadValue<float>();
//        bool isTriggerPressed = triggerValue > 0.1f;

//        // Handle chainsaw audio (on/off)
//        if (isTriggerPressed && !chainsawAudio.isPlaying)
//        {
//            chainsawAudio.Play();
//        }
//        else if (!isTriggerPressed && chainsawAudio.isPlaying)
//        {
//            chainsawAudio.Stop();
//        }

//        // Cutting logic: if the chainsaw is in contact with a tree and the trigger is pressed
//        if (currentTree != null && isTriggerPressed)
//        {
//            contactTimer += Time.deltaTime;

//            if (contactTimer >= cutTimeRequired)
//            {
//                CutDownTree(currentTree);
//                contactTimer = 0f; // Reset the timer
//            }
//        }
//        else
//        {
//            contactTimer = 0f; // Reset timer if not actively cutting
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        // Detect tree collision
//        if (other.CompareTag("Tree"))
//        {
//            currentTree = other.gameObject;
//            contactTimer = 0f;
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        // Clear tree reference on exit
//        if (other.CompareTag("Tree") && other.gameObject == currentTree)
//        {
//            currentTree = null;
//            contactTimer = 0f;
//        }
//    }

//    private void CutDownTree(GameObject tree)
//    {
//        Debug.Log($"Tree {tree.name} has been cut down!");

//        // Apply physics to simulate the tree falling
//        Rigidbody rb = tree.GetComponent<Rigidbody>();
//        if (rb != null)
//        {
//            rb.isKinematic = false;
//            rb.AddForce(Vector3.forward * 50.0f, ForceMode.Impulse);
//        }

//        // Clear the current tree reference
//        currentTree = null;
//    }
//}
using UnityEngine;
using UnityEngine.InputSystem;

public class ChainsawCutting : MonoBehaviour
{
    [Header("Chainsaw Input")]
    public InputActionReference chainsawTriggerReference; // Input for the right trigger

    [Header("Chainsaw Settings")]
    public AudioSource chainsawAudio;               // Chainsaw sound
    public float cutTimeRequired = 5.0f;            // Time required to cut a tree
    public ParticleSystem cuttingEffect;           // Optional particle effect (e.g., sawdust)

    private float contactTimer = 0.0f;              // Timer for cutting
    private GameObject currentTree = null;          // Reference to the tree being cut
    private bool isTriggerHeld = false;             // Whether the chainsaw trigger is pressed

    void Update()
    {
        // Read trigger input
        float triggerValue = chainsawTriggerReference.action.ReadValue<float>();
        isTriggerHeld = triggerValue > 0.1f;

        // Play or stop chainsaw audio based on trigger input
        if (isTriggerHeld && !chainsawAudio.isPlaying)
        {
            chainsawAudio.Play();
        }
        else if (!isTriggerHeld && chainsawAudio.isPlaying)
        {
            chainsawAudio.Stop();
        }

        // Cutting logic: Chainsaw must be in contact with a tree and trigger must be held
        if (currentTree != null && isTriggerHeld)
        {
            contactTimer += Time.deltaTime;

            // Optional: Trigger cutting effect
            if (cuttingEffect && !cuttingEffect.isPlaying)
            {
                cuttingEffect.Play();
            }

            // Check if the tree has been cut down
            if (contactTimer >= cutTimeRequired)
            {
                CutDownTree(currentTree);
                contactTimer = 0f; // Reset timer
            }
        }
        else
        {
            // Reset cutting effect and timer if not actively cutting
            if (cuttingEffect && cuttingEffect.isPlaying)
            {
                cuttingEffect.Stop();
            }
            contactTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detect if the chainsaw is in contact with a tree
        if (other.CompareTag("Tree"))
        {
            currentTree = other.gameObject;
            contactTimer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Clear tree reference when the chainsaw exits the tree collider
        if (other.CompareTag("Tree") && other.gameObject == currentTree)
        {
            currentTree = null;
            contactTimer = 0f;
        }
    }

    private void CutDownTree(GameObject tree)
    {
        Debug.Log($"Tree {tree.name} has been cut down!");

        // Example: Apply physics to make the tree fall
        Rigidbody rb = tree.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(Vector3.forward * 50f, ForceMode.Impulse);
        }

        // Optional: Disable the tree or replace it with chopped wood/logs
        TreeBehavior treeScript = tree.GetComponent<TreeBehavior>();
        if (treeScript != null)
        {
            treeScript.OnCutDown();
        }

        currentTree = null; // Clear reference
    }
}
