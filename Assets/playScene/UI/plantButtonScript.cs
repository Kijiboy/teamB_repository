using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plantButtonScript : MonoBehaviour
{
    public int buttonNumber;
    
    [SerializeField] plantAdministerSystem pTBC;

    public void OnButtonPressed()
    {
        pTBC.buttonPressed(buttonNumber);
    }
}
