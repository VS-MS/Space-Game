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

    }

}
