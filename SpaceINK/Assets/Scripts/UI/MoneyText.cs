using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{

    //ссылка на обект типа DataSave для сохранения прогресса
    private DataSave dataSave;
    private TextMeshProUGUI textMoney;

    private void Awake()
    {
        textMoney = gameObject.GetComponent<TextMeshProUGUI>();
        dataSave = FindObjectOfType<DataSave>();
    }
    // Update is called once per frame
    void Update()
    {   
        string money_ = dataSave.money.ToString();
        
        textMoney.text = money_;
    }

}
