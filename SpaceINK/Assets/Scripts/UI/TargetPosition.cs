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

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
    private void FollowTarget_()
    {
        ColorDistance();
        SizeDistance();

        Vector3 toPosition = target.transform.position;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = GetAngleFromVectorFloat(dir);
        targetArrow.transform.localEulerAngles = new Vector3(0, 0, angle);

        float borderSize = 40f;

        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(target.transform.position);
        bool isOffscreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;
        //Debug.Log(isOffscreen + " " + targetPositionScreenPoint);

        if (isOffscreen)
        {
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, borderSize, Screen.width - borderSize);
            cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, borderSize, Screen.height - borderSize);

            Vector3 pointerWorldPosition = Camera.main.ScreenToWorldPoint(cappedTargetScreenPosition);
            targetArrow.transform.position = pointerWorldPosition;
            targetArrow.transform.localPosition = new Vector3(targetArrow.transform.localPosition.x, targetArrow.transform.localPosition.y, 0f);

        }
        else
        {
            Vector3 pointerWorldPosition = Camera.main.ScreenToWorldPoint(targetPositionScreenPoint);
            targetArrow.transform.position = pointerWorldPosition;
            targetArrow.transform.localPosition = new Vector3(targetArrow.transform.localPosition.x, targetArrow.transform.localPosition.y, 0f);

        }
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
            if(distance < 0.1)
            {
                colorArrow.a = 0f;
                targetArrow.GetComponent<SpriteRenderer>().color = colorArrow;
            }
            else
            {
                colorArrow.a = distance / 2;
                targetArrow.GetComponent<SpriteRenderer>().color = colorArrow;
            }
            
        }
        else
        {
            colorArrow.a = 1;
            targetArrow.GetComponent<SpriteRenderer>().color = colorArrow;
        }

    }

    void Update()
    {
        if(target)
        {
            if (target.tag == "EnemyShip")
            {
                if (target.GetComponent<Unit>().shipState != Unit.State.Die)
                {
                    FollowTarget_();
                }
                else
                {
                    Destroy(targetArrow);
                    Destroy(this);
                }
            }
            else
            {
                FollowTarget_();
            }
        }
        else
        {
            Destroy(targetArrow);
            Destroy(this);
        }
    }
}
