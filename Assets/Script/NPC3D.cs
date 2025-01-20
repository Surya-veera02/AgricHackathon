using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using static UnityEngine.GraphicsBuffer;


public class NPC3D : MonoBehaviour
{
    [Header("Character")]
    public string characterName = "";

    [Header("Yarn Specific")]
    public string talkToNode = "";
    public YarnProject scriptToLoad;
    public DialogueRunner dialogueRunner; //refernce to the dialogue control
    public GameObject dialogueCanvas; //refernce to the canvas

    [Header("Dialogue Canvas")]
    public Vector3 PostionSpeachBubble = new Vector3(0f, 2.0f, 0.0f);
    private float canvasTurnSpeed = 2;
    private bool canvasActive;
    private GameObject playerGameObject;


    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        dialogueCanvas = GameObject.FindGameObjectWithTag("Dialogue Canvas");
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        playerGameObject = GameObject.FindGameObjectWithTag("Player");


        if (scriptToLoad == null)
        {
            Debug.LogError("NPC3D not set up with yarn scriptToLoad ", this);
        }

        if (string.IsNullOrEmpty(characterName))
        {
            Debug.LogWarning("NPC3D not set up with characterName", this);
        }

        if (string.IsNullOrEmpty(talkToNode))
        {
            Debug.LogError("NPC3D not set up with talkToNode", this);
        }

        if (dialogueRunner == null)
        {
            Debug.LogError("dialogueRunner not set up", this);
        }

        if (dialogueCanvas == null)
        {
            Debug.LogError("Dialogue Canvas not set up", this);
        }

        if (playerGameObject == null)
        {
            Debug.LogError("Player Game Object not set up", this);
        }

        if (scriptToLoad != null && dialogueRunner != null && dialogueRunner != null)
        {
            dialogueRunner.yarnProject = scriptToLoad; //adds the script to the dialogue system
        }
    }
    void Update()
    {
        if (canvasActive)
        {
            Vector3 lookDir = dialogueCanvas.transform.position - playerGameObject.transform.position;
            float radians = Mathf.Atan2(lookDir.x, lookDir.z);
            float degrees = radians * Mathf.Rad2Deg;

            float str = Mathf.Min(canvasTurnSpeed * Time.deltaTime, 1);
            Quaternion targetRotation = Quaternion.Euler(0, degrees, 0);
            dialogueCanvas.transform.rotation = Quaternion.Slerp(dialogueCanvas.transform.rotation, targetRotation, str);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if other is player
        if (other.gameObject.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(talkToNode))
            {
                if (dialogueCanvas != null)
                {
                    //move the Canvas to the object and off set
                    canvasActive = true;
                    dialogueCanvas.transform.SetParent(transform); // use the root to prevent scaling
                    dialogueCanvas.GetComponent<RectTransform>().anchoredPosition3D = transform.TransformVector(PostionSpeachBubble);
                }

                if (dialogueRunner.IsDialogueRunning)
                {
                    dialogueRunner.Stop();
                }
                Debug.Log("start dialogue");
                dialogueRunner.StartDialogue(talkToNode);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvasActive = false;
            dialogueRunner.Stop();

        }
    }
}

//-----------------------------------------------------------------------------------------------------------------------------------

//using UnityEngine;
//using Yarn.Unity;

//public class NPC3D : MonoBehaviour
//{
//    public DialogueRunner dialogueRunner; // Reference to the DialogueRunner in the scene
//    public YarnProject scriptToLoad;     // The Yarn Project to assign

//    void Start()
//    {
//        if (dialogueRunner == null)
//        {
//            Debug.LogError("DialogueRunner is not assigned!");
//            return;
//        }

//        if (scriptToLoad == null)
//        {
//            Debug.LogError("Yarn Project is not assigned!");
//            return;
//        }

//        // Assign the Yarn Project to the DialogueRunner
//        dialogueRunner.SetProject(scriptToLoad);
//    }
//}


//----------------------------------------------------------------------------------------------------------------------------------

//using UnityEngine;
//using Yarn.Unity;

//public class NPC3D : MonoBehaviour
//{
//    [Header("Dialogue Settings")]
//    public string talkToNode = "Start"; // The Yarn node to start dialogue
//    public DialogueRunner dialogueRunner; // Reference to the Dialogue Runner
//    public YarnProject scriptToLoad; // Yarn Project to dynamically assign
//    public GameObject dialogueCanvasPrefab; // Prefab for the Dialogue Canvas

//    private GameObject dialogueCanvasInstance; // Instance of the Dialogue Canvas
//    private bool playerInRange = false; // Tracks if the player is near the NPC

//    public Vector3 PostionSpeachBubble = new Vector3(0f, 2.0f, 0.0f);

//    void Start()
//    {
//        // Ensure the Dialogue Runner is assigned
//        if (dialogueRunner == null)
//        {
//            dialogueRunner = FindObjectOfType<DialogueRunner>();
//            if (dialogueRunner == null)
//            {
//                Debug.LogError("DialogueRunner is not assigned or found in the scene!");
//                return;
//            }
//        }

//        // Assign the Yarn Project to the Dialogue Runner
//        if (scriptToLoad != null)
//        {
//            dialogueRunner.SetProject(scriptToLoad);
//        }
//        else
//        {
//            Debug.LogError("Yarn Project is not assigned!");
//        }

//        // Instantiate the Dialogue Canvas if a prefab is provided
//        if (dialogueCanvasPrefab != null)
//        {
//            dialogueCanvasInstance = Instantiate(dialogueCanvasPrefab);
//            dialogueCanvasInstance.SetActive(false); // Keep it hidden until dialogue starts
//        }
//        else
//        {
//            Debug.LogError("Dialogue Canvas Prefab is not assigned!");
//        }
//    }

//    void Update()
//    { //&& Input.GetKeyDown(KeyCode.E
//        // Check for player input to start dialogue when in range
//        if (playerInRange ) // Replace KeyCode.E with your interaction key
//        {
//            StartDialogue();
//            dialogueCanvasPrefab.transform.SetParent(transform); // use the root to prevent scaling
//            dialogueCanvasPrefab.GetComponent<RectTransform>().anchoredPosition3D = transform.TransformVector(PostionSpeachBubble);
//        }
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        // Detect when the player enters the NPC's interaction range
//        if (other.CompareTag("Player"))
//        {
//            playerInRange = true;
//            ShowInteractionPrompt(true); // Show interaction prompt if using one
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        // Detect when the player leaves the NPC's interaction range
//        if (other.CompareTag("Player"))
//        {
//            playerInRange = false;
//            ShowInteractionPrompt(false); // Hide interaction prompt if using one
//        }
//    }

//    private void StartDialogue()
//    {
//        if (dialogueRunner == null || dialogueRunner.IsDialogueRunning)
//        {
//            Debug.LogWarning("DialogueRunner is either null or already running dialogue.");
//            return;
//        }

//        // Show the Dialogue Canvas if it exists
//        if (dialogueCanvasInstance != null)
//        {
//            dialogueCanvasInstance.SetActive(true);
//        }

//        // Start the Yarn dialogue at the specified node
//        dialogueRunner.StartDialogue(talkToNode);
//    }

//    private void ShowInteractionPrompt(bool show)
//    {
//        // Optional: Show or hide an interaction prompt (like "Press E to talk")
//        // Example: Activate or deactivate a UI element attached to the NPC
//        Debug.Log(show ? "Player can interact" : "Player left interaction range");
//    }

//    public void EndDialogue()
//    {
//        // Hide the Dialogue Canvas when the dialogue ends
//        if (dialogueCanvasInstance != null)
//        {
//            dialogueCanvasInstance.SetActive(false);
//        }
//    }
//}
