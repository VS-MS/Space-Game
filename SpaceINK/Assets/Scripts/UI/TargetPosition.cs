using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    public GameObject target; // объект за которым надо следить
    public GameObject arrow; // экземпляр стрелки
    private GameObject targetArrow; //ссылка для хранения объекта стрелки
    private Color colorArrow;

    private void Awake()
    {
        targetArrow = Instantiate(arrow, transform.position, transform.rotation);
        colorArrow = targetArrow.GetComponent<SpriteRenderer>().color;
    }

    private void Start()
    {

    }
    private void FollowTarget()
    {
        if (target)
        {
            if (target.tag == "EnemyShip")
            {
                if (target.GetComponent<Unit>().shipState != Unit.State.Die)
                {
                    Vector3 targetOnScreen = Camera.main.WorldToViewportPoint(target.transform.position);
                    targetOnScreen.x = Mathf.Clamp01(targetOnScreen.x);
                    targetOnScreen.y = Mathf.Clamp01(targetOnScreen.y);
                    targetArrow.transform.position = Camera.main.ViewportToWorldPoint(targetOnScreen);
                    ColorDistance();
                    SizeDistance();
                }
                else
                {
                    Destroy(targetArrow, .5f);
                }
            }
            else
            {
                Vector3 targetOnScreen = Camera.main.WorldToViewportPoint(target.transform.position);
                targetOnScreen.x = Mathf.Clamp01(targetOnScreen.x);
                targetOnScreen.y = Mathf.Clamp01(targetOnScreen.y);
                targetArrow.transform.position = Camera.main.ViewportToWorldPoint(targetOnScreen);
                ColorDistance();
                SizeDistance();
            }
                
        }
        else
        {
            Destroy(targetArrow, .5f);
        }
        
    }

    //изменение размера в зависимости от расстояния 
    private void SizeDistance()
    {
        var heading = targetArrow.transform.position - target.transform.position;
        var distance = heading.magnitude;
        if(distance < 200)
        {
            Vector3 vectorScale = new Vector3( (-distance + 400)/ 400, (-distance + 400) / 400);
            targetArrow.transform.localScale = vectorScale;
        }
        else
            targetArrow.transform.localScale = new Vector3 (0.5f, 0.5f, 1);
    }

    //скрываем цель, если она находиться на экране и включаем, если она за объектом
    private void ColorDistance()
    {
        var heading = targetArrow.transform.position - target.transform.position;
        var distance = heading.magnitude;
        if (distance < 2)
        {
            colorArrow.a = distance/2;
            targetArrow.GetComponent<SpriteRenderer>().color = colorArrow;
        }
        else
        {
            colorArrow.a = 1;
            targetArrow.GetComponent<SpriteRenderer>().color = colorArrow;
        }

    }

    void Update()
    {
        FollowTarget();
        //ColorDistance();
    }
}
