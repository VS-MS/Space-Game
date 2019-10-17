﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyEndGame : MonoBehaviour
{
    public Button buttonX2;
    public TextMeshProUGUI textMoney;
    private Desc_ numberToString = new Desc_();

    private PlayerShip playerShip;

    private void Awake()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
    }
    public void UpdateMoneyEnd()
    {     
        //исправить!!! добавить нормальную переменную, в которой будем хранить набранные деньги за пройденный уровень.
        playerShip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
        textMoney.text = numberToString.ShortNumber(DataSave.instance.money - playerShip.moneyGet);

        if (DataSave.instance.money - playerShip.moneyGet <= 0)
        {
            buttonX2.interactable = false;
        }
    }

    public void MoneyX2()
    {
        //тут будет логи вознаграждения за просмотр видео
        playerShip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
        textMoney.text = numberToString.ShortNumber((DataSave.instance.money - playerShip.moneyGet) * 2);

        DataSave.instance.money = DataSave.instance.money + (DataSave.instance.money - playerShip.moneyGet);
    }
}