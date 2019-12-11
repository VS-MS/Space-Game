using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject laserParticle;

    protected float speed = 150.0f;
    public float Speed { set { if (value > 0) speed = value; else speed = 1; } }
    protected Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    [SerializeField]
    protected float damage = 1.0f;
    public float Damage { set { damage = value; } }
    protected SpriteRenderer sprite;

    public Color color
    {
        set { sprite.color = value; }
    }

    /*
    private int maxKillCount = 1;
    public int MaxKillCount { set { maxKillCount = value; } }

    private int killCount = 0;
    */
    private void OnEnable()
    {
        StartCoroutine("TimeToDie");
    }

    private void OnDisable()
    {
        StopCoroutine("TimeToDie");
    }

    //Корутина(вместо Destroy), по истечению которой, объект вернется в пул
    IEnumerator TimeToDie()
    {
        yield return new WaitForSeconds(4.1f);
        this.gameObject.SetActive(false);
    }
}
