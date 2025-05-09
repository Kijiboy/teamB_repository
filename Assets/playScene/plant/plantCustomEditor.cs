using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(plantScript))] // ネットのコードをほとんどそのまま使用
public class plantCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // **元のインスペクターを表示**
        DrawDefaultInspector();

        // **ターゲットのオブジェクトを取得**
        plantScript plantSC = (plantScript)target;

        // **カスタムボタンを追加**
        if (GUILayout.Button("Grow plant"))
        {
            // **ボタンが押されたときの処理**
            plantSC.gotShot();
        }
    }
}