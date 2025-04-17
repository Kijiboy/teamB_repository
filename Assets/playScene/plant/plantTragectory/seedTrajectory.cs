using System.Collections;
using System.Collections.Generic;
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
    private shootsOutPlant sOP;
    private Vector3 initialVelocity;
    private Vector3 arcStartPosition;

    [SerializeField] float trajectorySpeed;

    [SerializeField] LayerMask collisionLayer; // インスペクターで設定可能

    [SerializeField] GameObject player;
    [SerializeField] player_control player_Control;
    Rigidbody2D rb;

    [SerializeField] GameObject pointerPrefab;
    GameObject pointerObject;

    GameObject arcObjectsParent;

    Vector2[] basePlayerPositions;

    Vector2[] lineRendererPositions1;
    Vector2[] lineRendererPositions2;
    

    void Start()
    {
        pointerObject = Instantiate(pointerPrefab, Vector3.zero, Quaternion.identity);

        CreateLineRendererObjects();//ラインレンダラーのオブジェクトを必要な数を作成してるっぽい
        sOP = gameObject.GetComponent<shootsOutPlant>();
        rb = GetComponent<Rigidbody2D>();
        drawArc = true;
        StartCoroutine(DrawTrajectoryGradually());
    }


    void FixedUpdate()
{
    if (!drawArc) return; // drawArc が false の場合は処理をスキップ

    initialVelocity = player_Control.isFacingRight
        ? sOP.plantData.baseVelocity
        : new Vector3(-sOP.plantData.baseVelocity.x, sOP.plantData.baseVelocity.y, 0);

    float timeStep = predictionTime / segmentCount;
    bool draw = false;
    float hitTime = float.MaxValue;

    for (int i = 0; i < lineRenderers.Length; i++)
    {
        float startTime = timeStep * i;
        float endTime = startTime + timeStep;

        SetLineRendererPosition(i, startTime, endTime, !draw);

        if (!draw)
        {
            hitTime = GetArcHitTime(startTime, endTime);
            if (hitTime != float.MaxValue)
            {
                draw = true;
            }
        }
    }

    if (hitTime != float.MaxValue)
    {
        Vector3 hitPosition = GetArcPositionAtTime(hitTime);
        ShowPointer(hitPosition);
    }
    else
    {
        pointerObject.SetActive(false);
    }
}
                
    private IEnumerator DrawTrajectoryGradually()
    {
        initialVelocity = sOP.plantData.baseVelocity;
        arcStartPosition = player.transform.position;

        float timeStep = predictionTime / segmentCount;//一セグメント毎の描画する時間の範囲（未来予知のような）
        bool draw = false;
        float hitTime = float.MaxValue;

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

            yield return new WaitForSeconds(trajectorySpeed * timeStep);//セグメント毎の描画する時間の範囲（未来予知のような）
        }
        /*
        if (hitTime != float.MaxValue)
        {
            Vector3 hitPosition = GetArcPositionAtTime(hitTime);
             ShowPointer(hitPosition); // 必要に応じてポインターを表示
        }*/

        drawArcAllTime = true;
    }

    private void ShowPointer(Vector3 position)
    {
        pointerObject.transform.position = position;
        pointerObject.SetActive(true);
    }

    private Vector3 GetArcPositionAtTime(float time)
    {
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

    private float GetArcHitTime(float startTime, float endTime)
    {
        Vector3 startPosition = GetArcPositionAtTime(startTime);
        Vector3 endPosition = GetArcPositionAtTime(endTime);

        RaycastHit2D hitInfo = Physics2D.Linecast((Vector2)startPosition, (Vector2)endPosition, collisionLayer);
        if (hitInfo.collider != null)
        {
            float distance = Vector3.Distance(startPosition, endPosition);
            return startTime + (endTime - startTime) * (hitInfo.distance / distance);
        }
        return float.MaxValue;
    }
}