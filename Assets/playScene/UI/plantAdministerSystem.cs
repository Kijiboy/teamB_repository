using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plantAdministerSystem : MonoBehaviour
{
    public GameObject selectedPlant;
    public GameObject[] plantButtons;//plantのボタンに対応したデータはプレイ中も切り替わる予定です。
    public GameObject[] plantForEachButton;//上のボタンに対応した植物のデータを左に代入する予定ですけど少し複雑ですね

    [SerializeField] Color selectedColor;

    public void buttonPressed(int number)
    {
        foreach(GameObject button in plantButtons)
        {
            if (button.GetComponent<plantButtonScript>().buttonNumber == number)
            {
                button.GetComponent<Image>().color = selectedColor;//ボタンの色を変えた後
                button.GetComponent<Button>().enabled = false;//念の為ボタンとして使えなくする
            }

            else
            {
                button.GetComponent<Image>().color = Color.white;
                button.GetComponent<Button>().enabled = true;//別のボタンが選ばれたら使えるようにする
            }

            //↓ここでselectedPlantに選ばれた植物を代入
        }
    }
}
