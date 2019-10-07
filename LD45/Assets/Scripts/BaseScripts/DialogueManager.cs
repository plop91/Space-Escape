using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    //specific to game**
    public TMP_Text dialogueText;

    //**

    public Animator animator;
    public Queue<string> sentences;
    private Player player;
    void Start()
    {
        sentences = new Queue<string>();
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EndDialogue();
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        sentences.Clear();
        player.canMove = false;
        PauseMenu.GameIsPaused = true;

        foreach (string sentence in dialogue.dialogue)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        player.canMove = true;
        PauseMenu.GameIsPaused = false;
    }
}
