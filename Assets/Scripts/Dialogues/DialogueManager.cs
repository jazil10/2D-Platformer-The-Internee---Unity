using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject dialogueBox; // Reference to the dialogue UI Panel.
    public TextMeshProUGUI dialogueText; // Reference to the TextMeshProUGUI element in the UI Panel.

    private bool isDialogueActive = false; // To track if a dialogue is currently active.

    private Queue<string> dialogueQueue = new Queue<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(string speaker, string[] dialogueLines)
    {
        if (!isDialogueActive)
        {
            isDialogueActive = true;
            dialogueBox.SetActive(true);

            dialogueQueue.Clear();

            foreach (string line in dialogueLines)
            {
                dialogueQueue.Enqueue(speaker + ": " + line); // Include the speaker name.
            }

            DisplayNextLine();
        }
    }


    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextLine();
        }
    }

    private void DisplayNextLine()
    {
        if (dialogueQueue.Count > 0)
        {
            string nextLine = dialogueQueue.Dequeue();
            dialogueText.text = nextLine;
            Debug.Log("Displaying Line: " + nextLine);

        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        Debug.Log("End Dialogue is being called wohoo");
        dialogueBox.SetActive(false);
        isDialogueActive = false;
    }
}
