using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class movePlantAim : MonoBehaviour
{
    [SerializeField] plantAdministerSystem pAS;
    [SerializeField] player_control player_Control;
    GameObject selectedPlant;
    plantDataClass plantData;

    seedScript seedScript;
   public void selectedPlantModified()
{
    selectedPlant = pAS.selectedPlant; // ← 先に代入！
    seedScript = selectedPlant.GetComponent<seedScript>();
    plantData = seedScript.plantData;
}

    void Start()
    {
        selectedPlantModified();
    }

    void FixedUpdate()
    {
        
        plantData = seedScript.plantData;
        
        setPlantRightOrLeft(player_Control.isFacingRight);

        float velocityStep = 3f;
        if (Input.GetKey(KeyCode.I))
        {
            if((Mathf.Atan2(plantData.actualVelocity.y, plantData.actualVelocity.x) * Mathf.Rad2Deg >= 95) ||( Mathf.Atan2(plantData.actualVelocity.y, plantData.actualVelocity.x) * Mathf.Rad2Deg <= 85))
            {
                if(player_Control.isFacingRight)
                {
                    AdjustVelocity(velocityStep);
                }
                else
                {
                    AdjustVelocity(-velocityStep);
                }
            }
        }

        if (Input.GetKey(KeyCode.K))
        {
            if((Mathf.Atan2(plantData.actualVelocity.y, plantData.actualVelocity.x) * Mathf.Rad2Deg <= -95) || (Mathf.Atan2(plantData.actualVelocity.y, plantData.actualVelocity.x) * Mathf.Rad2Deg >= -85))
            {
                if(player_Control.isFacingRight)
                {
                    AdjustVelocity(-velocityStep);
                }
                else
                {
                    AdjustVelocity(velocityStep);
                }
            }
        }
        
        pAS.selectedPlantData.actualVelocity = plantData.actualVelocity;

    }

     private void AdjustVelocity(float angleStep)
    {
        // 現在の速度ベクトルの角度を計算
        float currentAngle = Mathf.Atan2(plantData.actualVelocity.y, plantData.actualVelocity.x) * Mathf.Rad2Deg;
        // 角度を調整（半円を描くように）
        currentAngle += angleStep;

        // 新しい角度に基づいて速度ベクトルを計算

        float speed = plantData.baseVelocity.magnitude;
        plantData.actualVelocity.x = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * speed;
        plantData.actualVelocity.y = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * speed;
    }

    public void setPlantRightOrLeft(bool isFacingRight)
    {
        if(isFacingRight)
        {
            plantData.actualVelocity = new Vector3(Mathf.Abs(plantData.actualVelocity.x), plantData.actualVelocity.y, 0);
        }
        else
        {
            plantData.actualVelocity = new Vector3(-Mathf.Abs(plantData.actualVelocity.x), plantData.actualVelocity.y, 0);
        }
    }
}
