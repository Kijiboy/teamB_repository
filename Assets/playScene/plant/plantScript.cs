using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class plantDataClass
{
    public GameObject seedPrehub;
    public GameObject grewPrehub;
    public float speed;//投げた際のスピード
    public float mass;//落下スピード
}

public class plantScript : MonoBehaviour
{
    public plantDataClass plantData;
}
