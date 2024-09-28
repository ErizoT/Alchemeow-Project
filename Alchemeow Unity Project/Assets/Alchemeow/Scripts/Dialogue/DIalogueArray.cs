using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueArray : MonoBehaviour
{
    private static DialogueArray _instance;
    public static DialogueArray Instance => _instance;
    
    public List<IngredientDialogue> ingredientDialogueList;
    [SerializeField] DialogueDisplay dialogueDisplay;

    private void Awake()
    {
        // Singleton to make sure this is the only camera manager in the scene
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        StartNextDialogue();
    }

    public void StartNextDialogue()
    {
        Debug.Log("Called The next dialogue");
        List<string> stringList = ingredientDialogueList[0].dialogueEntries;
        dialogueDisplay.StartDialogue(stringList);
    }

    public void ClearIngredient()
    {
        ingredientDialogueList.RemoveAt(0);
    }
}

[System.Serializable]
public class IngredientDialogue
{
    [SerializeField] string ingredientName;
    public List<Sprite> characterExpressions;
    public List<string> dialogueEntries;
    
    public void CheckEntries(IngredientDialogue ingD) // Little check to see if both expression and dialogue lists are the same
    {
        if (characterExpressions.Count != dialogueEntries.Count)
        {
            Debug.LogError("Number of expressions don't match number of dialogue lines in" + ingD);
        }
    }
}
