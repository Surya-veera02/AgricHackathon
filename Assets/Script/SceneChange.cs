using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public Transform NPCSpawnPoint;     //position of NPC spawn point in the scene
    public GameObject chainsaw;         //chainsaw gameobject enabling
    public GameObject squirrelStore;       //storage of Scorn for squirrel

    public Material newSkyboxMaterialWinter;    // Skybox material for the new scene

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = NPCSpawnPoint.position;
            other.transform.rotation = NPCSpawnPoint.rotation;

            if (chainsaw != null)
            {

                //chainsaw.SetActive(true);
                StartCoroutine(EnableChainsawWithDelay());
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
        }

        IEnumerator EnableChainsawWithDelay()
        {
            yield return new WaitForSeconds(1.0f);
            chainsaw.SetActive(true);
        }

    }
}