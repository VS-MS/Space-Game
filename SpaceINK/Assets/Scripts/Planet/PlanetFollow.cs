using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFollow : MonoBehaviour
{
    private GameObject playerShip;
    [SerializeField]
    //скорость, с которой будет двигаться планета. Чем больше число, тем медленнее будет он двигаться.
    private float scrollSpeed;
    private Vector3 startState;

    void Awake()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        startState = this.transform.position;
    }

    void Update()
    {
        if (playerShip)
        {
            Vector3 playerPosition = playerShip.transform.position;
            playerPosition.x /= scrollSpeed;
            playerPosition.y /= scrollSpeed;
            this.transform.position = startState + playerPosition;
        }
    }
}
