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
    //Переменная для присвоения позиции слоя, так и не разобрался где ее можно ввести в инспекторе.
    [SerializeField]
    private int orderInLayer = 0;

    void Start()
    {
        
    }

    private void Awake()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<MeshRenderer>().sortingOrder = orderInLayer;
    }
    // Update is called once per frame
    void Update()
    {
        if(playerShip)
        {
            Vector3 playerPosition = playerShip.transform.position;
            playerPosition.z = orderZ;
            //this.transform.position = Vector3.Lerp(transform.position, playerShip.transform.position, 2 * Time.deltaTime);
            this.transform.position = playerPosition;
            this.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", playerShip.transform.position / scrollSpeed);
        }
        

    }

}
