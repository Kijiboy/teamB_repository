using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant2Script : MonoBehaviour
{
    bool isMature = false;
    
    [SerializeField] GameObject fruitPrefab;
    [SerializeField] GameObject immaturePlant;
    [SerializeField] GameObject grownPrefab;

    public void gotShot()
    {
        if(!isMature)
        {
            grow();
        }
        else
        {
            formFruit();
        }
    }

    public void grow()
    {
        Instantiate(grownPrefab, transform.position, transform.rotation);
        Destroy(immaturePlant);
        isMature = true;
    }

    public void formFruit()
    {
        Vector3 offset = transform.up * 1.5f;
        Instantiate(fruitPrefab, transform.position + offset, Quaternion.identity);
    }
}
