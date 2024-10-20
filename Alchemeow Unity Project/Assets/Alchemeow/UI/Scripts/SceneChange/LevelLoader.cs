using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void GoToScene(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
    }

    IEnumerator LoadLevel(string levelName)
    {
        Debug.Log("Loading Level " + levelName);
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelName);
    }
}
