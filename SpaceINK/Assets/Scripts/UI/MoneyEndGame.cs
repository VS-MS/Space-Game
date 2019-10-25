using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EasyMobile;

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

    public void Update() 
    {
        if (Advertising.IsRewardedAdReady())
        {
            buttonX2.interactable = true;
        }
        else
        {
            buttonX2.interactable = false;
        }
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
        /*
        playerShip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
        textMoney.text = numberToString.ShortNumber((DataSave.instance.money - playerShip.moneyGet) * 2);

        DataSave.instance.money = DataSave.instance.money + (DataSave.instance.money - playerShip.moneyGet);
        */
        
        AdManager.instance.adCounter = 0;
        
        // Check if rewarded ad is ready
        bool isReady = Advertising.IsRewardedAdReady();
        // Show it if it's ready
        if (isReady)
        {
            Advertising.ShowRewardedAd();
        }
        

    }

    // Subscribe to rewarded ad events
    void OnEnable()
    {
        Advertising.RewardedAdCompleted += RewardedAdCompletedHandler;
        Advertising.RewardedAdSkipped += RewardedAdSkippedHandler;
    }

    // Unsubscribe events
    void OnDisable()
    {
        Advertising.RewardedAdCompleted -= RewardedAdCompletedHandler;
        Advertising.RewardedAdSkipped -= RewardedAdSkippedHandler;
    }

    // Event handler called when a rewarded ad has completed
    void RewardedAdCompletedHandler(RewardedAdNetwork network, AdPlacement location)
    {
        Debug.Log("Rewarded ad has completed. The user should be rewarded now.");

        playerShip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();
        textMoney.text = numberToString.ShortNumber((DataSave.instance.money - playerShip.moneyGet) * 2);
        DataSave.instance.money = DataSave.instance.money + (DataSave.instance.money - playerShip.moneyGet);
    }

    // Event handler called when a rewarded ad has been skipped
    void RewardedAdSkippedHandler(RewardedAdNetwork network, AdPlacement location)
    {
        Debug.Log("Rewarded ad was skipped. The user should NOT be rewarded.");
    }
}
