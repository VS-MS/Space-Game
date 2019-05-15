using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemyShip : Unit
{
    //Радиус обнаружения противника
    public float radarRadius = 30;
    //Точка, по которой будет стрелять турель в случае обнаружения цели, по умолчанию это сама цель
    public Vector3 targetingPosition;


    private enum State
    {
        Idle,       //состояние покоя
        Atack,      //цель найдена, атакуем
        Patrol,     //патрулирование
    }
    //начальное и текущее состояние
    private State state = State.Idle;
    public GameObject playerShip;
    //вектор выходящий из данного объекта в корабль игрока
    private Vector3 heading;

    [SerializeField]
    private SimpleBullet simpleBullet;
    [SerializeField]
    private float bulletSpeed = 0.1f;
    public GameObject[] turretArray;
    [SerializeField]
    private float turretRotationSpeed;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 previousTargetPosition;
    private Vector3 targetSpeed;
    //начальная позиция коробля, используется для возврата, если цель не найдена
    private Vector3 startPoint;

    public float fireDelta = 0.70F;//скорость стрельбы
    //private float nextFire = 0.5F;
    private float myTime = 0.5F;//время прошло от последнего выстрела


    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
        startPoint = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (myTime <= fireDelta) //считаемвремя до след выстрела
        {
            myTime = myTime + Time.deltaTime;
        }

        if (playerShip)
        {
            //heading - вектор выходящий из данного объекта в корабль игрока
            heading = playerShip.transform.position - gameObject.transform.position;

            if (heading.sqrMagnitude < radarRadius * radarRadius) //проверяем, попал корабль игрока в зону радара
            {
                state = State.Atack;
                Chase();
                //Shoot();
            }
            else
            {
                state = State.Idle;
                FollowingPoint(startPoint);
            }
            DebugLine();

            previousTargetPosition = playerShip.transform.position;
        }
        else
        {
            state = State.Idle;
            FollowingPoint(startPoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FollowingPoint(Vector3 point)
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
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1.5f);
        if (m_Rigidbody2D.velocity.magnitude <= maxSpeed)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * boostForce);
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
        //плавно прварачиваем объект
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
        //Поворачиваем башни в сторону цели
        for (int i = 0; i < turretArray.Length; i++)
        {
            turretArray[i].transform.rotation = Quaternion.Slerp(turretArray[i].transform.rotation, q, Time.deltaTime * turretRotationSpeed);
            turretArray[i].GetComponent<TurretEnemy>().ShootTurret();
            turretArray[i].GetComponent<TurretEnemy>().bulletSpeed = bulletSpeed;
        }   
        //Ускоряемсяв сторону коробля игрока
        //СИла ускорения зависит от растояния до игрока, чем ближе, тем она меньше(пока не работает, слишком вялые противники с такой опцией)
        if (/*distance >=2 &&*/ m_Rigidbody2D.velocity.magnitude <= maxSpeed)
        {
            //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * (boostForce * (heading.magnitude / radarRadius)));
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
            float timeToTarget = dist / (bulletSpeed * 50);
            targetingPosition = playerShip.transform.position + targetSpeed * timeToTarget;
        }

        return targetingPosition;
    }

    void DebugLine()
    {
        if (state == State.Atack)
        {
            Debug.DrawLine(transform.position, playerShip.transform.position, Color.red);
            Debug.DrawLine(transform.position, CalculateAim(), Color.yellow);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radarRadius);
    }
}
