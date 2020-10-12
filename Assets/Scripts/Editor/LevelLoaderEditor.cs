using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(LevelLoader))]
[CanEditMultipleObjects]
public class LevelLoaderEditor : Editor
{
    private ReorderableList list1;
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        this.serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Levels", EditorStyles.boldLabel);
        ReorderableListUtility.DoLayoutListWithFoldout(this.list1);


        this.serializedObject.ApplyModifiedProperties();
    }

    private void OnEnable()
    {
        var property = this.serializedObject.FindProperty("levels");       
        this.list1 = ReorderableListUtility.CreateAutoLayout(
            property);
            //,new string[] { "Element", "Level Name"},
            //new float?[] { 100, 70 });
    }
    
}
