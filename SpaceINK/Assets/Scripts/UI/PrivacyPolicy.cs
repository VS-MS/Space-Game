using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyPolicy : MonoBehaviour
{
    public string url_;
    public void OpenPrivacyPolicy()
    {
        Application.OpenURL(url_);
    }
}
