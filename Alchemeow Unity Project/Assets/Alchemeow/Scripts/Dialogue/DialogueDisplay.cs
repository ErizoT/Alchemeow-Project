using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;
using UnityEngine.ProBuilder.MeshOperations;
using System.Diagnostics.CodeAnalysis;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] private ObjectChecker objectChecker;
    
    public TextMeshProUGUI textComponent;
    public Image portraitSprite;
    public List<Sprite> changeSprites;
    public List<string> lines;
    public float textSpeed;

    public Animator fadeOut;
    public string DialogueEnd;
    public int DialogueProgression;

    [SerializeField] private StudioEventEmitter dialogueEmitter;
    [SerializeField] private StudioEventEmitter nextEmitter;


    public int index;

    // Start is called before the first frame update
    void Awake()
    {
        //fadeOut = GetComponent<Animator>();
        textComponent.text = string.Empty;
        
    }

    public void AdvanceText()
    {
        if (lines != null) 
        {
            // For when the player proceeds at the end of a sentence
            if (textComponent.text == lines[index] && fadeOut.GetBool("DialogueEnd") == false)
            {
                nextEmitter.Play();
                NextLine();
                //portraitSprite.sprite = changeSprites[index];
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

    public void StartDialogue(List<string> dialogueList, List<Sprite> characterSprites)
    {
        fadeOut.SetBool("DialogueEnd", false);
        lines = new List<string>(dialogueList);
        changeSprites = new List<Sprite>(characterSprites);
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        portraitSprite.sprite = changeSprites[index];

        // Check the current character expression for sound changes
        string currentExpression = portraitSprite.sprite.name;
        Debug.Log("current expression: " + currentExpression);
        
        if (currentExpression.Contains("r_"))
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("CurrentCharacter", 0);
            Debug.Log("deez");
        }
        else if (currentExpression.Contains("Cat"))
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("CurrentCharacter", 1);


            Debug.Log("nuts");
        }



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
            //Debug.Log("CLOSE");
            lines = null;
            changeSprites = null;
            StartCoroutine(CameraManager.Instance.BackToPlayer());
        }
    }


}
