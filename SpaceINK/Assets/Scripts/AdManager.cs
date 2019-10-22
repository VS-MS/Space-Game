using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (!RuntimeManager.IsInitialized())
        {
            RuntimeManager.Init();
        }
    }
}
