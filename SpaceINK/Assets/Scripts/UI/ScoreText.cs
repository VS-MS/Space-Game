using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{

    //ссылка на обект типа DataSave для сохранения прогресса
    private DataSave dataSave;
    private TextMeshProUGUI textScore;

    private void Awake()
    {
        textScore = gameObject.GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        //приходится в апдейте обновлять, так как объект dataSave может быть удален во время загрузки сцены
        //ПРОВЕРИТЬ НА ПРОИЗВОДИТЕЛЬНОСТЬ!
        dataSave = FindObjectOfType<DataSave>();
        string money_ = dataSave.money.ToString();
        
        textScore.text = money_;
    }

}
