using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootsOutPlant : MonoBehaviour
{
    [SerializeField] GameObject seedPrehub;
    public plantButtonScript choosenButton;//装填ボタンのスクリプトを取得する(どうにかして)

    public plantDataClass plantData;//ここで撃ち出すタネのデータを取得する

    void Start()
    {
        plantData = seedPrehub.GetComponent<seedScript>().plantData;//とりあえず今はテスト用のタネのデータをスタートで取得する
    }
    void Update()
    {
        if(this.GetComponent<player_control>().isFacingRight)//ここで向きをvelocityに入れなければタネtrajectoryが向きを取得できない
        {
            plantData.actualVelocity = new Vector3(plantData.baseVelocity.x, plantData.baseVelocity.y, 0);
        }
        else
        {
            plantData.actualVelocity = new Vector3(-plantData.baseVelocity.x, plantData.baseVelocity.y, 0);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(choosenButton==null) return;
            if(!choosenButton.isReady) return;//装填中は撃てない
            choosenButton.used();
            Instantiate(seedPrehub, transform.position, Quaternion.identity);//seedPrehubは場合によって変わる
        }
    }  
}
