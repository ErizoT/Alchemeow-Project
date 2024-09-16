using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChecker : MonoBehaviour
{
    public List<Potion> potions;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Destroyed" + other.gameObject);

        Ingredient incomingIng = other.gameObject.GetComponent<IngData>().ingClass;
        Ingredient requiredIng = potions[0].ingredients[0];

        if (incomingIng.ingID == requiredIng.ingID) // Play when the correct ingredient is put in
        {
            // Play positive sound effect
            // Play good particle effect
            // Change cauldron colour to green
            // Play good cauldron animation
            CompleteObjective();
            print("Placed the correct ingredient");
        }
        else // When the ingredient is wrong
        {
            // Play bad sound effect
            // Play bad particle effect
            // Play bad sound
            // play bad cauldron animation
            print("Placed the wrong ingredient");
        }

        Destroy(other.gameObject);
        
        // If it's an essential item, respawn it
    }

    private void CompleteObjective()
    {
        // Remove the ingredient[0] from the current potion's ingredient list
        // Change the crystal ball's image to the next one
        // Pan the main camera to the crystal ball

        // *If the current potion's ingredient list is at 0*
        // CompleteQuest();

        potions[0].ingredients.RemoveAt(0);
        print("Removed an ingredient");
        
        
        if(potions[0].ingredients.Count == 0)
        {
            //print("complete potion");
            CompleteQuest();
        }

    }

    private void CompleteQuest()
    {
        // Remove potion[0] from the potion list

        print("Moved to new potion");
        potions.RemoveAt(0);
    }

}

 [System.Serializable] public class Potion
{
    public List<Ingredient> ingredients;
}

[System.Serializable]
public class Ingredient
{
    public string ingID;
    public Texture2D ingIMG;

    public Ingredient(string id)
    {
        ingID = id;
    }
}