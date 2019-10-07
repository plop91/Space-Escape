using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool automaticDialogue = true;
    private bool alreadyOpened = false;
    public Dialogue dialogue;

    public void Update()
    {
        if (automaticDialogue && !alreadyOpened)
        {
            TriggerDialogue();
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        alreadyOpened = true;
    }

}