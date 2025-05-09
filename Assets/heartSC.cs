using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartSC : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<playerReciver>().gotHeal(1);
            Destroy(gameObject);
        }
    }
}
