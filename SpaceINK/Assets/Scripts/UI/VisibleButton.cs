using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisibleButton : MonoBehaviour
{
    public int lvlNomber;
    public GameObject imageButton;
    private Button buttonLvl;
    void Start() 
    {
        buttonLvl = this.GetComponent<Button>();
        //Условие на текущий доступный уровень
        if(lvlNomber == DataSave.instance.levelComplite + 1)
        {
            buttonLvl.interactable = true;
            imageButton.SetActive(true);
            //тут надо будет сделать анимацию или еще что
        }
        //условие на все пройденные уровни
        if(lvlNomber <= DataSave.instance.levelComplite)
        {
            buttonLvl.interactable = true;
            imageButton.SetActive(false);
        }
        //Условие на все не пройденные уровни
        if (lvlNomber > DataSave.instance.levelComplite + 1)
        {
            buttonLvl.interactable = false;
            imageButton.SetActive(false);
        }
    }

}
