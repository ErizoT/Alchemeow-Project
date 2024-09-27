using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueArray : MonoBehaviour
{
    private static DialogueArray _instance;
    public static DialogueArray Instance => _instance;
    
    public List<IngredientDialogue> ingredientDialogueList;
    [SerializeField] DialogueDisplay disalogueDisplay;

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
}

[System.Serializable]
public class IngredientDialogue
{
    [SerializeField] string ingredientName;
    public List<DialogueEntry> dialogueEntries;
}
[System.Serializable]
public class DialogueEntry
{
    public Sprite characterExpressions;
    public string dialogueArray;
}
