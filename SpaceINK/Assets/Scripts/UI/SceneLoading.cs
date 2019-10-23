using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EasyMobile;

public class SceneLoading : MonoBehaviour
{
    private string sceneName; 

    public Image loadingCircle;
    public TextMeshProUGUI loadingText;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(AsyncLoad());
    }

    public void LoadeSceneName(string sceneName_) 
    {
        sceneName = sceneName_;
        //StartCoroutine(AsyncLoad());
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
                StartCoroutine(AsyncLoad());
            }

        }
        else
        {
            StartCoroutine(AsyncLoad());
        }
    }

    public void RestartThisScene()
    {
        string s = SceneManager.GetActiveScene().name;
        int lvlNumber;
        try
        {
            lvlNumber = Convert.ToInt32(s);
        }
        catch (System.FormatException)
        {
            lvlNumber = 1;
            Debug.LogError("Не верное название сцены, сцена должна называться только целочисленным числом. Установленно значение по умолчанию равное = " + lvlNumber);
        }

        sceneName = (lvlNumber).ToString();

        Debug.Log("Загружаем сцену №" + sceneName);
        StartCoroutine(AsyncLoad());
    }

    public void LoadeNextScene()
    {
        string s = SceneManager.GetActiveScene().name;
        int lvlNumber;
        try
        {
            lvlNumber = Convert.ToInt32(s);
        }
        catch (System.FormatException)
        {
            lvlNumber = 1;
            Debug.LogError("Не верное название сцены, сцена должна называться только целочисленным числом. Установленно значение по умолчанию равное = " + lvlNumber);
        }

        sceneName = (lvlNumber + 1).ToString();

        Debug.Log("Загружаем сцену №" + sceneName);
        StartCoroutine(AsyncLoad());
    }
    IEnumerator AsyncLoad() 
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while(!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            loadingText.text = progress.ToString("0%");
            loadingCircle.fillAmount = progress;
            yield return null;
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
        StartCoroutine(AsyncLoad());
    }

    // Unsubscribe
    void OnDisable()
    {
        Advertising.InterstitialAdCompleted -= InterstitialAdCompletedHandler;
    }

}
