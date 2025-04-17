using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(seedScript))] // ネットのコードをほとんどそのまま使用
public class plantCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // **元のインスペクターを表示**
        DrawDefaultInspector();

        // **ターゲットのオブジェクトを取得**
        seedScript plantSC = (seedScript)target;

        // **カスタムボタンを追加**
        if (GUILayout.Button("Grow plant"))
        {
            // **ボタンが押されたときの処理**
            plantSC.land(270);
        }
    }
}