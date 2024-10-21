using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using FMODUnity;

public class audio_UISelectedSLIDER : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    
    [SerializeField] private EventReference selectSound; 
    [SerializeField] private EventReference changeSound;  

    
    private Slider slider;

    private void Awake()
    {
        
        slider = GetComponent<Slider>();

       
        slider.onValueChanged.AddListener(PlayChangeSound);
    }

 
    private void PlayChangeSound(float value)
    {
        if (!changeSound.IsNull)
        {
            RuntimeManager.PlayOneShot(changeSound);
        }
    }

   
    public void OnSelect(BaseEventData eventData)
    {
        PlaySelectSound();
    }

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySelectSound();
    }

    
    private void PlaySelectSound()
    {
        if (!selectSound.IsNull)
        {
            RuntimeManager.PlayOneShot(selectSound);
        }
    }
}
