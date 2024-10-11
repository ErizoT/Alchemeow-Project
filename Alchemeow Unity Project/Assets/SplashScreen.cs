using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using FMODUnity;

public class SplashScreenScript : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter musicEmitter;
    private bool continuePlaying = true;

          IEnumerator Start()
    {
        SplashScreen.Begin();
        musicEmitter.Play();

        // Continue drawing the splash screen until it's finished or the user provides input
        while (!SplashScreen.isFinished)
        {
            SplashScreen.Draw();

            // Check for any key press or mouse click
            if (Input.anyKey || Input.GetMouseButtonDown(0))
            {
                musicEmitter.SetParameter("SkipSplash", 1);
                SplashScreen.Stop(SplashScreen.StopBehavior.FadeOut);  
           
                
                break;
            }

            yield return null;
        }
    }
}
