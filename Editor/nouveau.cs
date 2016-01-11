using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

class Nouveau : EditorWindow
{
    [MenuItem("Assets/Create/Scriptable Object...")]
    static void FromMenuItem()
    {
        var sc = typeof(ScriptableObject);
        
        List<Type> soTypes = new List<Type>();
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            var name = assembly.GetName().Name;
            if (!(name.StartsWith("Assembly-") == true && name.Contains("-Editor") == false))
                continue;
            
            var types = assembly.GetTypes().Where(t => sc.IsAssignableFrom(t));
            foreach(var t in types)
            {
                if (t == typeof(Nouveau))
                    continue;
                    
               soTypes.Add(t);
            }
        }
     
        var window = (Nouveau) CreateInstance(typeof(Nouveau));
        window.titleContent = new GUIContent("New ScriptableObject...");
        window.types = soTypes;
        window.minSize = new Vector2(250, 50);
        window.maxSize = new Vector2(250, 50);
        window.ShowUtility();
    }
    
    protected List<Type> types
    {
        get
        {
            return mTypes;
        }
        set
        {
            mTypes = value;
            mTypesStr = new String[mTypes.Count];
            for (int i=0;i < mTypes.Count;i++)
            {
                mTypesStr[i] = mTypes[i].Name;
            }
        }
    }
    
    List<Type> mTypes = new List<Type>();
    string[] mTypesStr;
    int mSelected;
    
    void OnGUI()
    {
        mSelected = EditorGUILayout.Popup(mSelected, mTypesStr);
        
        if (GUILayout.Button("Create") && mTypes.Count > 0 && mSelected >= 0 && mSelected < mTypes.Count)
        {
            var type = mTypes[mSelected];
            var asset = CreateInstance(type);
            var path = EditorUtility.SaveFilePanel(
					"Save " + type.ToString(),
					Application.dataPath,
					 type.ToString() + ".asset",
					"asset");
                    
            if (path.StartsWith(Application.dataPath) == false)
            {
                EditorUtility.DisplayDialog("Creation Error", "Path must be within the Assets folder", "Okay");
                return;
            }
            
            path = path.Remove(0, Application.dataPath.Length + 1);
            
            asset.name = path.Replace(".asset", String.Empty);
		    path  = AssetDatabase.GenerateUniqueAssetPath("assets/" + path);
        
            Debug.Log(asset);
            Debug.Log(path);
            Debug.Log(Application.dataPath);
                    
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();   
        }
    }  
}
