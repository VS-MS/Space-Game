using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Unit {

    [Header("Main Weapon Settings")]
    [SerializeField]
    //Экземпляр обычной пули игрока
    private PlayerBullet playerBulletSimple;
    [SerializeField]
    //Экземпляр супер пули игрока
    private PlayerBullet playerBulletSuper; 

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

    private StatusSlider playerBar;

    void Start ()
    {
        RefreshPlayerStat();
        playerBar = FindObjectOfType<StatusSlider>();
        if(playerBar)
        {
            playerBar.RefreshStat();
        }
        else
        {
            Debug.LogError("StatusSlider not found");
            Debug.LogWarning("Add StatusSlider obj to the scene!");
        }
    }
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        boostWing = this.transform.Find("BoostWing").gameObject;
    }

    private void RefreshPlayerStat()
    {
        
        /*
         * Cannon
         */
        bulletDamage = DataSave.instance.cannonDamage;
        bulletSpeed = DataSave.instance.cannonBulletSpeed;
        fireDelta = DataSave.instance.cannonFireRate;
        cannonCount = DataSave.instance.cannonCount;
        /*
         * SuperShot
         */
        superShootMaxPoints = DataSave.instance.ssMaxTime; /*---*/ superShootPoints = DataSave.instance.ssMaxTime;
        ssDamageRatio = DataSave.instance.ssDamage;
        superShootDelta = DataSave.instance.ssTimeReload;

        /*
         * Armor Shield
         */
        armorPoints = DataSave.instance.shipArmor; /*---*/ maxArmorPoints = DataSave.instance.shipArmor;
        shieldPoints = DataSave.instance.shipShield; /*---*/ maxShieldPoints = DataSave.instance.shipShield;
        shieldDelta = DataSave.instance.shipShieldDelta;

        /*
         * Engine
         */
        maxSpeed = DataSave.instance.shipMaxSpeed;
        boostForce = DataSave.instance.shipAcceleration;
        rotationSpeed = DataSave.instance.shipRotation;

        /*
         * SuperBoost
         */
        sbSpeedRatio = DataSave.instance.sbMaxSpeed;
        sbAccelerationRatio = DataSave.instance.sbAcceleration;
        boostPoints = DataSave.instance.sbMaxTime; /*---*/ boostMaxPoints = DataSave.instance.sbMaxTime;
        boostDelta = DataSave.instance.sbTimeReload;

        

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

    public void Boost(Vector3 boostVector)
    {
        //умножаем вектор направления джойстика на максимальную скорость,
        //чтобы узнать вектор желаемого ускорения и плюсуем его с вектор ускорения корабля
        //если они полностью совпадут, то их длинна будет равна двумя maxspeed.
        //С помощью этого условия, можно разворачивать корабль даже если максимальная скорость превышена.
        if ( (boostVector * maxSpeed + (Vector3)m_Rigidbody2D.velocity).magnitude <= maxSpeed * 2)
        {
            m_Rigidbody2D.AddForce((transform.up + boostVector) * (boostForce * Time.deltaTime));
        }
    }

    //Супер ускорение.
    public void SuperBoost()
    {   
        
        if(boostPoints > 0)
        {
            boostPoints-= Time.deltaTime * 65;
            if (m_Rigidbody2D.velocity.magnitude < (maxSpeed * sbSpeedRatio))
            {
                m_Rigidbody2D.AddForce(transform.up * boostForce * sbAccelerationRatio * Time.deltaTime);
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
        PlayerBullet newBullet = Instantiate(playerBulletSimple, bulletPosition.position, playerBulletSimple.transform.rotation) as PlayerBullet;
        newBullet.Speed = bulletSpeed;
        newBullet.Parent = gameObject;
        newBullet.Direction = m_Rigidbody2D.transform.up;
        //newBullet.trailWidth = 0.5f;

        //newBullet.color = buletcolor;
        newBullet.Damage = bulletDamage;
    }

    private void LaunchSuperBullet(Transform bulletPosition)
    {
        PlayerBullet newBullet = Instantiate(playerBulletSuper, bulletPosition.position, playerBulletSuper.transform.rotation) as PlayerBullet;
        newBullet.Speed = bulletSpeed;
        newBullet.Parent = gameObject;
        newBullet.Direction = m_Rigidbody2D.transform.up;
        //newBullet.trailWidth = 0.3f;

        //newBullet.color = buletcolor;
        newBullet.Damage = bulletDamage * ssDamageRatio;
    }


    public void SuperShoot()
    {
        if (superShootPoints > 0)
        {
            //superShootPoints--;
            if (myTime > fireDelta / 2 && superShootPoints >= 5)
            {
                superShootPoints -= 5;
                //В зависимости от уровня пушки, будем стрелять 1, 2, 3, или 5 пушка за выстрел
                switch (cannonCount)
                {
                    case 1:
                        LaunchSuperBullet(gunTransform[0]);
                        break;
                    case 2:
                        LaunchSuperBullet(gunTransform[1]);
                        LaunchSuperBullet(gunTransform[2]);
                        break;
                    case 3:
                        for (int i = 0; i <= 2; i++)
                        {
                            LaunchSuperBullet(gunTransform[i]);
                        }
                        break;
                    case 4:
                        for (int i = 1; i <= 4; i++)
                        {
                            LaunchSuperBullet(gunTransform[i]);
                        }
                        break;
                    case 5:
                        for (int i = 0; i < gunTransform.Length; i++)
                        {
                            LaunchSuperBullet(gunTransform[i]);
                        }
                        break;
                }
                myTime = 0.0F;
            }
        }
        
    }
    

    public void Shoot()
    {
        if (myTime > fireDelta)
        {
            //В зависимости от уровня пушки, будем стрелять 1, 2, 3, или 5 пушка за выстрел
            switch (cannonCount)
            {
                case 1:
                    LaunchBullet(gunTransform[0]);
                    break;
                case 2:
                    LaunchBullet(gunTransform[1]);
                    LaunchBullet(gunTransform[2]);
                    break;
                case 3:
                    for (int i = 0; i <= 2; i++)
                    {
                        LaunchBullet(gunTransform[i]);
                    }
                    break;
                case 4:
                    for (int i = 1; i <= 4; i++)
                    {
                        LaunchBullet(gunTransform[i]);
                    }
                    break;
                case 5:
                    for (int i = 0; i < gunTransform.Length; i++)
                    {
                        LaunchBullet(gunTransform[i]);
                    }
                    break;
            }

            myTime = 0.0F;
        }
    }


}
