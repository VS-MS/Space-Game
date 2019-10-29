using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    private TextMeshProUGUI textMoney;
    private Desc_ numberToString = new Desc_();

    private long moneyTmp;
    //private bool moneyIsChange = false;
    // starting value for the Lerp
    private float t = 0.0f;

    private void Awake()
    {
        moneyTmp = 0;
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
        if(moneyTmp != DataSave.instance.money && t >= 1)
        {
            t = 0;
        }
        if (t < 1.0f)
        {
            t += 0.01f;
            try
            {
                moneyTmp = (long)Mathf.SmoothStep(moneyTmp, DataSave.instance.money, t);
            }
            catch
            {
                moneyTmp = 0;
                Debug.LogError("Error money");
            }
            
        }
        textMoney.text = numberToString.ShortNumber(moneyTmp);
        /*
        string money_;
        try
        {
            money_ = numberToString.ShortNumber(DataSave.instance.money);
        }
        catch (System.NullReferenceException)
        {
             money_ = "ERROR MONEY";
            Debug.LogError("Error money");
        }
        textMoney.text = money_;
        */
    }

    public void LateUpdate()
    {
        
    }
}
