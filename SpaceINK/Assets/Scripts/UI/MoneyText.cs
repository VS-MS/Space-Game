using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    private TextMeshProUGUI textMoney;
    private Desc_ numberToString = new Desc_();
    private void Awake()
    {
        textMoney = gameObject.GetComponent<TextMeshProUGUI>();
    }
    //корутина для пропуска одного кадра, иначе найдем ссылку на объект, который будет уничтожен в следующем кадре.

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

        string money_ = numberToString.ShortNumber(DataSave.instance.money); //dataSave.money.ToString();
        long mon_tmp = DataSave.instance.money;
        textMoney.text = money_;

        //Debug.Log(dataSave.money);
    }

    public void LateUpdate()
    {
        
    }
}
