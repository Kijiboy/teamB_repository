using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantTragectoryBoxSC : MonoBehaviour
{
    [SerializeField] Vector2 gap;
    public void receiceDirection(int dir)//左->0, 下->1, 右->2, 上->3 このスクリプトはこオブジェクトのコライダーに呼び出される
    {     
        Debug.Log("plantTragectoryBoxSC: " + dir);
    switch (dir)
    {
        case 0:
                transform.rotation = Quaternion.Euler(0, 0, 270);
                transform.position = new Vector2(-gap.x, 0);
                break;
            
        case 1:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.position = new Vector2(0, -gap.y);
                break;
            
        case 2:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                transform.position = new Vector2(gap.x, 0);
                break;
            
        case 3:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                transform.position = new Vector2(0, gap.y);
            break;
            
        default:
            Debug.LogError("Invalid direction");
            break;
        }
    }
}
