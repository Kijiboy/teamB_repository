using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_detectGround : MonoBehaviour
{
    public bool isOnGround { get; private set; }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}
