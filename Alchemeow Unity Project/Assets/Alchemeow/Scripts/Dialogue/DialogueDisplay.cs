using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;
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

    [SerializeField] private StudioEventEmitter dialogueEmitter;
    

    public int index;

    // Start is called before the first frame update
    void Awake()
    {
        //fadeOut = GetComponent<Animator>();
        textComponent.text = string.Empty;
        
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

    public void StartDialogue(List<string> dialogueList)
    {
        if (lines != null)
        {
            fadeOut.SetBool("DialogueEnd", false);
            lines = new List<string>(dialogueList);
            index = 0;
            StartCoroutine(TypeLine());
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            dialogueEmitter.Play();
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
            StartCoroutine(CameraManager.Instance.BackToPlayer());
        }
    }


}
