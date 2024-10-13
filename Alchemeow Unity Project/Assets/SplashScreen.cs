using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using FMODUnity;

public class SplashScreenScript : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter musicEmitter;
    [SerializeField] private GameObject mainmenu;

    private bool splashSkipped = false;

    IEnumerator Start()
    {
        SplashScreen.Begin();
        musicEmitter.Play();

        float splashDuration = 15f; // Set the splash screen duration to 15 seconds
        float timeElapsed = 0f;

        // Continue drawing the splash screen until it's finished, the user provides input, or 15 seconds pass
        while (!SplashScreen.isFinished && timeElapsed < splashDuration)
        {
            SplashScreen.Draw();

            // Check for any key press or mouse click to skip
            if (Input.anyKey || Input.GetMouseButtonDown(0))
            {
                musicEmitter.SetParameter("SkipSplash", 1);
                SplashScreen.Stop(SplashScreen.StopBehavior.FadeOut);
                mainmenu.SetActive(true);
                splashSkipped = true; // Mark the splash screen as skipped
                break;
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

               // Activate the main menu once the splash screen has finished or 15 seconds have passed
        mainmenu.SetActive(true);
    }
}
