using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class Storage : MonoBehaviour
{
    public int storeAcorns = 5; // Maximum capacity in body
    public int hideAcorn = 7;      //Ground capacity
    private List<GameObject> storedAcorns = new List<GameObject>();     //list of Acorn to store in squirrel 
    private List<GameObject> hideAcorns = new List<GameObject>();       // hide the acorns in ground

    public TMP_Text hideAcornStorage;
    public TMP_Text storeAcornStorage;

    public AudioSource AcornAudioSource;

    public void AddAcorn(GameObject acorn)
    {

        //Collecting and storing
        if (storedAcorns.Count < storeAcorns)
        {
            storedAcorns.Add(acorn);
            Debug.Log("Acorn stored! Total: " + storedAcorns.Count);
            AcornAudioSource.Play();                                    //sound enabled

            storeAcornStorage.text = "Acorn Collected = " + storedAcorns.Count + "/" + storeAcorns;
        }
        else
        {
            Debug.Log("Storage is full!");
            storeAcornStorage.text = "Storage is full!, store it below tree";
        }

        //Hiding the Acorns
        if (hideAcorns.Count < hideAcorn)
        {
            hideAcorns.Add(acorn);
            Debug.Log("Acorn stored! Total: " + hideAcorns.Count);

            hideAcornStorage.text = "Acorn Stored = " + hideAcorns.Count + "/" + hideAcorn;
        }
        else
        {
            Debug.Log("Storage is full!");
            hideAcornStorage.text = "Storage Full!";
        }
    }

    public void RemoveAcorn(GameObject acorn)
    {
        //Acorn Storage
        if (storedAcorns.Contains(acorn))
        {
            storedAcorns.Remove(acorn);
            Debug.Log("Acorn removed! Remaining: " + storedAcorns.Count);
            storeAcornStorage.text = "Acorn Collected = " + storedAcorns.Count + "/" + storeAcorns;
            AcornAudioSource.Play();                                //sound enabled
        }
        else
        {
            Debug.Log("Acorn not found in storage!");
            storeAcornStorage.text = "Acorn Empty";
        }

        //Hiding Acorn
        if (hideAcorns.Contains(acorn))
        {
            hideAcorns.Remove(acorn);
            Debug.Log("Acorn removed! Remaining: " + hideAcorns.Count);
            storeAcornStorage.text = "Acorn Collected = " + hideAcorns.Count + "/" + storeAcorns;

        }
        else
        {
            Debug.Log("Acorn not found in storage!");
            hideAcornStorage.text = "Acorn Empty";
        }
    }

    //public int GetAcornCount()
    //{
    //    return hideAcorns.Count;
    //    return storedAcorns.Count;
    //}
}
