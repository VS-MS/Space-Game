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
        StartCoroutine(CheckData());
    }
    //корутина для пропуска одного кадра, иначе найдем ссылку на объект, который будет уничтожен в следующем кадре.
    IEnumerator CheckData()
    { 
        yield return new WaitForEndOfFrame();
        dataSave = FindObjectOfType<DataSave>();
    }

    private void Start()
    {
        //dataSave = FindObjectOfType<DataSave>();
        //Debug.Log(dataSave.GetInstanceID());
        //Один раз обновляем текст до загрузки, 
        //иначе сначало будет видна надпись "Score", 
        //прежде чем сработает апдейт и она обновиться.
        //string money_ = dataSave.money.ToString();
        //textMoney.text = money_;
    }
    public void Update()
    {
        if(dataSave)
        {
            string money_ = dataSave.money.ToString();
            textMoney.text = money_;
        }
        //Debug.Log(dataSave.money);
    }

    public void LateUpdate()
    {
        
    }
}
