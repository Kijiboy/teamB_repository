using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantScript : MonoBehaviour
{
    [SerializeField] int plantType;
    [SerializeField] plant1Script plant1;

    [SerializeField] plant2Script plant2;

    const string componentName = "checkForSpaceToGrow";
    public void gotShot()
    {
        if (plantType == 1)
        {
            checkForSpaceToGrow cFSTG = plant1.transform.GetComponent<transferCheck>().check;
            if(cFSTG.haveSpaceToGrow())
            {
                plant1.gotShot();
            }
            else
            {
                Debug.Log("No space to grow");
            }
        }
        else if (plantType == 2)
        {
            checkForSpaceToGrow cFSTG = plant2.transform.GetComponent<transferCheck>().check;
            if(cFSTG.haveSpaceToGrow())
            {
                plant2.gotShot();
            }
        }
        else if (plantType == 3)
        {
            //plant3.grow();
        }
    }
}
