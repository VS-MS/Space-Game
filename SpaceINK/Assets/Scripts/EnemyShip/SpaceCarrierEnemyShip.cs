using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCarrierEnemyShip : EnemyShip
{
    //вектор выходящий из данного объекта в корабль игрока
    private Vector3 heading;

    [SerializeField]
    //переменная для экземпляра класса корабля истребителя
    private FighterEnemyShip simpleDroneFighter;

    [SerializeField]
    //количество кораблей истребителей, которые может одновременно выпустить авианосец
    private int shipCount = 3;


    //массив, в котором будем хранить ссылки на экземпляры выпущенных кораблей
    private FighterEnemyShip[] droneFighter;
    

    public float fireDelta = 0.70F;//скорость стрельбы
    //private float nextFire = 0.5F;
    private float myTime = 0.5F;//время прошло от последнего выстрела

    private void Awake()
    {
        StatusSliderInt(5.0f, 3.0f);
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
        if (startPoint == new Vector3(0, 0, 0))
        {
            startPoint = gameObject.transform.position;
        }
        droneFighter = new FighterEnemyShip[shipCount];
        moneyCount = SetMoneyCount();
    }

    // Start is called before the first frame update
    void Start()
    {
        maxArmorPoints = armorPoints;
        maxShieldPoints = shieldPoints;
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
            this.transform.Find("SpriteRender").gameObject.SetActive(false);


        }
        else
        {
            //считаемвремя до след выстрела, счетчик работает, только если есть не выпущенный корабль
            if (myTime <= fireDelta && droneFighter.Length >= shipCount)
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


            if (playerShip)
            {
                //heading - вектор выходящий из данного объекта в корабль игрока
                heading = playerShip.transform.position - gameObject.transform.position;

                if (heading.sqrMagnitude < radarRadius * radarRadius) //проверяем, попал корабль игрока в зону радара
                {
                    state = State.Attack;
                    Chase();
                    LaunchDrone();
                }
                else
                {
                    state = State.Idle;
                    FollowingPoint(startPoint);
                }
                //DebugLine();
            }
            else
            {
                state = State.Idle;
                FollowingPoint(startPoint);
            }
        }

        
    }

    //функция запуска дронов
    private void LaunchDrone() 
    {
        //проверяем время для запуска следующего дрона
        if (myTime > fireDelta)
        {
            //перебераем массив с выпущенными дронами
            for (int i = 0; i < shipCount; i++)
            {
                //если в массиве есть уничтоженный корабль, выпускаем новый и записываем ссылку на экземпляр в эту ячейку массива
                if (droneFighter[i] == null)
                {
                    FighterEnemyShip newDroneFighter = Instantiate(simpleDroneFighter, this.transform.position, simpleDroneFighter.transform.rotation) as FighterEnemyShip;
                    newDroneFighter.armorPoints = (int)maxArmorPoints/15;
                    newDroneFighter.shieldPoints = (int)maxShieldPoints/15;
                    newDroneFighter.bulletDamage = bulletDamage;
                    newDroneFighter.startPoint = gameObject.transform.position;
                    newDroneFighter.parentPosition = gameObject.transform;
                    newDroneFighter.statusSlider.maxArmor = (int)maxArmorPoints / 15;
                    newDroneFighter.statusSlider.maxShield = (int)maxShieldPoints / 15;
                    newDroneFighter.GetComponent<Rigidbody2D>().velocity = this.transform.up * 10;
                    droneFighter[i] = newDroneFighter;
                    myTime = 0.0F;
                    break;
                }
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
        //плавно прварачиваем объект
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
        //Ускоряемсяв сторону коробля игрока
        //СИла ускорения зависит от растояния до игрока, чем ближе, тем она меньше(пока не работает, слишком вялые противники с такой опцией)
        if (/*distance >=2 &&*/ m_Rigidbody2D.velocity.magnitude <= maxSpeed)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * boostForce);
        }

    }
}
