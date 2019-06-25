using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    public GameObject target; // объект за которым надо следить
    public GameObject arrow; // стрелка

    private void Awake()
    {

    }

    void Update()
    {
        if(target)
        {
            if (target.GetComponent<Unit>().shipState != Unit.State.Die)
            {
                Vector3 targetOnScreen = Camera.main.WorldToViewportPoint(target.transform.position);
                targetOnScreen.x = Mathf.Clamp01(targetOnScreen.x);
                targetOnScreen.y = Mathf.Clamp01(targetOnScreen.y);
                arrow.transform.position = Camera.main.ViewportToWorldPoint(targetOnScreen);
            }
            else
            {
                Destroy(arrow, .5f);
            }
        }
    }
}
