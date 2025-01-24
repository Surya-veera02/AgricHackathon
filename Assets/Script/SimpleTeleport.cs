using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Climbing;

public class SimpleTeleport : MonoBehaviour
{
    public Transform teleportDestination; // The destination to teleport the player to

    public OVRScreenFade fade;

    public GameObject chainsaw;         //chainsaw gameobject enabling
    public GameObject squirrelStore;       //storage of Scorn for squirrel

    public GameObject player;

    public Material newSkyboxMaterialWinter;    // Skybox material for the new scene

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is tagged as "Player"
        if (other.tag == "Player")
        {
            StartCoroutine(MovingPlayer());

            //Teleport the player to the destination

            //ClimbInteractable[] climbInteractables = GetComponents<ClimbInteractable>();

            //for (int i = 0; i < climbInteractables.Length; i++)
            //{
            //    climbInteractables[i].enabled = false;
            //}

            //player.transform.position = teleportDestination.transform.position;

            //Debug.Log("Player position  = " + player.transform.position);

            //player.transform.position = teleportDestination.transform.position;
            //player.transform.rotation = teleportDestination.transform.rotation;

            //chainsaw.SetActive(true);

            // Debug message to confirm the teleportation
        }
    }

    public IEnumerator MovingPlayer()
    {
        ClimbInteractable[] climbInteractables = GetComponents<ClimbInteractable>();

        for (int i = 0; i < climbInteractables.Length; i++)
        {
            climbInteractables[i].enabled = false;
        }

        fade.FadeOut();

        yield return new WaitForSeconds(3f);

        player.transform.position = teleportDestination.transform.position;
        Debug.Log("Player position  = " + player.transform.position);
        ///////////

        Debug.Log($"Player moved to position: {teleportDestination.position} and rotation: {teleportDestination.rotation}");

        if (chainsaw != null)
        {

            chainsaw.SetActive(true);
            //StartCoroutine(EnableChainsawWithDelay());
            squirrelStore.SetActive(false);
            Debug.Log("Changed scene with chainsaw prefab enabled.");

            RenderSettings.skybox = newSkyboxMaterialWinter;
            DynamicGI.UpdateEnvironment(); // Update the lighting to reflect the new skybox
            Debug.Log("Winter Skybox changed.");
        }
        else
        {
            Debug.LogWarning("Chainsaw GameObject is not assigned.");

            Debug.LogWarning("New skybox material is not assigned.");
        }



        /////////////
        yield return new WaitForSeconds(0.5f);

        if (player.transform.position != teleportDestination.transform.position)
        {
            Debug.Log("Player did not move to correct position!! Retrying...");
            player.transform.position = teleportDestination.transform.position;
        }
        else
        {
            Debug.Log("Player successfully teleported to: " + teleportDestination.transform.position);

        }
        fade.FadeIn();

    }
}
