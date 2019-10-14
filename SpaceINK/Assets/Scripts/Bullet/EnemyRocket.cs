using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : Unit
{
    //вектор выходящий из данного объекта в корабль игрока
    private Vector3 heading;
    public float rocketSpeed = 100;
    public float rocketRotation = 1; 
    public float rocketDamage = 3;

    private Vector3 targetingPosition;
    private GameObject playerShip;
    private Vector3 targetSpeed;
    
    private Rigidbody2D m_Rigidbody2D;


    // Start is called before the first frame update
    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
        rotationSpeed *= Random.Range(rocketRotation * 0.1f, rocketRotation);
        maxSpeed *= Random.Range(rocketSpeed * 0.5f, rocketSpeed * 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (shipState == State.Die && !flagDie)
        {
            foreach (Collider2D collider in this.GetComponents<Collider2D>())
            {
                collider.enabled = false;
            }
            //отключаем спрайты
            foreach (SpriteRenderer sprite in this.GetComponents<SpriteRenderer>())
            {
                sprite.enabled = false;
            }

            flagDie = true;
        }
        if (shipState != State.Die)
        {
            if (playerShip)
            {
                //FollowingPoint(playerShip.transform.position);
                Chase();
                //DebugLine();
            }
        }
    }

    private void Chase()
    {

        var headingAim = CalculateAim() - gameObject.transform.position;
        //distance - растояние от данного объекта до корабль игрока
        var distance = headingAim.magnitude;
        //direction - направление от данного объекта до игрока
        var direction = headingAim / distance;

        //Вычисляем угол между данным объектом и кораблем игрока в градах
        float angle = Mathf.Atan2(headingAim.y, headingAim.x) * Mathf.Rad2Deg - 90;

        //Ищем квантарион этого угла
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        
        //Ускоряемсяв сторону коробля игрока
        //СИла ускорения зависит от растояния до игрока, чем ближе, тем она меньше(пока не работает, слишком вялые противники с такой опцией)
        if (m_Rigidbody2D.velocity.magnitude <= maxSpeed)
        {

            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * (boostForce));
            if(distance <= 15)
            {
                //плавно прварачиваем объект
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed * (3.0f - distance / 7.5f));
            }
            else
            {
                //плавно прварачиваем объект
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
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
        //if (m_Rigidbody2D.velocity.magnitude <= (maxSpeed * Random.Range(0.5f, 1.5f)))
        if (m_Rigidbody2D.velocity.magnitude <= maxSpeed)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * boostForce);
        }
    }

    protected virtual Vector3 CalculateAim()
    {
        //По умолчанию турель стреляет прямо по цели, но, если цель движется, то нужно высчитать точку,
        //которая находится перед движущейся целью и по которой будет стрелять турель.
        //То есть турель должна стрелять на опережение
        targetingPosition = playerShip.transform.position;
        //Узнаем скорость цели
        targetSpeed = playerShip.GetComponent<Rigidbody2D>().velocity;
        //Debug.Log("PlayerShipVelosity = " + targetSpeed);
        //Высчитываем точку, перед мишенью, по которой нужно произвести выстрел, чтобы попасть по движущейся мишени
        //по идее, чем больше итераций, тем точнее будет положение точки для упреждающего выстрела
        for (int i = 0; i < 4; i++)
        {
            float dist = (transform.position - targetingPosition).magnitude;
            //Debug.Log("dist = " + dist);
            float timeToTarget = dist / (rocketSpeed * 500 );
            targetingPosition = playerShip.transform.position + targetSpeed * timeToTarget;
        }

        return targetingPosition;
    }


    public void DebugLine()
    {
            Debug.DrawLine(transform.position, playerShip.transform.position, Color.red);
        Debug.DrawLine(transform.position, CalculateAim(), Color.yellow);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Unit>().armorPoints > 0)
            {
                collision.gameObject.GetComponent<Unit>().ReceiveDamage(rocketDamage);
                shipState = State.Die;
                Instantiate(particleBoom[Random.Range(0, particleBoom.Length)], transform.position, transform.rotation);
                //отключаем все коллайдеры на объекте
                Destroy(gameObject, 10.01f);
            }
            
        }
        if (collision.tag == "Asteroid")
        {
            shipState = State.Die;
            Instantiate(particleBoom[Random.Range(0, particleBoom.Length)], transform.position, transform.rotation);
            //отключаем все коллайдеры на объекте
            Destroy(gameObject, 10.01f);
        }

    }

}
