using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyEndGame : MonoBehaviour
{

    public TextMeshProUGUI textMoney;
    private Desc_ numberToString = new Desc_();

    private PlayerShip playerShip;

    private void Awake()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
    }
    public void UpdateMoneyEnd()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
        textMoney.text = numberToString.ShortNumber(DataSave.instance.money - playerShip.moneyGet);
    }
}
