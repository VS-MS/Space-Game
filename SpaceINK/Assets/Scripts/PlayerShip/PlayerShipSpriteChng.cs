using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipSpriteChng : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Sprite[] playerShipSkin;
    void Start() 
    {
        SpriteRenderer spriteSkin = gameObject.GetComponent<SpriteRenderer>();
        spriteSkin.sprite = playerShipSkin[DataSave.instance.cannonCount - 1];

        /*
        switch (DataSave.instance.cannonCount)
        {
            case 1:
                spriteSkin.sprite = playerShipSkin[1];
                break;
            case 2:
                Debug.Log("2");
                break;
            case 3:
                Debug.Log("3");
                break;
            case 4:
                Debug.Log("4");
                break;
            case 5:
                Debug.Log("5");
                break;
        }
           */ 
    }

}
