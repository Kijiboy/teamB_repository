using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_detectGround : MonoBehaviour
{
    public bool isOnGround { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOnGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOnGround = false;
    }
}
