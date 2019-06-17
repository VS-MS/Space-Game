using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    public Transform target; // объект за которым надо следить
    public GameObject arrow; // стрелка

    void Update()
    {
        Vector3 targetOnScreen = Camera.main.WorldToViewportPoint(target.position);
        targetOnScreen.x = Mathf.Clamp01(targetOnScreen.x);
        Debug.Log("X = " + Mathf.Clamp01(targetOnScreen.x));
        targetOnScreen.y = Mathf.Clamp01(targetOnScreen.y);
        Debug.Log("Y = " + Mathf.Clamp01(targetOnScreen.y));

        arrow.transform.position = Camera.main.ViewportToWorldPoint(targetOnScreen);
    }
}
