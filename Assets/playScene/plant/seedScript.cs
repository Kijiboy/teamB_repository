using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class plantDataClass
{
    [Header("plantData")]
    public GameObject grewPrehub;

    public GameObject pointerPrefab;

    [SerializeField] float speed;//投げた際のスピード
    public float maturePlantMovingSpeed;

    public Vector2 baseVelocity; 
    public Vector2 actualVelocity; //実際のスピード
    public bool isMature { get;  set; }//外側からは変更できない
    bool directionDecided = false;
}

public class seedScript : MonoBehaviour
{
    public plantDataClass plantData;//基本固定、参照用だけどinspectorから変更できるようにする
    public plantAdministerSystem pAS;

    GameObject player; 

    Rigidbody2D rb;

    bool touchedGround = false;

    bool LLTouchedGround = false;
    bool LRTouchedGround = false;
    bool URTouchedGround = false;
    bool ULTouchedGround = false;

    Vector2 gap = Vector2.zero;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        shootOut();
    }

    public void shootOut()//射出された
    {
        rb.GetComponent<Rigidbody2D>().velocity = pAS.selectedPlantData.actualVelocity;
    }
    public void land(float angle)//着地した
    {
        GameObject obj = grow(angle);//ここの受け渡しが少し不恰好
        angle -= 90;//なぜかここのタイミングで調整

        float gridSize = 1f; // グリッドのサイズ

        // スナップ処理（Tilemap のアンカーを考慮）

        Vector2 newPosition = (Vector2)transform.position + gap;
        Debug.Log(gap);

        float snappedX = Mathf.Floor(newPosition.x / gridSize) * gridSize + gridSize * 0.5f;
        float snappedY = Mathf.Floor(newPosition.y / gridSize) * gridSize + gridSize * 0.5f;

        // 法線ベクトルを計算
        obj.transform.position = new Vector2(snappedX, snappedY);

        // Rigidbody2D の設定
        if(obj.GetComponent<Rigidbody2D>() != null)
        {
            Rigidbody2D rbO = obj.GetComponent<Rigidbody2D>();
            rbO.velocity = GetComponent<Rigidbody2D>().velocity.normalized * plantData.maturePlantMovingSpeed;
            rbO.isKinematic = true;
        }

        // 現在のオブジェクトを破棄
        Destroy(this.gameObject);
    }

    public GameObject grow(float angle)//成長する
    {
        GameObject obj = Instantiate(plantData.grewPrehub, transform.position, Quaternion.Euler(0, 0, angle));
        plantData.isMature = true;
        return obj;
    }

    public void receiveDirection(int dir)//左->0, 下->1, 右->2, 上->3 このスクリプトはこオブジェクトのコライダーに呼び出される
    {
        if (touchedGround) { return; }

        var count = 0;
        if (LLTouchedGround) { count++; }
        if (LRTouchedGround) { count++; }
        if (ULTouchedGround) { count++; }
        if (URTouchedGround) { count++; }
        if (count > 1)
        {
            Debug.Log("斜めに乗る心配がない時");
            gap = Vector2.zero;
            
        }
        switch (dir)
        {
            case 0:
                gap.x = 0;
                land(270);//左
                touchedGround = true;
                Debug.Log("landed 1");
                break;
            
            case 1:
                gap.y = 0;
                land(0);//下
                touchedGround = true;
                Debug.Log("landed 2");

                break;
            
            case 2:
                gap.x = 0;
                land(90);//右
                touchedGround = true;
                Debug.Log("landed 3");
                break;
            
            case 3:
                gap.y = 0;
                land(180);//上
                touchedGround = true;
                Debug.Log("landed 4");
                break;
            
            default:
                Debug.LogError("Invalid direction");
                break;
        }
    }

    public void recieveDirection2(int dir)//こっちは4すみ
    {
         switch (dir)
        {
            case 4:
                LLTouchedGround = true;
                gap += new Vector2(-0.25f, -0.25f);
                break;
            
            case 5:
                LRTouchedGround = true;
                gap += new Vector2(0.25f, -0.25f);
                break;
            
            case 6:
                ULTouchedGround = true;
                gap += new Vector2(-0.25f, 0.25f);
                break;
            
            case 7:
                URTouchedGround = true;
                gap += new Vector2(0.25f, 0.25f);
                break;
            
            default:
                Debug.LogError("Invalid direction");
                break;
        }
    }
}