using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Unit {

    [Header("Main Weapon Settings")]
    [SerializeField]
    //Экземпляр пули игрока
    private PlayerBullet playerBullet;
    [SerializeField]
    private float bulletDamage;
    [SerializeField]
    private float bulletSpeed;
    public float fireDelta;//скорость стрельбы
    public int cannonCount;
    private float myTime;//время прошло от последнего выстрела


    
    [Header("Super Boost Settings")]
    public float boostMaxPoints;
    public float boostDelta;
    private float boostTime;
    public float boostPoints = 100;
    private float sbSpeedRatio = 1.5f;
    private float sbAccelerationRatio = 1.5f; 


    [Header("Super Shoot Settings")]
    public float superShootPoints;
    public float superShootMaxPoints;
    public float superShootDelta;
    private float superShootTime;
    public float ssDamageRatio; 

    private Vector2 m_velocite;
    private Rigidbody2D m_Rigidbody2D;
    private float rotationTime;
    public float RotationTime { set { if (value >= 0 & value <= 1) rotationTime = value; else rotationTime = 0.01f; } }

    [SerializeField]
    private Transform[] gunTransform;
    [HideInInspector]
    public GameObject boostWing;

    void Start ()
    {

    }
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        boostWing = this.transform.Find("BoostWing").gameObject;
        StartCoroutine(CheckData());
    }
    //корутина для пропуска одного кадра, иначе найдем ссылку на объект, который будет уничтожен в следующем кадре.
    IEnumerator CheckData() 
    {
        yield return new WaitForEndOfFrame();
        dataSave = FindObjectOfType<DataSave>();
        RefreshPlayerStat();
    }

    private void RefreshPlayerStat()
    {
        
        /*
         * Cannon
         */
        bulletDamage = dataSave.cannonDamage;
        Debug.Log(dataSave.cannonDamage);
        bulletSpeed = dataSave.cannonBulletSpeed;
        fireDelta = dataSave.cannonFireRate;
        cannonCount = dataSave.cannonCount;
        /*
         * SuperShot
         */
        superShootMaxPoints = dataSave.ssMaxTime; /*---*/ superShootPoints = dataSave.ssMaxTime;
        ssDamageRatio = dataSave.ssDamage;
        superShootDelta = dataSave.ssTimeReload;

        /*
         * Armor Shield
         */
        armorPoints = dataSave.shipArmor; /*---*/ maxArmorPoints = dataSave.shipArmor;
        shieldPoints = dataSave.shipShield; /*---*/ maxShieldPoints = dataSave.shipShield;
        shieldDelta = dataSave.shipShieldDelta;

        /*
         * Engine
         */
        maxSpeed = dataSave.shipMaxSpeed;
        boostForce = dataSave.shipAcceleration;
        rotationSpeed = dataSave.shipRotation;

        /*
         * SuperBoost
         */
        sbSpeedRatio = dataSave.sbMaxSpeed;
        sbAccelerationRatio = dataSave.sbAcceleration;
        boostPoints = dataSave.sbMaxTime; /*---*/ boostMaxPoints = dataSave.sbMaxTime;
        boostDelta = dataSave.sbTimeReload;

        

    }
    // Update is called once per frame
    void Update ()
    {
        if(shipState == State.Die)
        {
            this.GetComponent<PlayerShipControl>().enabled = false;
        }
        else
        {

        }


        if (myTime <= fireDelta) //считаемвремя до след выстрела
        {
            myTime = myTime + Time.deltaTime;
        }

        if (shieldTime <= shieldDelta) //считаем до восстановления щита
        {
            shieldTime += Time.deltaTime;
        }
        else//если таймер пройдет, проверяем, нужно ли восстановить щит
        {
            if (shieldPoints < maxShieldPoints)
            {
                shieldPoints += 1; //восстанавливаем щит на еденицу
            }
            shieldTime = 0;//обнуляем счетчик
        }

        //счетчик для super boost
        if (boostTime <= boostDelta)
        {
            boostTime += Time.deltaTime;
        }
        else
        {
            if (boostPoints < boostMaxPoints)
            {
                boostPoints += 1;
            }
            boostTime = 0;
        }

        //счетчик для super shoot
        if (superShootTime <= superShootDelta)
        {
            superShootTime += Time.deltaTime;
        }
        else
        {
            if (superShootPoints < superShootMaxPoints)
            {
                superShootPoints += 1;
            }
            superShootTime = 0;
        }
    }

    private void FixedUpdate()
    {
        
    }

    

    public void Move(float move)
    {
        //Переменная для поворота с ускорением
        if(rotationTime < 1f)
        {
            rotationTime += Time.deltaTime;
        }
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.transform.rotation * Quaternion.Euler(0f, 0f, rotationSpeed * move * Time.deltaTime), rotationTime);
        //m_Rigidbody2D.AddTorque(transform.up * rotationSpeed * move);
    }

    public void Move(Vector3 moveVector)
    {
        //ищем угол поворота джостика
        float angle = Mathf.Atan2(moveVector.y, moveVector.x) * Mathf.Rad2Deg - 90;
        //Ищем квантарион этого угла
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        //плавно прварачиваем объект
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, q, rotationSpeed * Time.deltaTime);
        //m_Rigidbody2D.AddTorque(transform.up * rotationSpeed * (float) moveVector.z);
        //m_Rigidbody2D.AddTorque(angle);
    }


    public void Boost()
    {
        

        if (m_Rigidbody2D.velocity.magnitude < maxSpeed)
        {
            m_Rigidbody2D.AddForce(transform.up * boostForce);
        }
    }

    public void Boost(float boostAxis, Vector3 boostVector)
    {
        /*if (m_Rigidbody2D.velocity.magnitude < maxSpeed)
        {
            //Debug.Log("Booooooost");
            m_Rigidbody2D.AddForce(transform.up * (boostForce * boostAxis));
        }*/

        //умножаем вектор направления джойстика на максимальную скорость,
        //чтобы узнать вектор желаемого ускорения и плюсуем его с вектор ускорения корабля
        //если они полностью совпадут, то их длинна будет равна двумя maxspeed.
        //С помощью этого условия, можно разворачивать корабль даже если максимальная скорость превышена.
        if ( (boostVector * maxSpeed + (Vector3)m_Rigidbody2D.velocity).magnitude <= maxSpeed * 2)
        {
            m_Rigidbody2D.AddForce(transform.up * (boostForce * boostAxis));
        }
    }

    //Супер ускорение.
    public void SuperBoost()
    {   
        
        if(boostPoints > 0)
        {
            boostPoints--;
            if (m_Rigidbody2D.velocity.magnitude < (maxSpeed * sbSpeedRatio))
            {
                m_Rigidbody2D.AddForce(transform.up * boostForce * sbAccelerationRatio);
                boostWing.transform.Find("Trail_6").gameObject.GetComponent<TrailRenderer>().emitting = true;
                boostWing.transform.Find("Trail_7").gameObject.GetComponent<TrailRenderer>().emitting = true;
            }
        }
        else
        {

            boostWing.transform.Find("Trail_6").gameObject.GetComponent<TrailRenderer>().emitting = false;
            boostWing.transform.Find("Trail_7").gameObject.GetComponent<TrailRenderer>().emitting = false;
        }
    }

    private void LaunchBullet(Transform bulletPosition)
    {
        PlayerBullet newBullet = Instantiate(playerBullet, bulletPosition.position, playerBullet.transform.rotation) as PlayerBullet;
        newBullet.Speed = bulletSpeed;
        newBullet.Parent = gameObject;
        newBullet.Direction = m_Rigidbody2D.transform.up;
        //newBullet.trailWidth = 0.5f;

        //newBullet.color = buletcolor;
        newBullet.Damage = bulletDamage;
    }

    private void LaunchSuperBullet(Transform bulletPosition)
    {
        PlayerBullet newBullet = Instantiate(playerBullet, bulletPosition.position, playerBullet.transform.rotation) as PlayerBullet;
        newBullet.Speed = bulletSpeed;
        newBullet.Parent = gameObject;
        newBullet.Direction = m_Rigidbody2D.transform.up;
        newBullet.trailWidth = 0.5f;

        //newBullet.color = buletcolor;
        newBullet.Damage = bulletDamage * ssDamageRatio;
    }


    public void SuperShoot()
    {
        if (superShootPoints > 0)
        {
            superShootPoints--;
            if (myTime > fireDelta)
            {
                if (cannonCount == 1)
                {
                    LaunchSuperBullet(gunTransform[0]);
                }
                else
                {
                    for (int i = 0; i < gunTransform.Length; i++)
                    {
                        LaunchSuperBullet(gunTransform[i]);
                    }
                }
                
                myTime = 0.0F;
            }
        }
        
    }
    

    public void Shoot()
    {
        if (myTime > fireDelta)
        {
            if (cannonCount == 1)
            {
                LaunchBullet(gunTransform[0]);
            }
            else
            {
                for (int i = 0; i < gunTransform.Length; i++)
                {
                    LaunchBullet(gunTransform[i]);
                }
            }
                
            myTime = 0.0F;
        }
    }

}
