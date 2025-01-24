using UnityEngine;

public class SimpleTeleport : MonoBehaviour
{
    public Transform teleportDestination; // The destination to teleport the player to

    public GameObject chainsaw;         //chainsaw gameobject enabling
    public GameObject squirrelStore;       //storage of Scorn for squirrel

    public Material newSkyboxMaterialWinter;    // Skybox material for the new scene

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Teleport the player to the destination
            other.transform.position = teleportDestination.position;
            other.transform.rotation = teleportDestination.rotation;

            // Debug message to confirm the teleportation
            Debug.Log("Player teleported to: " + teleportDestination.position);

            if (chainsaw != null)
            {

                chainsaw.SetActive(true);
                squirrelStore.SetActive(false);
                Debug.Log("Changed scene with chainsaw prefab enabled.");

                //RenderSettings.skybox = newSkyboxMaterialWinter;
                DynamicGI.UpdateEnvironment(); // Update the lighting to reflect the new skybox
                Debug.Log("Winter Skybox changed.");
            }
            else
            {
                Debug.LogWarning("Chainsaw GameObject is not assigned.");

                Debug.LogWarning("New skybox material is not assigned.");
            }
        }

    }
}
