using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChecker : MonoBehaviour
{
    public GameObject ballPrefab;
    public List<Potion> potions;
    [HideInInspector] public Texture2D ballImage;

    public Animator cauldronAnimator;

    public ParticleSystem particleCorrect;
    public ParticleSystem particleIncorrect;

    [SerializeField] FMODUnity.EventReference cauldronHappy;
    private FMOD.Studio.EventInstance cauldronHappyInstance;
    [SerializeField] FMODUnity.EventReference cauldronAngry;
    private FMOD.Studio.EventInstance cauldronAngryInstance;


    private void Start()
    {
        cauldronAnimator.GetComponent<Animator>();
        
        particleCorrect.GetComponent<ParticleSystem>();
        particleIncorrect.GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // WHEN AN INGREDIENT IS PUT INSIDE THE CAULDRON

        // Access CameraManager to change the cameraAngle to the cauldron
        //CameraManager.Instance.ChangeCameraState("Cauldron");

        Ingredient incomingIng = other.gameObject.GetComponent<IngData>().ingClass;
        Ingredient requiredIng = potions[0].ingredients[0];

        if (incomingIng.ingID == requiredIng.ingID) // Play when the correct ingredient is put in
        {
            // Play positive sound effect
            cauldronHappyInstance = FMODUnity.RuntimeManager.CreateInstance(cauldronHappy);
            cauldronHappyInstance.start();

            // Play good particle effect
            //particleCorrect.Play();           // Disabled for the meantime because the good animation wouldnt play

            // Play good cauldron animation
            cauldronAnimator.SetTrigger("AddedIngredient");
            cauldronAnimator.SetBool("CorrectIngredient", true);

            // Call CameraManager to do the CorrectIngredient camera moveent
            StartCoroutine(CameraManager.Instance.CorrectIngredient());

            // Mark complete objective
            print("Placed the correct ingredient");
            CompleteObjective();
            
        }
        else // When the ingredient is wrong
        {
            // Play bad sound effect
            cauldronAngryInstance = FMODUnity.RuntimeManager.CreateInstance(cauldronAngry);
            cauldronAngryInstance.start();
            
            // Play bad particle effect
            particleIncorrect.Play();

            // play bad cauldron animation
            cauldronAnimator.SetTrigger("AddedIngredient");
            cauldronAnimator.SetBool("CorrectIngredient", false);

            // Call CameraManager to do the WrongIngredient camera moveent
            StartCoroutine(CameraManager.Instance.WrongIngredient());

            print("Placed the wrong ingredient");
        }

        Destroy(other.gameObject);
        cauldronAnimator.SetTrigger("AddedIngredient");

        // If it's an essential item, respawn it
    }

    private void CompleteObjective()
    {
        // Remove the ingredient[0] from the current potion's ingredient list
        // Change the crystal ball's image to the next one

        ballImage = potions[0].ingredients[0].ingIMG;
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