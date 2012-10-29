using System.Collections;
using UnityEditor;
using UnityEngine;
using System.Xml;
using System.Xml.XPath;
using System.Collections.Generic;

[CustomEditor (typeof(BCharacterAssambler))]
public class CharacterAssamblerEDITOR : Editor {
	override public void OnInspectorGUI()
    {
		BCharacterAssambler a = (BCharacterAssambler)target;
		if(GUILayout.Button("get slots"))
		{
			getSlots(a);
		}
		
		if(GUILayout.Button("assamble character"))
		{
			a.assambleRandomCharacter();
		}
		
		EditorGUILayout.ObjectField(a.arm_upper_left,typeof(GameObject));
		EditorGUILayout.ObjectField(a.arm_upper_right,typeof(GameObject));
		EditorGUILayout.ObjectField(a.arm_lower_left,typeof(GameObject));
		EditorGUILayout.ObjectField(a.arm_lower_right,typeof(GameObject));
		EditorGUILayout.ObjectField(a.shin_left,typeof(GameObject));
		EditorGUILayout.ObjectField(a.shin_right,typeof(GameObject));
		EditorGUILayout.ObjectField(a.shoulder_left,typeof(GameObject));
		EditorGUILayout.ObjectField(a.shoulder_right,typeof(GameObject));
		EditorGUILayout.ObjectField(a.thight_left,typeof(GameObject));
		EditorGUILayout.ObjectField(a.thight_right,typeof(GameObject));
		EditorGUILayout.ObjectField(a.sword,typeof(GameObject));
		EditorGUILayout.ObjectField(a.shield,typeof(GameObject));
		EditorGUILayout.ObjectField(a.head,typeof(GameObject));
		EditorGUILayout.ObjectField(a.torso,typeof(GameObject));
		EditorGUILayout.ObjectField(a.stomage,typeof(GameObject));
		
		
	}
	void getSlots(BCharacterAssambler a){
		Transform root = a.transform;
	
		Transform[] allChildren = root.GetComponentsInChildren<Transform>();
		
		foreach (Transform child in allChildren) {
				//look for arm_upper
					if (child.name.ToLower().StartsWith("arm_upper_left")) {
						a.arm_upper_left = child.gameObject.transform;
					}
					if (child.name.ToLower().StartsWith("arm_upper_right")) {
						a.arm_upper_right = child.gameObject.transform;
					}
					if (child.name.ToLower().StartsWith("arm_lower_left")) {
						a.arm_lower_left = child.gameObject.transform;
					}
					if (child.name.ToLower().StartsWith("arm_lower_right")) {
						a.arm_lower_right = child.gameObject.transform;
					}
					if (child.name.ToLower().StartsWith("thigth_left")) {
						a.thight_left = child.gameObject.transform;
					}
					if (child.name.ToLower().StartsWith("thigth_right")) {
						a.thight_right = child.gameObject.transform;
					}
					if (child.name.ToLower().StartsWith("shin_left")) {
						a.shin_left = child.gameObject.transform;
					}
					if (child.name.ToLower().StartsWith("shin_right")) {
						a.shin_right = child.gameObject.transform;
					}
			
					if (child.name.ToLower().StartsWith("shoulder_left")) {
						a.shoulder_left = child.gameObject.transform;
					}
					if (child.name.ToLower().StartsWith("shoulder_right")) {
						a.shoulder_right = child.gameObject.transform;
					}
			
					if (child.name.ToLower().StartsWith("sword")) {
						a.sword = child.gameObject.transform;
					}
			
					if (child.name.ToLower().StartsWith("shield")) {
						a.shield = child.gameObject.transform;
					}
			
					if (child.name.ToLower().StartsWith("torso")) {
						a.torso = child.gameObject.transform;
					}
			
					if (child.name.ToLower().StartsWith("head")) {
						a.head = child.gameObject.transform;
					}
			
					if (child.name.ToLower().StartsWith("stomage")) {
						a.stomage = child.gameObject.transform;
					}
			}
		}
}