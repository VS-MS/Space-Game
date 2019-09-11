using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipBodyChng : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerShipBody;
    void Start()
    {
        for (int i = 0; i < playerShipBody.Length; i++)
        {
            if (DataSave.instance.cannonCount - 1 == i)
            {
                playerShipBody[i].SetActive(true);
            }
            else
                playerShipBody[i].SetActive(false);
        }
    }


}
