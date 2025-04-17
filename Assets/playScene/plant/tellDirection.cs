using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tellDirection : MonoBehaviour//なんでスクリプト一緒にしたかは不明。気になったら分けてください
{
    [SerializeField] seedScript seedS;
    [SerializeField] plantTragectoryBoxSC plantT;

    [SerializeField] int number;

    [SerializeField]bool isTragectory;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(!isTragectory)
            {
                seedS.receiceDirection(number);
            }

        }
        else if(collision.gameObject.CompareTag("Plant"))//今後帰るかも
        {
            Destroy(seedS.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("tellDirection: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(isTragectory)
            {
                plantT.receiceDirection(number);
            }
        }
    }
}
