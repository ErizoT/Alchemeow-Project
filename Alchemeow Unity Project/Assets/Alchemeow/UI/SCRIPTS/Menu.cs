using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject optionsMenu;

    public GameObject optionsFirstButton, optionsClosedButton;

    public GameObject startGame;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       

    }

    public void OpenOptions()

    {
        optionsMenu.SetActive(true);

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        //set a new selected obj
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }

    public void CloseOptions()

    {
        optionsMenu.SetActive(false);

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);

        //set a new selected obj
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }

   

        public void GameStart()

    {
        startGame.SetActive(false);

        //clear selected obj 
        EventSystem.current.SetSelectedGameObject(null);

    }
}
