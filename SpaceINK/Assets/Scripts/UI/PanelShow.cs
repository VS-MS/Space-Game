using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelShow : MonoBehaviour
{
    public GameObject upgradeAnim;
    public GameObject selectAnim;

    //включить анимацию выезда для панели Upgrade, а для осталных анимацию заезда за экран
    public void ShowUpgrade()
    {
        upgradeAnim.GetComponent<Animator>().SetBool("Show", true);
        selectAnim.GetComponent<Animator>().SetBool("Show", false);
    }

    //включить анимацию выезда для панели Select, а для осталных анимацию заезда за экран
    public void ShowSelect()
    {
        upgradeAnim.GetComponent<Animator>().SetBool("Show", false);
        selectAnim.GetComponent<Animator>().SetBool("Show", true);
    }
}
