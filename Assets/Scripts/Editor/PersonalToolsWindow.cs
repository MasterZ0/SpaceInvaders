using UnityEditor;
using UnityEngine;
using System;

public class PersonalToolsWindow : EditorWindow {


    [MenuItem("Window/Personal Tools")]
    public static void ShowWindow() {
        GetWindow<PersonalToolsWindow>("Personal Tools");
    }
    public void OnGUI() {
        if (GUILayout.Button("Round Transform Position")) {
            RoundTransformPosition();
        }
        if (GUILayout.Button("Round Local Scale")) {
            RoundScale();
        }
    }

    void RoundTransformPosition() {
        foreach (GameObject obj in Selection.gameObjects) {
            Undo.RecordObject(obj, "rounded");

            Vector3 pos = obj.transform.localPosition;
            pos.x = (float)Math.Round(pos.x * 2, MidpointRounding.AwayFromZero) / 2;
            pos.y = (float)Math.Round(pos.y * 2, MidpointRounding.AwayFromZero) / 2;
            pos.z = (float)Math.Round(pos.z * 2, MidpointRounding.AwayFromZero) / 2;
            obj.transform.localPosition = pos;
        }
    }
    void RoundScale() {
        foreach (GameObject obj in Selection.gameObjects) {
            Undo.RecordObject(obj, "rounded");

            Vector3 scale = obj.transform.localScale;
            scale.x = (float)Math.Round(scale.x * 2, MidpointRounding.AwayFromZero) / 2;
            scale.y = (float)Math.Round(scale.y * 2, MidpointRounding.AwayFromZero) / 2;
            scale.z = (float)Math.Round(scale.z * 2, MidpointRounding.AwayFromZero) / 2;
            obj.transform.localScale = scale;
        }
    }
}
