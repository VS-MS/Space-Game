using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour 
{
    [SerializeField]
    private float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this, lifeTime);
    }

}
