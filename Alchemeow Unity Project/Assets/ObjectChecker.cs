using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChecker : MonoBehaviour
{
    public List<Quest> quests;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Destroyed" + other.gameObject);

        Ingredient toCheck = other.gameObject.GetComponent<Ingredient>();
        if (toCheck.ingID == quests[0].ingredient)
        {
            CompleteQuest();
        }
        Destroy(other.gameObject);
    }

    private void CompleteQuest()
    {
        print("YEAHHH");
        quests.RemoveAt(0);
    }

}

 [System.Serializable] public class Quest
{
    public string ingredient;
    public Texture2D ingredientPic;
}