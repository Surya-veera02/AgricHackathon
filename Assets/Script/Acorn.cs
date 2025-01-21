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
    private float rotationSpeed = 2.0f; // Speed of the left-right swing

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Spawn the arrow above the acorn
        if (arrowPrefab != null)
        {
            arrowInstance = Instantiate(arrowPrefab, transform.position + Vector3.up * 0.01f, Quaternion.Euler(90, 0, 0));
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

    private void AnimateArrow()
    {
        // Up and down movement
        float newY = Mathf.Sin(Time.time * moveSpeed) * moveHeight;
        arrowInstance.transform.localPosition = new Vector3(0, 2.0f + newY, 0);

        // Left and right rotation (around Z-axis for horizontal swing)
        float rotationAngle = Mathf.Sin(Time.time * rotationSpeed) * 90f; // Swing between 90 degrees
        arrowInstance.transform.localRotation = Quaternion.Euler(90, 0, rotationAngle); // Keep facing down while swinging
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Storage") && !isStored)
        {
            isStored = true;
            rb.isKinematic = true;
            transform.SetParent(other.transform);
            transform.localPosition = Vector3.zero;
            other.GetComponent<Storage>().AddAcorn(this.gameObject);

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
            isStored = false;
            rb.isKinematic = false;
            transform.SetParent(null);
            other.GetComponent<Storage>().RemoveAcorn(this.gameObject);
        }
    }
}
