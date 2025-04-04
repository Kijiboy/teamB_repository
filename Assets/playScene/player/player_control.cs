using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player_control : MonoBehaviour
{
    [SerializeField] playerData pD;
    [SerializeField] public player_Physical_Ability pPA;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] player_detectGround pDG;
    public bool isRunning { get; private set; }
    public bool isJumping { get; private set; }//下のjump関数でtrueにして、着地関数でfalseに(制作予定)
    Vector2 playerScale ;

    private void Start()
    {
        pPA = pD.pPA_D;
        playerScale = transform.localScale;
    }

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

        playerRb.velocity = new Vector2(pPA.runSpeed * movingDirection, playerRb.velocity.y);//早さにムラができるのは嫌だったのでvelocityを直接書き換えることにしました。
        if (movingDirection == 0) { return false; }
        else
        {
            transform.localScale = new Vector2(playerScale.x * movingDirection, playerScale.y);
            return true;
        }
    }

    public bool jump()//ジャンプの関数、ジャンプ中かどうかを返り値として返す
    {
        if (pDG.isOnGround)//着地中に
        {
            if (Input.GetKeyDown(KeyCode.W)) //Wが押されている間ジャンプ、GetKeyDownでも可
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
                playerRb.AddForce(new Vector2(0, pPA.jumpStrength), ForceMode2D.Impulse);
                StartCoroutine(jumpPressedDuration());
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

    private IEnumerator jumpPressedDuration()//ボタンを押した長さに応じて高さを帰る為のスクリプト
    {
        for (int i = 0; i < 15/*変数作ろうと思うのですが、とりあえず15で*/; i++)
        {
            if (Input.GetKey(KeyCode.W)) { playerRb.AddForce(new Vector2(0, pPA.jumpStrength * 5)); }

            for (int x = 0; x < 10; x++)//とりあえず15フレーム
            {
                yield return null;
            }
        }
    }

}