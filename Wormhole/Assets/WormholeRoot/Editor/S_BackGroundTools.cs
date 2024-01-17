using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;

public class S_BackGroundTools : EditorWindow
{
    S_BackgroundData data;
    Color tileColor = Color.white;
    Color gridColor=Color.black;

    string fileName = "";

    Material backgroundMaterial;
    bool error;

    private void OnEnable()
    {
        backgroundMaterial = GameObject.FindGameObjectWithTag("Background").GetComponent<MeshRenderer>().material;
    }


    [MenuItem("Tools/Level/Background Setter")]
    public static void ShowWindow()
    {
        
        S_BackGroundTools window = GetWindow<S_BackGroundTools>("Background Setter".ToUpper());
        
        window.Show();
        
    }

    private void OnGUI()
    {
        

        EditorGUILayout.Space(20);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Select colors to edit background".ToUpper(), EditorStyles.boldLabel);
        
        if (GUILayout.Button("Reset"))
        {
            Reset();
        }
        
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(15);
        tileColor = EditorGUILayout.ColorField("Tile Color",tileColor);
        gridColor = EditorGUILayout.ColorField("Grid Color", gridColor);
        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        data = EditorGUILayout.ObjectField("Choose a file", data, typeof(S_BackgroundData), false) as S_BackgroundData;
        if (GUILayout.Button("Load"))
        {
            Load();
        }
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        fileName = EditorGUILayout.TextField("File Name", fileName);
        if (GUILayout.Button("Save"))
        {
            Save();
        }
        EditorGUILayout.EndHorizontal();
        if(error)
            EditorGUILayout.TextField("Error :  name not mentions");

        EditorGUILayout.Space(10);
        SetBackground();
    }

    private void SetBackground()
    {
        backgroundMaterial.SetColor("_TileColor", tileColor);
        backgroundMaterial.SetColor("_BorderColor", gridColor);
    }


    private void Reset()
    {
        tileColor = Color.white;
        gridColor = Color.black;

        
        error = false;
        data = null;

    }

    private void Load()
    {
        tileColor = data.tileColor;
        gridColor = data.borderColor;

        SetBackground();
    }
    private void Save() 
    {
        var obj = ScriptableObject.CreateInstance<S_BackgroundData>();
        if (fileName != "")
        {
            error = false;
            //AssetDatabase.ExportPackage(obj, "Assets/WormholeRoot/Background/" + "SO_" + fileName,)
            AssetDatabase.CreateAsset(obj, "Assets/WormholeRoot/Background/" + "SO_" + fileName+".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Selection.activeObject = obj;
            obj.tileColor = tileColor;
            obj.borderColor = gridColor;
        }
        else
            error = true;
    }
}
