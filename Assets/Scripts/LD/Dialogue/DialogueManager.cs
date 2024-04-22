using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }
    // Start the dialogue
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    // Display the next sentence in the dialogue
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

    // Coroutine to display the sentence letter by letter
    IEnumerator TypeSentence(string sentence)
    {
        // Clear the dialogue text
        dialogueText.text = "";
        // Display the sentence letter by letter
        foreach (char letter in sentence.ToCharArray())
        {
            // Add the letter to the dialogue text
            dialogueText.text += letter;
            yield return null;
        }
    }

    // Close the dialogue box
    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
