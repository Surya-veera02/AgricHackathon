using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class SceneChangeSquirrel : MonoBehaviour
{
    public Transform NPCSpawnPoint;     //position of NPC spawn point in the scene
    public GameObject hut;         //hut constructed after sceenchange
    public GameObject cutTrees;       //disable trees prefab which are cut 
    public GameObject squirrelStore;       //storage of Scorn for squirrel
    public GameObject hideAcorns;           //disabling the acorn gameobject prefab
    public GameObject smallTree;            // enabling a tree sapling in nut stored place

    public GameObject chainsaw;         //chainsaw gameobject disable

    //public AudioSource endAudio;        //attaching the audio for squirrel naration

    public Material newSkyboxMaterialNormal;    // Skybox material for the new scene

    public GameObject player;
    public OVRScreenFade fade;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            StartCoroutine(MovingPlayer());


            //other.transform.position = NPCSpawnPoint.position;
            //other.transform.rotation = NPCSpawnPoint.rotation;

            //if (hut != null)
            //{

            //    hut.SetActive(true);
            //    squirrelStore.SetActive(true);
            //    chainsaw.SetActive(false);
            //    cutTrees.SetActive(false);
            //    hideAcorns.SetActive(false);
            //    smallTree.SetActive(true);
            //    Debug.Log("Changed scene with to squirrel base with scene update.");

            //    StartCoroutine(EnableEndNarrationWithDelay());

            //    RenderSettings.skybox = newSkyboxMaterialNormal;
            //    DynamicGI.UpdateEnvironment(); // Update the lighting to reflect the new skybox
            //    Debug.Log("Normal Skybox changed.");

            //}
            //else
            //{
            //    Debug.LogWarning("SceneChange prefabs not applied in code.");

            //    Debug.LogWarning("New skybox material is not assigned.");
            //}
        }

        //IEnumerator EnableEndNarrationWithDelay()
        //{
        //    yield return new WaitForSeconds(1.0f);
        //    //endAudio.Play();
        //}
    }

    public IEnumerator MovingPlayer()
    {

        fade.FadeOut();
        yield return new WaitForSeconds(2.0f);

        if (hut != null && smallTree != null )
        {

            hut.SetActive(true);
            squirrelStore.SetActive(true);
            chainsaw.SetActive(false);
            cutTrees.SetActive(false);
            hideAcorns.SetActive(false);
            smallTree.SetActive(true);
            Debug.Log("Changed scene with to squirrel base with scene update.");

            //StartCoroutine(EnableEndNarrationWithDelay());

            RenderSettings.skybox = newSkyboxMaterialNormal;
            DynamicGI.UpdateEnvironment(); // Update the lighting to reflect the new skybox
            Debug.Log("Normal Skybox changed.");

        }
        else
        {
            Debug.LogWarning("SceneChange prefabs not applied in code.");

            Debug.LogWarning("New skybox material is not assigned.");
        }
        yield return new WaitForSeconds(0.5f);

        player.transform.position = NPCSpawnPoint.position;
        player.transform.rotation = NPCSpawnPoint.rotation;

        fade.FadeIn();
    }
}
