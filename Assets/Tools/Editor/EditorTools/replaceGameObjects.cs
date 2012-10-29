using UnityEngine;
using System.Collections;
using UnityEditor;

public class replaceGameObjects :ScriptableWizard {
   public GameObject objectToReplaceWith;
	
    void OnWizardUpdate () {
        helpString = "prefab to replace with!";
        isValid = (objectToReplaceWith != null);
    }
    
    void OnWizardCreate () {
		foreach(GameObject g in Selection.gameObjects){
			GameObject nG = (GameObject) PrefabUtility.InstantiatePrefab(objectToReplaceWith);
			nG.transform.position = g.transform.position;
			nG.transform.rotation = g.transform.rotation;
			nG.transform.parent = g.transform.parent;
			nG.transform.localScale = g.transform.localScale;

		}
		foreach(GameObject g in Selection.gameObjects){
			DestroyImmediate(g);
		}
    }
    
    [MenuItem("UniworldTools/replace Objects")]
    public static void replaceObjects () {
        ScriptableWizard.DisplayWizard<replaceGameObjects>("replace", "replace!");
    }
}