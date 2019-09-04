using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipSpeedTest : MonoBehaviour
{
    private TextMeshProUGUI textSpeed;
    private GameObject playerShip;
    // Start is called before the first frame update
    void Start()
    {
        textSpeed = gameObject.GetComponent<TextMeshProUGUI>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        textSpeed.text = playerShip.GetComponent<Rigidbody2D>().velocity.magnitude.ToString();
    }
}
