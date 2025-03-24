using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class player_physical_ability
{
    [SerializeField] public float runSpeed;
    [SerializeField] public float jumpStrength;
}

public class player_control : MonoBehaviour
{
    [SerializeField] public player_physical_ability pPA_1;
    [SerializeField] Rigidbody2D playerRb;
    public bool isRunning { get; private set; }

    void Update()
    {
         isRunning = run();
    }

    void FixedUpdate()
    {
        if (!isRunning)
        {
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);  // 動いていない時は横方向の速度だけゼロにする、また、!isRunningのところに色々足しやすいため、あとあと楽な処理
        }

    }

    public bool run()
    {
        int movingDirection = 0;

        if (Input.GetKey(KeyCode.A))
        {
            movingDirection = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movingDirection = 1;
        }

        playerRb.velocity = new Vector2(pPA_1.runSpeed * movingDirection, playerRb.velocity.y);
        if (movingDirection == 0) { return false; }
        else { return true; }
    }
}