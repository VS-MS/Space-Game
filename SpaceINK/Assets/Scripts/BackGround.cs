using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{

    private GameObject playerShip;
    [SerializeField]
    //переменная для определения глубины слоя бекграунда
    private float orderZ;
    [SerializeField]
    //скорость, с которой будет вращаться бекграунд. Чем больше число, тем медленнее будет он двигаться.
    private float scrollSpeed; 

    void Start()
    {
        
    }

    private void Awake()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = playerShip.transform.position;
        playerPosition.z = orderZ;
        //this.transform.position = Vector3.Lerp(transform.position, playerShip.transform.position, 2 * Time.deltaTime);
        this.transform.position = playerPosition;
        this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", playerShip.transform.position / scrollSpeed);

    }

}
