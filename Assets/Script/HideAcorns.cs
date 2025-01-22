using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HideAcorns : MonoBehaviour
{

    public int hideAcorn = 7;      //Ground capacity
    private List<GameObject> hideAcorns = new List<GameObject>();       // hide the acorns in ground

    public TMP_Text hideAcornStorage;

    public AudioSource AcornAudioSource;

    public void AddAcorn(GameObject acorn)
    {

        //Hiding the Acorns
        if (hideAcorns.Count < hideAcorn)
        {
            hideAcorns.Add(acorn);
            Debug.Log("Acorn hidden! Total: " + hideAcorns.Count);

            hideAcornStorage.text = "Acorn hidden = " + hideAcorns.Count + "/" + hideAcorn;
        }
        else
        {
            Debug.Log("Hiding is full!");
            hideAcornStorage.text = "Hiding Full!";
        }
    }

    public void RemoveAcorn(GameObject acorn)
    {

        //Hiding Acorn
        if (hideAcorns.Contains(acorn))
        {
            hideAcorns.Remove(acorn);
            Debug.Log("Acorn removed! Remaining: " + hideAcorns.Count);
            hideAcornStorage.text = "Acorn Collected = " + hideAcorns.Count + "/" + hideAcorn;

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
