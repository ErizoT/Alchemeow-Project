using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using FMODUnity;

public class audio_UISelected : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    
    [SerializeField] private EventReference selectSound; 
    [SerializeField] private EventReference pressSound;


    private Button button;

    private void Awake()
    {
        // Get the Button component attached to this GameObject
        button = GetComponent<Button>();

       
        button.onClick.AddListener(PlayPressSound);
    }

    
    private void PlayPressSound()
    {
        if (!pressSound.IsNull)
        {
            RuntimeManager.PlayOneShot(pressSound);
        }
    }

    
    public void OnSelect(BaseEventData eventData)
    {
        PlaySelectSound();
    }

    // Optional: Play sound when hovered over with the mouse
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySelectSound();
    }

    // Common function for playing the select sound
    private void PlaySelectSound()
    {
        if (!selectSound.IsNull)
        {
            RuntimeManager.PlayOneShot(selectSound);
        }
    }
}
