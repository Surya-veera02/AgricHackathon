using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class Storage : MonoBehaviour
{
    public int maxAcorns = 5; // Maximum capacity
    private List<GameObject> storedAcorns = new List<GameObject>();

    public TMP_Text playerAcornStorage;

    public void AddAcorn(GameObject acorn)
    {
        if (storedAcorns.Count < maxAcorns)
        {
            storedAcorns.Add(acorn);
            Debug.Log("Acorn stored! Total: " + storedAcorns.Count);

            playerAcornStorage.text = "Collected = " + storedAcorns.Count + "/5";
        }
        else
        {
            Debug.Log("Storage is full!");
        }
    }

    public void RemoveAcorn(GameObject acorn)
    {
        if (storedAcorns.Contains(acorn))
        {
            storedAcorns.Remove(acorn);
            Debug.Log("Acorn removed! Remaining: " + storedAcorns.Count);
        }
        else
        {
            Debug.Log("Acorn not found in storage!");
        }
    }

    public int GetAcornCount()
    {
        return storedAcorns.Count;
    }
}
