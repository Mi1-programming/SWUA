using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Building))]
public class BuildingEditor : Editor
{
    private void AddCoord(SerializedProperty target, Vector3 newCoord)
    {
        target.InsertArrayElementAtIndex(0);
        SerializedProperty coord = target.GetArrayElementAtIndex(0);
        coord.serializedObject.ApplyModifiedProperties();
        coord.vector3Value = newCoord;
        coord.serializedObject.ApplyModifiedProperties();
    }
    private void CopyTemplate()
    {
        SerializedProperty gen = serializedObject.FindProperty("gen");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        if (GUILayout.Button("Copy Template"))
        {
            SerializedProperty temps = serializedObject.FindProperty("templates");
            temps.InsertArrayElementAtIndex(0);
            SerializedProperty tiles = temps.GetArrayElementAtIndex(0).FindPropertyRelative("tiles");

            AddCoord(tiles, Vector3.one);
            AddCoord(tiles, Vector3.zero);
            AddCoord(tiles, Vector3.forward);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
