using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tellDirection : MonoBehaviour//なんでスクリプト一緒にしたかは不明。気になったら分けてください
{
    [SerializeField] seedScript seedS;
    [SerializeField] int number;

    [SerializeField] Rigidbody2D rb;

    [SerializeField]bool isTragectory;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(number < 4)
            {
                seedS.receiveDirection(number);
            }
            
        }
        else if(collision.gameObject.CompareTag("Plant"))
        {
            Destroy(seedS.gameObject);
        }

        else if(collision.gameObject.CompareTag("plantGround"))
        {
            Destroy(seedS.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(number > 3)
            {
                seedS.recieveDirection2(number);
            }     
        }

        if(collision.gameObject.CompareTag("Plant"))
        {
            Destroy(seedS.gameObject);
        }

        else if(collision.gameObject.CompareTag("plantGround"))
        {
            Destroy(seedS.gameObject);
        }
    }
}

    