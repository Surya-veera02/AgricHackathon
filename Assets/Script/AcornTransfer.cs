using UnityEngine;

public class AcornTransfer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Acorn"))
        {
            // Logic for receiving an acorn
            other.transform.SetParent(transform); // Attach to the receiving object
            other.GetComponent<Rigidbody>().isKinematic = true; // Disable physics
            Debug.Log("Acorn transferred to new object!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Acorn"))
        {
            // Notify storage to remove acorn
            Storage storage = other.GetComponentInParent<Storage>();
            if (storage != null)
            {
                storage.RemoveAcorn(other.gameObject);
            }

            Debug.Log("Acorn removed from storage!");
        }
    }
}
