using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeSquirrel : MonoBehaviour
{
    public Transform NPCSpawnPoint;     //position of NPC spawn point in the scene
    public GameObject hut;         //hut constructed after sceenchange
    public GameObject cutTrees;       //disable trees prefab which are cut 
    public GameObject squirrelStore;       //storage of Scorn for squirrel
    public GameObject hideAcorns;           //disabling the acorn gameobject prefab

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = NPCSpawnPoint.position;
            other.transform.rotation = NPCSpawnPoint.rotation;

            if (hut != null)
            {

                hut.SetActive(true);
                squirrelStore.SetActive(true);
                cutTrees.SetActive(false);
                hideAcorns.SetActive(false);
                Debug.Log("Changed scene with to squirrel base with scene update.");
            }
            else
            {
                Debug.LogWarning("SceneChange prefabs not applied in code.");
            }
        }
    }
}
