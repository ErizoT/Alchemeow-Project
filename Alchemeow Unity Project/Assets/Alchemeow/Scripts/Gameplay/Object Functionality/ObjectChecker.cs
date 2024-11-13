using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChecker : MonoBehaviour
{
    public GameObject ballPrefab;
    public List<Potion> potions;
    public Texture2D ballImage;

    public DialogueDisplay dialogue;
    public Animator cauldronAnimator;

    public ParticleSystem particleCorrect;
    public ParticleSystem particleIncorrect;

    [SerializeField] FMODUnity.EventReference cauldronHappy;
    private FMOD.Studio.EventInstance cauldronHappyInstance;
    [SerializeField] FMODUnity.EventReference cauldronAngry;
    private FMOD.Studio.EventInstance cauldronAngryInstance;

    [Header("Crystal Ball Things")]
    [SerializeField] GameObject crystalBallImage;
    [SerializeField] ParticleSystem crystalBallParticle;




    private void Start()
    {
        cauldronAnimator.GetComponent<Animator>();
        
        particleCorrect.GetComponent<ParticleSystem>();
        particleIncorrect.GetComponent<ParticleSystem>();

        ballImage = potions[0].ingredients[0].ingIMG;
        crystalBallImage.GetComponent<MeshRenderer>().material.SetTexture("_Texture", ballImage);
    }

    private void OnTriggerEnter(Collider other)
    {
        // WHEN AN INGREDIENT IS PUT INSIDE THE CAULDRON

        IngData ingredientData = other.GetComponent<IngData>();
        Ingredient incomingIng = ingredientData.ingClass;
        Ingredient requiredIng = potions[0].ingredients[0];

        if (incomingIng.ingID == requiredIng.ingID) // Play when the correct ingredient is put in
        {
            // Play positive sound effect
            cauldronHappyInstance = FMODUnity.RuntimeManager.CreateInstance(cauldronHappy);
            cauldronHappyInstance.start();

            // Play good particle effect
            particleIncorrect.Stop();
            particleCorrect.Stop();
            particleCorrect.Play();

            //ballImage = potions[0].ingredients[0].ingIMG;
            //StartCoroutine(SwitchImage(ballImage));

            // Play good cauldron animation
            cauldronAnimator.SetTrigger("AddedIngredient");
            cauldronAnimator.SetBool("CorrectIngredient", true);

            // Call DialogueArray to cycle to next ingredient dialogue list
            DialogueArray.Instance.ClearIngredient();



            // Mark complete objective
            //print("Placed the correct ingredient");
            CompleteObjective();
            
        }
        else // When the ingredient is wrong
        {
            // Play bad sound effect
            cauldronAngryInstance = FMODUnity.RuntimeManager.CreateInstance(cauldronAngry);
            cauldronAngryInstance.start();

            // Play bad particle effect
            particleCorrect.Stop();
            particleIncorrect.Stop();
            particleIncorrect.Play();

            // play bad cauldron animation
            cauldronAnimator.SetTrigger("AddedIngredient");
            cauldronAnimator.SetBool("CorrectIngredient", false);

            // Call CameraManager to do the WrongIngredient camera moveent
            StartCoroutine(CameraManager.Instance.WrongIngredient());

            //print("Placed the wrong ingredient");
        }

        if (ingredientData.canRespawn)
        {
            ingredientData.Respawn();
        }
        else
        {
            Destroy(other.gameObject);
        }
        cauldronAnimator.SetTrigger("AddedIngredient");
    }

    public void CompleteObjective()
    {
        // Remove the first ingredient from the current potion
        potions[0].ingredients.RemoveAt(0);

        // Check if there are no more ingredients left
        if (potions[0].ingredients.Count == 0)
        {
            // Initiate the ending sequence for this potion
            print("Completed the quest!");
            CompleteQuest();
        }
        else
        {
            // Call CameraManager to do the CorrectIngredient camera moveent
            StartCoroutine(CameraManager.Instance.CorrectIngredient());

            // Update the crystal ball image to the next ingredient
            ballImage = potions[0].ingredients[0].ingIMG;
            StartCoroutine(SwitchImage(ballImage));
        }
    }

    private void CompleteQuest()
    {
        // Call CameraManager to do the CorrectIngredient camera moveent
        StartCoroutine(CameraManager.Instance.FinalPotion());
    }

    IEnumerator SwitchImage(Texture2D image)
    {
        yield return new WaitForSeconds(2);
        crystalBallImage.GetComponent<MeshRenderer>().material.SetTexture("_Texture", image);
        crystalBallParticle.Play();

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