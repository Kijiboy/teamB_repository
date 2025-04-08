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
//TODO: 銃で撃たれた際にor地面に当たった瞬間に種から成長する関数を追加右下の