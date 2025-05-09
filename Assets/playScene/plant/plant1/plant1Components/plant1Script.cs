using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class plant1Script : MonoBehaviour
{
    [SerializeField] GameObject plantTip;
    [SerializeField] GameObject connectorPrehub;
    // Start is called before the first frame update
    List<GameObject> plantParts = new List<GameObject>();
    void Start()
    {
        plantParts.Add(plantTip);
    }
    public void gotShot()
    {
        foreach (GameObject part in plantParts)
        {
            part.transform.localPosition = new Vector3(part.transform.localPosition.x, part.transform.localPosition.y + 1, 0);
        }
        plantParts.Add(Instantiate(connectorPrehub, transform.localPosition, plantTip.transform.rotation, transform));
    }
}
