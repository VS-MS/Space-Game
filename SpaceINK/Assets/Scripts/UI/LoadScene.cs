using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyMobile;

public class LoadScene : MonoBehaviour
{
    public string sceneNameLoad;
    public void LoadByName(string sceneName)
    {
        sceneNameLoad = sceneName;
        if (AdManager.instance.adCounter > 1)
        {
            //запускаем межстрочное объявление
            AdManager.instance.adCounter = 0;

            // Check if interstitial ad is ready
            bool isReady = Advertising.IsInterstitialAdReady();

            // Show it if it's ready
            if (isReady)
            {
                Advertising.ShowInterstitialAd();
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
 
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
        
    }

    // Subscribe to the event
    void OnEnable()
    {
        Advertising.InterstitialAdCompleted += InterstitialAdCompletedHandler;
    }

    // The event handler
    void InterstitialAdCompletedHandler(InterstitialAdNetwork network, AdLocation location)
    {
        Debug.Log("Interstitial ad has been closed.");
        SceneManager.LoadScene(sceneNameLoad);
    }

    // Unsubscribe
    void OnDisable()
    {
        Advertising.InterstitialAdCompleted -= InterstitialAdCompletedHandler;
    }

}
