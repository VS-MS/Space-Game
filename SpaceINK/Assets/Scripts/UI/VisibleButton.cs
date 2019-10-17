using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisibleButton : MonoBehaviour
{
    public int lvlNomber; 
    private Button buttonLvl;
    void Start() 
    {
        buttonLvl = this.GetComponent<Button>();

        //Условие на текущий доступный уровень
        if(lvlNomber == DataSave.instance.levelComplite + 1)
        {
            buttonLvl.interactable = true;
            //тут надо будет сделать анимацию или еще что
        }
        //условие на все пройденные уровни
        if(lvlNomber <= DataSave.instance.levelComplite)
        {
            buttonLvl.interactable = true;
        }
        //Условие на все не пройденные уровни
        if (lvlNomber > DataSave.instance.levelComplite + 1)
        {
            buttonLvl.interactable = false;
        }
    }

}
