using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusSlider : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Оставить пустым, если нужно использовать основную камеру")]
    private GameObject Target = null;

    private Vector3 TargetPosition;

    void Start()
    {
        if (Target == null)
        {
            Target = Camera.main.gameObject;
        }
    }

    private void LateUpdate()
    {
        if (TargetPosition != Target.transform.position)
        {
            TargetPosition = Target.transform.position;
            Debug.Log(TargetPosition);
            transform.LookAt(TargetPosition);
        }
    }
}
