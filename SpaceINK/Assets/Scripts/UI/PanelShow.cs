using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelShow : MonoBehaviour
{
    public GameObject upgradeAnim;
    public GameObject selectAnim;

    public void ShowUpgrade()
    {
        upgradeAnim.GetComponent<Animator>().SetBool("Show", true);
        selectAnim.GetComponent<Animator>().SetBool("Show", false);
    }
    public void ShowSelect()
    {
        upgradeAnim.GetComponent<Animator>().SetBool("Show", false);
        selectAnim.GetComponent<Animator>().SetBool("Show", true);
    }
}
