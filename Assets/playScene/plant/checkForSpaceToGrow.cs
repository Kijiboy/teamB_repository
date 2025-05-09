using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkForSpaceToGrow : MonoBehaviour
{
    public bool haveSpaceToGrow()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(
        origin: transform.position + transform.root.transform.up, 
        size: new Vector2(0.9f, 0.9f), 
        angle: 0f, 
        direction: Vector2.zero,
        distance: 0.05f
        );

        // 全ヒットオブジェクトを確認
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject == gameObject)
            {
                continue;
            }

            if (hit.collider.CompareTag("plantGround") || hit.collider.CompareTag("Plant") || hit.collider.CompareTag("Ground"))
            {
                return false;
            }
        }
        return true;
    }

        void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.root.transform.up, new Vector2(0.9f, 0.9f));
    }
}
