using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plantAdministerSystem : MonoBehaviour
{
    [SerializeField] GameObject plantOnStart;
    public GameObject _selectedPlant;

    public GameObject selectedPlant
    {
        get => _selectedPlant;
        set
        {
            _selectedPlant = value;
            selectedPlantPointer = selectedPlant.GetComponent<seedScript>().plantData.pointerPrefab;
            onSelectedPlantModified();
        }
    }
    public plantDataClass _selectedPlantData;
    public plantDataClass selectedPlantData
    {
        get => _selectedPlantData;
        set
        {
            _selectedPlantData = value;
            onSelectedPlantModified(); // ← 値がセットされたときに呼び出したい関数
        }
    }

    public GameObject selectedPlantPointer;
    public GameObject[] plantButtons;//plantのボタンに対応したデータはプレイ中も切り替わる予定です。
    public GameObject[] plantForEachButton;//上のボタンに対応した植物のデータを左に代入する予定ですけど少し複雑ですね

    [SerializeField] Color selectedColor;
    [SerializeField] Color notSelectedColor;

    [SerializeField] movePlantAim mPA;

    [SerializeField] seedTrajectory sT;

    void Start()
    {
        selectedPlant = plantOnStart;
        selectedPlantData = new plantDataClass(); // ← 明示的にインスタンス化が必要な可能性
        selectedPlantData.grewPrehub = plantOnStart.GetComponent<seedScript>().plantData.grewPrehub;
        selectedPlantData.baseVelocity = plantOnStart.GetComponent<seedScript>().plantData.baseVelocity;
        selectedPlantData.actualVelocity = plantOnStart.GetComponent<seedScript>().plantData.actualVelocity;
        onSelectedPlantModified();
    }
    public void buttonPressed(int number)
    {
        foreach(GameObject button in plantButtons)
        {
            if (button.GetComponent<plantButtonScript>().buttonNumber == number)
            {
                button.GetComponent<Image>().color = selectedColor;//ボタンの色を変えた後
                button.GetComponent<Button>().enabled = false;//念の為ボタンとして使えなくする

                //↓ここでselectedPlantに選ばれた植物を代入
                selectedPlant = button.GetComponent<plantButtonScript>().plantSeed;
            }

            else
            {
                button.GetComponent<Image>().color = notSelectedColor;
                button.GetComponent<Button>().enabled = true;//別のボタンが選ばれたら使えるようにする
            }
        }
    }
    
    private void onSelectedPlantModified()
    {
        mPA.selectedPlantModified();
        sT.selectedPlantModified();
    }
}
