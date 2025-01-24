using UnityEngine;

public class GameSetupManager : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject treesToDisable; // Parent GameObject containing all trees to disable
    public GameObject interactableAcorn; // Acorn GameObject to enable
    //public GameObject chainsaw;         //chainsaw gameobject disabling
    public GameObject squirrelStore;       //storage of Scorn for squirrel ennabling

    public GameObject acornNuts;

    [Header("Skybox Settings")]
    public Material startingSkyboxMaterial; // Initial Skybox material

    void Start()
    {
        SetupGameObjects();
        SetupSkybox();
    }


    private void SetupGameObjects()
    {
        acornNuts.SetActive(true);

        {
            // Disable trees
            if (treesToDisable != null)
            {
                treesToDisable.SetActive(false);
                Debug.Log("Trees have been disabled.");
            }
            else
            {
                Debug.LogWarning("TreesToDisable GameObject is not assigned.");
            }

            // Enable interactable acorn
            if (interactableAcorn != null)
            {
                interactableAcorn.SetActive(true);
                Debug.Log("Interactable Acorn has been enabled.");
            }
            else
            {
                Debug.LogWarning("Interactable Acorn GameObject is not assigned.");
            }

            //if (chainsaw != null)
            //{
            //    chainsaw.SetActive(true);
            //    Debug.Log("chainsaw has been enabled.");
            //}
            //else
            //{
            //    Debug.LogWarning("chainsaw GameObject is not assigned.");
            //}
            // Enable interactable acorn
            if (squirrelStore != null)
            {
                squirrelStore.SetActive(true);
                Debug.Log("squirrelStore has been enabled.");
            }
            else
            {
                Debug.LogWarning("squirrelStore GameObject is not assigned.");
            }
        }
    }
    private void SetupSkybox()
    {
        if (startingSkyboxMaterial != null)
        {
            RenderSettings.skybox = startingSkyboxMaterial;
            DynamicGI.UpdateEnvironment(); // Update lighting to match the new skybox
            Debug.Log("Skybox has been set to the starting material.");
        }
        else
        {
            Debug.LogWarning("Starting Skybox Material is not assigned.");
        }
    }
}



//        private void SetupGameObjects()
//    {
//        // Disable trees
//        if (treesToDisable != null)
//        {
//            treesToDisable.SetActive(false);
//            Debug.Log("Trees have been disabled.");
//        }
//        else
//        {
//            Debug.LogWarning("TreesToDisable GameObject is not assigned.");
//        }

//        // Enable interactable acorn
//        if (interactableAcorn != null)
//        {
//            interactableAcorn.SetActive(true);
//            Debug.Log("Interactable Acorn has been enabled.");
//        }
//        else
//        {
//            Debug.LogWarning("Interactable Acorn GameObject is not assigned.");
//        }

//        if (chainsaw != null)
//        {
//            chainsaw.SetActive(true);
//            Debug.Log("chainsaw has been enabled.");
//        }
//        else
//        {
//            Debug.LogWarning("chainsaw GameObject is not assigned.");
//        }
//        // Enable interactable acorn
//        if (squirrelStore != null)
//        {
//            squirrelStore.SetActive(true);
//            Debug.Log("squirrelStore has been enabled.");
//        }
//        else
//        {
//            Debug.LogWarning("squirrelStore GameObject is not assigned.");
//        }
//    }

//    private void SetupSkybox()
//    {
//        if (startingSkyboxMaterial != null)
//        {
//            RenderSettings.skybox = startingSkyboxMaterial;
//            DynamicGI.UpdateEnvironment(); // Update lighting to match the new skybox
//            Debug.Log("Skybox has been set to the starting material.");
//        }
//        else
//        {
//            Debug.LogWarning("Starting Skybox Material is not assigned.");
//        }
//    }
//}
