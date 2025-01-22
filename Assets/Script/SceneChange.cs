using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public Transform NPCSpawnPoint;     //position of NPC spawn point in the scene
    public GameObject chainsaw;         //chainsaw gameobject enabling
    public GameObject squirrelStore;       //storage of Scorn for squirrel

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
            }
            else
            {
                Debug.LogWarning("Chainsaw GameObject is not assigned.");
            }
        }

        IEnumerator EnableChainsawWithDelay()
        {
            yield return new WaitForSeconds(1.0f);
            chainsaw.SetActive(true);
        }

    }
}