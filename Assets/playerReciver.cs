using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerReciver : MonoBehaviour
{
    [SerializeField] playerData playerD;

    public void gotDammage(int damage)
    {
        playerD.pPA_D.hp -= damage;
        if (playerD.pPA_D.hp <= 0)
        {
            playerD.pPA_D.hp = 0;
            // Handle player death here
        }
    }
    
    public void gotHeal(int healAmount)
    {
        playerD.pPA_D.hp += healAmount;
        if (playerD.pPA_D.hp > playerD.pPA_D.maxHP)
        {
            playerD.pPA_D.hp = playerD.pPA_D.maxHP;
        }
    }
}
