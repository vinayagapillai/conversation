using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private Queue<string> sentences;
    
    public Text nameText;
    public Text dialogueText;
    public Animator dialogueAnimator;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        
        foreach(string sentence in dialogue.sentence)
        {
            sentences.Enqueue(sentence);
        }
        nameText.text = dialogue.name;
        dialogueAnimator.SetBool("inTalk", true);
        NextDialogue();
    }

    public void NextDialogue()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(SlowText(sentence));
    }

    IEnumerator SlowText(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue()
    {
        dialogueAnimator.SetBool("inTalk", false);
        //Debug.Log("empty dialogues");
    }
}
