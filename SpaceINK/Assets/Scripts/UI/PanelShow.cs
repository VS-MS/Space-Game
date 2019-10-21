using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelShow : MonoBehaviour
{
    public GameObject upgradeAnim;
    public GameObject selectAnim;
    public GameObject settingsAnim;

    //включить анимацию выезда для панели Upgrade, а для осталных анимацию заезда за экран
    public void ShowUpgrade()
    {
        upgradeAnim.GetComponent<Animator>().SetBool("Show", true);
        selectAnim.GetComponent<Animator>().SetBool("Show", false);
        settingsAnim.GetComponent<Animator>().SetBool("Show", false);
    }

    //включить анимацию выезда для панели Select, а для осталных анимацию заезда за экран
    public void ShowSelect()
    {
        upgradeAnim.GetComponent<Animator>().SetBool("Show", false);
        selectAnim.GetComponent<Animator>().SetBool("Show", true);
        settingsAnim.GetComponent<Animator>().SetBool("Show", false);
    }

    public void ShowSettings()
    {
        upgradeAnim.GetComponent<Animator>().SetBool("Show", false);
        selectAnim.GetComponent<Animator>().SetBool("Show", false);
        settingsAnim.GetComponent<Animator>().SetBool("Show", true);
    }
}
