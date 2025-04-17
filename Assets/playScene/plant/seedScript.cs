using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class plantDataClass
{
    [Header("plantData")]
    public GameObject grewPrehub;
    [SerializeField] float speed;//投げた際のスピード
    public float maturePlantMovingSpeed;

    public Vector2 baseVelocity; 
    public Vector2 actualVelocity; //実際のスピード
    public bool isMature { get;  set; }//外側からは変更できない
}

public class seedScript : MonoBehaviour
{
    public plantDataClass plantData;
    GameObject player; 

    Rigidbody2D rb;

    bool touchedGround = false;

    void Start()
    {
     player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        if(player.GetComponent<player_control>().isFacingRight)
        {
            plantData.actualVelocity = new Vector3(plantData.baseVelocity.x, plantData.baseVelocity.y, 0);
        }
        else
        {
            plantData.actualVelocity = new Vector3(-plantData.baseVelocity.x, plantData.baseVelocity.y, 0);
        }
        shootOut();
    }

    public void shootOut()//射出された
    {
        rb.GetComponent<Rigidbody2D>().velocity = plantData.actualVelocity;
    }
    public void land(float angle)//着地した
    {
        GameObject obj = grow(angle);//ここの受け渡しが少し不恰好
        Rigidbody2D rbO = obj.GetComponent<Rigidbody2D>();   
        rbO.velocity = GetComponent<Rigidbody2D>().velocity.normalized * plantData.maturePlantMovingSpeed;
        rbO.isKinematic = true;
        Destroy(this.gameObject);
    }

    public GameObject grow(float angle)//成長する
    {
        GameObject obj = Instantiate(plantData.grewPrehub, transform.position, Quaternion.Euler(0, 0, angle));
        plantData.isMature = true;
        return obj;
    }

    public void receiceDirection(int dir)//左->0, 下->1, 右->2, 上->3 このスクリプトはこオブジェクトのコライダーに呼び出される
    {
        if (touchedGround) { return; }
        switch (dir)
        {
            case 0:
                land(270);
                touchedGround = true;
                break;
            
            case 1:
                land(0);
                touchedGround = true;
                break;
            
            case 2:
                land(90);
                touchedGround = true;
                break;
            
            case 3:
                land(180);
                touchedGround = true;
                break;
            
            default:
                Debug.LogError("Invalid direction");
                break;
        }
    }
}