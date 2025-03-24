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
    [SerializeField] player_detectGround pDG;
    public bool isRunning { get; private set; }
    public bool isJumping { get; private set; }//下のjump関数でtrueにして、着地関数でfalseに(制作予定)

    void Update()
    {
         isRunning = run();
        isJumping = jump();
    }

    void FixedUpdate()
    {
        if (!isRunning)//update内でこの処理をしたら不安定だった為Fixed
        {
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);  // 動いていない時は横方向の速度だけゼロにする、また、!isRunningのところに色々足しやすいため、あとあと楽な処理
        }

    }

    public bool run()//左右の動き、移動中かどうかを返り値として返す
    {
        int movingDirection = 0;//向きのX方向を定めます。

        if (Input.GetKey(KeyCode.A))
        {
            movingDirection = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movingDirection = 1;
        }

        playerRb.velocity = new Vector2(pPA_1.runSpeed * movingDirection, playerRb.velocity.y);//早さにムラができるのは嫌だったのでvelocityを直接書き換えることにしました。
        if (movingDirection == 0) { return false; }
        else { return true; }
    }

    public bool jump()//ジャンプの関数、ジャンプ中かどうかを返り値として返す
    {
        if (pDG.isOnGround)//着地中に
        {
            if (Input.GetKey(KeyCode.W)) //Wが押されている間ジャンプ、GetKeyDownでも可
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
                playerRb.AddForce(new Vector2(0, pPA_1.jumpStrength), ForceMode2D.Impulse);
                return true;
            }

            else//着地中にジャンプしてなければそれはジャンプしていないということ
            {
                return false;
            }
        }

        else
        {
            return isJumping;
        }
    }

}