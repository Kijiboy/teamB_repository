using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrageSC : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("plantGround"))
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<playerReciver>().gotDammage(1);
            Destroy(gameObject);
        }
    }
}
