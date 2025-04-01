using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class player_Physical_Ability
{
    public float runSpeed;
    public float jumpStrength;
}

[System.Serializable]
public class player_Obtained_Plants
{
    public GameObject[] seeds;
}

public class playerDeta : MonoBehaviour
{
    public player_Physical_Ability pPA_D;//playerPhysicalAbility_Deta
    public player_Obtained_Plants pOp_D;//player_Obtained_Plants_Deta
}
