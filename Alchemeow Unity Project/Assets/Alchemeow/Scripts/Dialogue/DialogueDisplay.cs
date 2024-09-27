using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.ProBuilder.MeshOperations;
using System.Diagnostics.CodeAnalysis;
using System;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] private ObjectChecker objectChecker;
    
    public TextMeshProUGUI textComponent;
    public List<string> lines;
    public float textSpeed;

    public Animator fadeOut;
    public string DialogueEnd;
    public int DialogueProgression;

    // public AudioClip PrintingAudio;
    // public AudioSource source;
    // public AudioClip SkippingAudio;

    public int index;

    // Start is called before the first frame update
    void Start()
    {
        //fadeOut = GetComponent<Animator>();
        textComponent.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            AdvanceText();
        }
    }

    public void AdvanceText()
    {
        if (lines != null) 
        {
            // For when the player proceeds at the end of a sentence
            if (textComponent.text == lines[index] && fadeOut.GetBool("DialogueEnd") == false)
            {
                NextLine();
            }
            else // For when a player presses skip mid-sentence
            {
                // source.clip = SkippingAudio;
                // source.Play();
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        if (lines != null)
        {
            //lines = new List<string>(dialogueList);
            index = 0;
            StartCoroutine(TypeLine());
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            // source.clip = PrintingAudio;
            // source.Play();
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        index++;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());

        if (index == lines.Count)
        {
            fadeOut.SetBool("DialogueEnd", true);
            Debug.Log("CLOSE");
            lines = null;
        }

        /*
        // To add more points where the dialogue box closes, have the desired line number be empty text, and make a copy of the if function below
        // referring to that line to close dialogue box.
        if (index == 4)
        {
            fadeOut.SetBool("DialogueEnd", true);
            //fadeOut.SetInteger("DialogueProgression", 1);
            Debug.Log("CLOSE");
        }
        
        if (index == 8)
        {
            fadeOut.SetBool("DialogueEnd", true);
            //fadeOut.SetInteger("DialogueProgression", 2);
        }

        if(index == 10)
        {
            fadeOut.SetBool("DialogueEnd", true);
            //fadeOut.SetInteger("DialogueProgression", 3);
        }*/
    }


}
