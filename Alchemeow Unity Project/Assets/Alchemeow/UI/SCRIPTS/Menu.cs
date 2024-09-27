using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class Menu : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject selectorLeft;
    public GameObject selectorRight;

    public GameObject optionsFirstButton, optionsClosedButton;
    [SerializeField]
    private float selectorOffset = 2.0f;
    public GameObject startGame;
    private SelectableText currentSelectedText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        if (selectedObject == null) { return; }
        if (currentSelectedText?.gameObject != selectedObject) //check if the current selected gameObject has changed, if so...
        {
            if (currentSelectedText != null) //AND if currentSelectedText isn't null...
            {
                currentSelectedText.OnDeHighlight(); //tell the previous selected gameObject to stop being highlighted.
            }
        }
        SelectableText selectedText = selectedObject.GetComponentInChildren<SelectableText>(); //try to get SelectableText component from selectedObject
        if (selectedText == null) {
            selectorLeft.SetActive(false);
            selectorRight.SetActive(false);
            return; 
        } //if the current selected object has no SelectableText component, deactivate selectors and return (end early)
        currentSelectedText = selectedText;
        selectedText.OnHighlight();
        float text_x_extent = (selectedText.textMesh.mesh.bounds.extents.x) * (selectedText.transform.localScale.x);
        selectorLeft.SetActive(selectedText.showSelectors);
        selectorRight.SetActive(selectedText.showSelectors);
        if (selectedText.showSelectors == false) { return; }
        Vector3 currentSelectorOffset = new Vector3(selectorOffset + text_x_extent, 0, 0);
        selectorLeft.transform.position = selectedText.transform.position - currentSelectorOffset;
        selectorRight.transform.position = selectedText.transform.position + currentSelectorOffset;
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
