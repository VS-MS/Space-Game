using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;

    private Vector3 targetPosition;
    //private RectTransform pointerRectTransform;
    public GameObject targetImage;
    private GameObject target;

    private void Awake()
    {
        targetPosition = new Vector3(-50, 0);
        target = Instantiate(targetImage, transform.position, transform.rotation);
        //Debug.Log(transform.Find("Pointer"));
        //pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();

    }
    private void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = GetAngleFromVectorFloat(dir);
        target.transform.localEulerAngles = new Vector3(0, 0, angle);

        float borderSize = 40f;

        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffscreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;
        Debug.Log(isOffscreen + " " + targetPositionScreenPoint);

        if (isOffscreen)
        {
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, borderSize, Screen.width - borderSize);
            cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, borderSize, Screen.height - borderSize);

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            target.transform.position = pointerWorldPosition;
            target.transform.localPosition = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y, 0f);

        }
        else
        {
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
            target.transform.position = pointerWorldPosition;
            target.transform.localPosition = new Vector3(target.transform.localPosition.x, target.transform.localPosition.y, 0f);

        }
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
