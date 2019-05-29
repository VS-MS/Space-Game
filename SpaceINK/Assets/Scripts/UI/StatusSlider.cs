using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusSlider : MonoBehaviour
{
    public Slider sliderArmor;
    private PlayerShip playerShip;
    // Start is called before the first frame update
    void Start()
    {
        playerShip = FindObjectOfType<PlayerShip>();
    }

    private void Awake()
    {
        playerShip = FindObjectOfType<PlayerShip>();
        sliderArmor.maxValue = playerShip.armorPoints;
        sliderArmor.value = playerShip.armorPoints;
    }
    // Update is called once per frame
    void Update()
    {
        sliderArmor.value = playerShip.armorPoints;
    }
}
