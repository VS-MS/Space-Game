using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Unit
{
    //Радиус обнаружения противника
    public float radarRadius = 30;
    //Радиус начала стрельбы
    public float attackRadius;
    //Точка, по которой будет стрелять турель в случае обнаружения цели, по умолчанию это сама цель
    protected Vector3 targetingPosition;
    protected GameObject playerShip;
    protected Vector3 targetSpeed;
    public float bulletDamage = 3; 
    [SerializeField]
    protected float bulletSpeed = 1;
    [SerializeField]
    private EnemyStatusSlider enemyStatusSlider;
    public EnemyStatusSlider statusSlider;

    //начальная позиция коробля, используется для возврата, если цель не найдена
    public Vector3 startPoint;

    //начальное и текущее состояние
    protected State state = State.Idle;

    protected Rigidbody2D m_Rigidbody2D;

    protected int SetMoneyCount()
    {
        int money_;
        money_ = (int)((maxShieldPoints + maxShieldPoints) * bulletDamage);
        return money_;
    }
    public void StatusSliderInt(float _upDis, float _scaleX) 
    {
        maxArmorPoints = armorPoints;
        maxShieldPoints = shieldPoints;
        //EnemyStatusSlider newStatusSlider = Instantiate(enemyStatusSlider, this.transform.position) as EnemyStatusSlider;
        EnemyStatusSlider newStatusSlider = Instantiate(enemyStatusSlider, this.transform.position, this.transform.rotation) as EnemyStatusSlider;
        newStatusSlider.maxArmor = armorPoints;
        newStatusSlider.maxShield = shieldPoints;
        newStatusSlider.enemyShip = this;
        newStatusSlider.upDis = _upDis;
        newStatusSlider.SetScaleX(_scaleX);
        statusSlider = newStatusSlider;
        //Debug.Log(armorPoints);
        //Debug.Log(maxArmorPoints);
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
            float timeToTarget = dist / (bulletSpeed);
            targetingPosition = playerShip.transform.position + targetSpeed * timeToTarget;
        }

        return targetingPosition;
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
        //if (m_Rigidbody2D.velocity.magnitude <= maxSpeed)
        if((m_Rigidbody2D.transform.up * maxSpeed + (Vector3)m_Rigidbody2D.velocity).magnitude <= maxSpeed * 2)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * boostForce);
        }
    }

    public void DebugLine()
    {
        if (state == State.Attack)
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
