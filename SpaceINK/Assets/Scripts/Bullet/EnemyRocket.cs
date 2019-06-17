using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : Unit
{
    //вектор выходящий из данного объекта в корабль игрока
    private Vector3 heading;

    private Vector3 targetingPosition;
    private GameObject playerShip;
    private Vector3 targetSpeed;
    [SerializeField]
    private float rocketDamage = 3; 
    private Rigidbody2D m_Rigidbody2D;


    // Start is called before the first frame update
    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
        rotationSpeed *= Random.Range(0.5f, 1.5f);
        maxSpeed *= Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (shipState == State.Die)
        {
            //отключаем все коллайдеры на объекте
            foreach (Collider2D collider in this.GetComponents<Collider2D>())
            {
                collider.enabled = false;
            }
            //отключаем спрайты
            foreach (SpriteRenderer sprite in this.GetComponents<SpriteRenderer>())
            {
                sprite.enabled = false;
            }
        }
        else
        {
            if (playerShip)
            {
                FollowingPoint(playerShip.transform.position);
                DebugLine();
            }
        }
            

    }

    protected void FollowingPoint(Vector3 point)
    {
        //вектор от коробля до точки следования
        var headingAim = point - gameObject.transform.position;
        //находим растояние
        var distance = headingAim.magnitude;
        //вектор направления
        var direction = headingAim / distance;
        //Вычисляем угол между данным объектом и точкой следования
        float angle = Mathf.Atan2(headingAim.y, headingAim.x) * Mathf.Rad2Deg - 90;
        //Ищем квантарион этого угла
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        //плавно прварачиваем объект
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
        if (m_Rigidbody2D.velocity.magnitude <= (maxSpeed * Random.Range(0.5f, 1.5f)))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * boostForce);
        }
    }


    public void DebugLine()
    {
            Debug.DrawLine(transform.position, playerShip.transform.position, Color.red);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Unit>().armorPoints > 0)
            {
                collision.gameObject.GetComponent<Unit>().ReceiveDamage(rocketDamage);
                Destroy(gameObject); 
            }
            else
            {

            }
        }
        
    }

}
