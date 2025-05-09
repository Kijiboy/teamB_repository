using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    [SerializeField] Image bar;
    [SerializeField] Sprite[] hp2;
    public void hpUpdated(int hp, int maxValue)
    {
        if(maxValue == 2)
        {
            Debug.Log(hp);
            bar.sprite= hp2[hp];
        }
    }
}
