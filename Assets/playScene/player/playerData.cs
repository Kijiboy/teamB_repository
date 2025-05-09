using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class player_Physical_Ability
{
    public float baseRunSpeed;
    public float jumpStrength;
    public float speedMultiplierInAir;

    public HPUI hpUI;
    public int maxHP;
    [SerializeField]private int _hp;
    public int hp
    {
        get { return _hp; }
        set
        {
            if (value > maxHP)
            {
                hpUI.hpUpdated(maxHP, maxHP);
                _hp = maxHP;
            }
            else
            if (value < 0)
            {  hpUI.hpUpdated(0, maxHP);
                _hp = 0;
            }
            else
            {
                hpUI.hpUpdated(value, maxHP);
                _hp = value;
            }
        }
    }

}

[System.Serializable]
public class player_Obtained_Plants
{
    public GameObject[] seeds;
}

public class playerData : MonoBehaviour
{
    [Header("身体能力")]
    public player_Physical_Ability pPA_D;//playerPhysicalAbility_Deta
    [Header("習得した植物")]
    public player_Obtained_Plants pOp_D;//player_Obtained_Plants_Deta
}
