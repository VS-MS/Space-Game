using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
}
