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
        if(DataSave.instance.levelComplite + 1 == lvlNomber)
        {
            buttonLvl.interactable = false;
            //тут надо будет сделать анимацию или еще что
        }
        //условие на все пройденные уровни
        if(DataSave.instance.levelComplite + 1 < lvlNomber)
        {
            buttonLvl.interactable = false;
        }
        //Условие на все не пройденные уровни
        if (DataSave.instance.levelComplite + 1 > lvlNomber)
        {
            buttonLvl.interactable = true;
        }
    }

}
