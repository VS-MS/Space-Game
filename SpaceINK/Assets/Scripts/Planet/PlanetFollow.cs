using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFollow : MonoBehaviour
{
    private GameObject playerShip;
    [SerializeField]
    //скорость, с которой будет двигаться планета. Чем больше число, тем медленнее будет он двигаться.
    private float scrollSpeed;
    //переменная для определения глубины слоя планеты
    private float orderZ;
    private Vector3 startState;

    void Awake()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        orderZ = this.transform.position.z;
        startState = this.transform.position;
    }

    void Update()
    {
        if (playerShip)
        {
            Vector3 playerPosition = playerShip.transform.position;
            playerPosition.x /= scrollSpeed;
            playerPosition.y /= scrollSpeed;
            playerPosition.z = orderZ;
            this.transform.position = startState + playerPosition;
        }
    }
}
