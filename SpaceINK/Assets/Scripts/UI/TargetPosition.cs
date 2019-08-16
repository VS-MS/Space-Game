using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    public GameObject target; // объект за которым надо следить
    public GameObject arrow; // экземпляр стрелки
    private GameObject targetSprite; //ссылка для хранения объекта стрелки

    private void Awake()
    {
        targetSprite = Instantiate(arrow, transform.position, transform.rotation);
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
                targetSprite.transform.position = Camera.main.ViewportToWorldPoint(targetOnScreen);
            }
            else
            {
                Destroy(targetSprite, .5f);
            }
        }
    }
}
