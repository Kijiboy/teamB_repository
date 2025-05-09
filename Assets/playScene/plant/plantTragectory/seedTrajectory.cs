using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class seedTrajectory : MonoBehaviour
{
    private bool drawArc = true;
    private bool drawArcAllTime = false;
    [SerializeField] int segmentCount = 60;
    [SerializeField] float predictionTime = 6.0F;
    [SerializeField, Tooltip("放物線のマテリアル")]
    private Material arcMaterial;
    [SerializeField, Tooltip("放物線の幅")]
    private float arcWidth = 0.02F;
    private LineRenderer[] lineRenderers;
    [SerializeField] private plantAdministerSystem pAS;
    private Vector3 initialVelocity;
    private Vector3 arcStartPosition;

    [SerializeField] float trajectorySpeed;//元々ゆっくり描画する予定だったのを瞬時に変えたからこれ0にしになきゃバグる

    [SerializeField] LayerMask collisionLayer; // インスペクターで設定可能

    [SerializeField] GameObject player;
    [SerializeField] player_control player_Control;
    Rigidbody2D rb;
    public GameObject pointerPrefab;
    GameObject pointerObject;

    GameObject arcObjectsParent;

    Vector2[] basePlayerPositions;

    Vector2[] lineRendererPositions1;
    Vector2[] lineRendererPositions2;

    bool isShowingPointer;
    

    void Start()
    {
        CreateLineRendererObjects();//ラインレンダラーのオブジェクトを必要な数を作成してるっぽい
        rb = GetComponent<Rigidbody2D>();
        drawArc = true;
        startTragectory();
    }


    public void selectedPlantModified()
    {
        pointerPrefab = pAS.selectedPlantPointer;//本当はplantDataに格納したのをから取得したいけど、なんかバグる
        if (pointerObject != null){ Destroy(pointerObject);}
        pointerObject = Instantiate(pointerPrefab, Vector3.zero, Quaternion.identity);
    }
    void FixedUpdate()
    {
        if (!drawArc) return; // drawArc が false の場合は処理をスキップ

        
        float timeStep = predictionTime / segmentCount;
        bool draw = false;
        float hitTime = float.MaxValue;
        Vector2 hitNormal = Vector2.zero;

        for (int i = 0; i < lineRenderers.Length; i++)
        {
            float startTime = timeStep * i;
            float endTime = startTime + timeStep;

            SetLineRendererPosition(i, startTime, endTime, !draw);

            if (!draw)
            {
                hitTime = GetArcHitTime(startTime, endTime , out  hitNormal);
                if (hitTime != float.MaxValue)
                {
                    draw = true;
                }
            }
        }

        if (hitTime != float.MaxValue)
        {
            if(pointerObject == null)
            {
                pointerPrefab = pAS.selectedPlantPointer;
                pointerObject = Instantiate(pointerPrefab, Vector3.zero, Quaternion.identity);
            }
            Vector3 hitPosition = GetArcPositionAtTime(hitTime);
            ShowPointer(hitPosition, hitNormal);
        }
        else
        {
            Destroy(pointerObject);
        }
    }
    
                
    private void startTragectory()
    {
        arcStartPosition = player.transform.position;

        float timeStep = predictionTime / segmentCount;//一セグメント毎の描画する時間の範囲（未来予知のような）
        bool draw = false;

        for (int i = 0; i < segmentCount; i++)
        {
            float startTime = timeStep * i;//セグメントを開始する時間(ここで言う時間は少しややこしい)
            float endTime = startTime + timeStep;//セグメントが終わるする時間

            // LineRenderer を描画
            SetLineRendererPosition(i, startTime, endTime, !draw);

            /*if (!draw)//trajectoryが描画中じゃない時だけ描画する
            {
                hitTime = GetArcHitTime(startTime, endTime);
                if (hitTime != float.MaxValue)
                {
                    Vector3 hitPosition = GetArcPositionAtTime(hitTime);
                    ShowPointer(hitPosition); // 必要に応じてポインターを表示
                    draw = true;
                }
            }*/

            //yield return new WaitForSeconds(trajectorySpeed * timeStep);//セグメント毎の描画する時間の範囲（未来予知のような）
        }
        /*
        if (hitTime != float.MaxValue)
        {
            Vector3 hitPosition = GetArcPositionAtTime(hitTime);
             ShowPointer(hitPosition); // 必要に応じてポインターを表示
        }*/

        drawArcAllTime = true;
    }

    private void ShowPointer(Vector3 position, Vector2 normal)
    {
        
        if (normal == Vector2.zero || !isShowingPointer) return; // 法線がゼロベクトルの場合は処理をスキップ

        float snappedX = Mathf.Floor(position.x+(normal.x*0.05f)) + 0.5f;
        float snappedY = Mathf.Floor(position.y-(normal.y*0.05f)) + 0.5f;

        // 法線から角度を計算してポインターを回転
        float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg;
        pointerObject.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        Vector2 gap = Vector2.zero;
        
        if(normal.y == -1)
        {
            gap.y = -1;
        }
        if(normal.y>0.3 || normal.y<-0.3)//1でもいいけど誤差を許さないため
        {
            gap.y += 1;
        }
        // ポインターの位置を設定
        pointerObject.transform.position = new Vector2(snappedX , snappedY) + gap;
        pointerObject.SetActive(true);
    }

    private Vector3 GetArcPositionAtTime(float time)
    {
        initialVelocity = pAS.selectedPlantData.actualVelocity;
        return (arcStartPosition + ((initialVelocity * time) + (0.5f * time * time) * Physics.gravity));//軌跡のセグメント毎の計算はここ
    }

    private void SetLineRendererPosition(int index, float startTime, float endTime, bool draw = true)
    {
    arcStartPosition = transform.position;

    Vector3 startPosition = GetArcPositionAtTime(startTime);
    Vector3 endPosition = GetArcPositionAtTime(endTime);

    lineRenderers[index].SetPosition(0, startPosition);
    lineRenderers[index].SetPosition(1, endPosition);

    lineRendererPositions1[index] = startPosition;
    lineRendererPositions2[index] = endPosition;
    basePlayerPositions[index] = transform.position;

    lineRenderers[index].enabled = draw;
    }

    private void CreateLineRendererObjects()
    {
        arcObjectsParent = new GameObject("ArcObject");
        arcObjectsParent.transform.SetParent(transform);
        lineRenderers = new LineRenderer[segmentCount];
        basePlayerPositions = new Vector2[segmentCount];
        lineRendererPositions1 = new Vector2[segmentCount];
        lineRendererPositions2 = new Vector2[segmentCount];
        for (int i = 0; i < segmentCount; i++)
        {
            GameObject newObject = new GameObject("LineRenderer_" + i);
            newObject.transform.SetParent(arcObjectsParent.transform);
            lineRenderers[i] = newObject.AddComponent<LineRenderer>();//ここから
            lineRenderers[i].receiveShadows = false;
            lineRenderers[i].reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            lineRenderers[i].lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
            lineRenderers[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineRenderers[i].material = arcMaterial;
            lineRenderers[i].startWidth = arcWidth;
            lineRenderers[i].endWidth = arcWidth;
            lineRenderers[i].numCapVertices = 5;
            lineRenderers[i].enabled = false;//ここからここまでlineRendererの初期化
        }
    }

    private float GetArcHitTime(float startTime, float endTime, out Vector2 hitNormal)
    {
        Vector3 startPosition = GetArcPositionAtTime(startTime);
        Vector3 endPosition = GetArcPositionAtTime(endTime);

        RaycastHit2D hitInfo = Physics2D.Linecast((Vector2)startPosition, (Vector2)endPosition, collisionLayer);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("plant"))
            {
                isShowingPointer = false;
                hitNormal = hitInfo.normal;
                if(pointerObject != null)pointerObject.SetActive(false);
                return startTime + (endTime - startTime) * (hitInfo.distance / Vector3.Distance(startPosition, endPosition));
            }
            if(pointerObject != null)pointerObject.SetActive(true);
            isShowingPointer = true;
            float distance = Vector3.Distance(startPosition, endPosition);
            hitNormal = hitInfo.normal;
            return startTime + (endTime - startTime) * (hitInfo.distance / distance);
        }
        hitNormal = Vector2.zero;
        return float.MaxValue;
    }
}