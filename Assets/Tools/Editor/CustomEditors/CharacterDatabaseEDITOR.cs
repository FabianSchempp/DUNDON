using System.Collections;
using UnityEditor;
using UnityEngine;
using System.Xml;
using System.Xml.XPath;
using System.Collections.Generic;

[CustomEditor (typeof(CharacterDatabase))]
public class CharacterDatabaseEDITOR : Editor {
	override public void OnInspectorGUI()
    {
		CharacterDatabase a = (CharacterDatabase)target;
		
		if(GUILayout.Button("get prefabs"))
		{
			getPrefabs(a);
		}
		
		EditorGUILayout.Separator();
		
		EditorGUILayout.ObjectField(a.arm_upper_left_database_root,typeof(GameObject));
		EditorGUILayout.ObjectField(a.arm_upper_right_database_root,typeof(GameObject));
		EditorGUILayout.ObjectField(a.arm_lower_left_database_root,typeof(GameObject));
		EditorGUILayout.ObjectField(a.arm_lower_right_database_root,typeof(GameObject));
		EditorGUILayout.ObjectField(a.shin_left_database_root,typeof(GameObject));
		EditorGUILayout.ObjectField(a.shin_right_database_root,typeof(GameObject));
		EditorGUILayout.ObjectField(a.shoulder_left_database_root,typeof(GameObject));
		EditorGUILayout.ObjectField(a.shoulder_right_database_root,typeof(GameObject));
		EditorGUILayout.ObjectField(a.thight_left_database_root,typeof(GameObject));
		EditorGUILayout.ObjectField(a.thight_right_database_root,typeof(GameObject));
		EditorGUILayout.ObjectField(a.sword_database_root,typeof(GameObject));
		EditorGUILayout.ObjectField(a.shield_database_root,typeof(GameObject));
		EditorGUILayout.ObjectField(a.head_database_root,typeof(GameObject));
		EditorGUILayout.ObjectField(a.helm_database_root,typeof(GameObject));
		EditorGUILayout.ObjectField(a.torso_database_root,typeof(GameObject));
		EditorGUILayout.ObjectField(a.stomage_database_root,typeof(GameObject));
	}
	
	void getPrefabs(CharacterDatabase a){
		Transform root = a.transform;
	
		a.arm_lower_left_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/arm_lower_left.blend",typeof(GameObject));
		a.arm_lower_right_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/arm_lower_right.blend",typeof(GameObject));
		a.arm_upper_left_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/arm_upper_left.blend",typeof(GameObject));
		a.arm_upper_right_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/arm_upper_right.blend",typeof(GameObject));
		a.thight_left_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/thight_left.blend",typeof(GameObject));
		a.thight_right_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/thight_right.blend",typeof(GameObject));
		a.shin_left_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/shin_left.blend",typeof(GameObject));
		a.shin_right_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/shin_right.blend",typeof(GameObject));
		
		a.shoulder_left_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/shoulder_left.blend",typeof(GameObject));
		a.shoulder_right_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/shoulder_right.blend",typeof(GameObject));
		
		a.sword_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/swords.blend",typeof(GameObject));
		a.shield_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/shields.blend",typeof(GameObject));
		a.torso_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/torso.blend",typeof(GameObject));
		a.head_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/heads.blend",typeof(GameObject));
		a.helm_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/helmets.blend",typeof(GameObject));
		a.stomage_database_root = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Resources/models/items/stomache.blend",typeof(GameObject));
		a.initialize();
	}

}