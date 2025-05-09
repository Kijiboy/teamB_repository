using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class emitBarrage : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int count = 5; // Duration for which the barrage will be emitted

    void Start()
    {
        StartCoroutine(emitBarrageCoroutine(0.5f, count)); // Example: emit 10 bullets with 0.5 second interval
    }

    IEnumerator emitBarrageCoroutine(float interval, int barrageCount)
    {
        for (int i = 0; i < barrageCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10); // Adjust the velocity as needed
            yield return new WaitForSeconds(interval);
        }
    }
}
