using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    private TextMeshProUGUI textMoney;
    private Desc_ numberToString = new Desc_();

    private long moneyTmp;
    private float t = 0.0f;

    private void Awake()
    {
        moneyTmp = 0;
        textMoney = gameObject.GetComponent<TextMeshProUGUI>();
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
    }

    public void LateUpdate()
    {
        
    }
}
