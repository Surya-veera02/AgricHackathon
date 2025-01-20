using UnityEngine;

public class Acorn : MonoBehaviour
{
    public GameObject arrowPrefab; // Reference to the arrow prefab
    private GameObject arrowInstance; // Instance of the arrow
    private bool isStored = false; // Tracks if the acorn is stored
    private Rigidbody rb;

    // Arrow animation variables
    private float moveSpeed = 1.5f; // Speed of the up-down movement
    private float moveHeight = 0.3f; // Height of the up-down movement
    private float rotationSpeed = 50f; // Speed of the left-right rotation

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Spawn the arrow above the acorn
        if (arrowPrefab != null)
        {
            arrowInstance = Instantiate(arrowPrefab, transform.position + Vector3.up * 2.0f, Quaternion.identity);
            arrowInstance.transform.SetParent(transform); // Parent to the acorn
        }
    }

    void Update()
    {
        // Animate the arrow if it exists
        if (arrowInstance != null)
        {
            AnimateArrow();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Storage") && !isStored)
        {
            // Acorn is being stored
            isStored = true;
            rb.isKinematic = true; // Disable physics
            transform.SetParent(other.transform); // Attach to storage
            transform.localPosition = Vector3.zero; // Position relative to storage
            other.GetComponent<Storage>().AddAcorn(this.gameObject); // Notify storage

            // Destroy the arrow when collected
            if (arrowInstance != null)
            {
                Destroy(arrowInstance);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Storage") && isStored)
        {
            // Acorn is being removed from storage
            isStored = false;
            rb.isKinematic = false; // Re-enable physics
            transform.SetParent(null); // Detach from storage
            other.GetComponent<Storage>().RemoveAcorn(this.gameObject); // Notify storage
        }
    }

    private void AnimateArrow()
    {
        // Up and down movement
        float newY = Mathf.Sin(Time.time * moveSpeed) * moveHeight;
        arrowInstance.transform.localPosition = new Vector3(0, 2.0f + newY, 0);

        // Left and right rotation
        arrowInstance.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
