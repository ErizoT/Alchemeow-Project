using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngData : MonoBehaviour
{
    [Tooltip("Name that the cauldron will check when placed inside.")]
    [SerializeField] string ingredientID;

    [Tooltip("Will respawn if accidentally placed in the cauldron")]
    [SerializeField] bool essential; 
    
    [HideInInspector] public Ingredient ingClass;

    private void Start()
    {
        ingClass = new Ingredient(ingredientID);
    }
}
