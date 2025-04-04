using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plantButtonScript : MonoBehaviour
{
    public int buttonNumber;
    public GameObject plantType;//このボタン（項目）が示す植物。
    [SerializeField] plantAdministerSystem pTBC;

    public void OnButtonPressed()
    {
        pTBC.buttonPressed(buttonNumber);
    }
}
