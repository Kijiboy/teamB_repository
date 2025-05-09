using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootsOutPlant : MonoBehaviour
{
    public plantButtonScript choosenButton;//装填ボタンのスクリプトを取得する(どうにかして)

    public plantDataClass plantData;//ここで撃ち出すタネのデータを取得する

    [SerializeField] player_control playerControl;

    [SerializeField] plantAdministerSystem pAS;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {   
            if(choosenButton==null) return;
            if(!choosenButton.isReady) return;//装填中は撃てない
            choosenButton.used();
            var plantSeed = Instantiate(pAS.selectedPlant, transform.position, Quaternion.identity);//seedPrehubは場合によって変わる
            plantSeed.GetComponent<seedScript>().pAS = pAS;
        }
    }  
}
