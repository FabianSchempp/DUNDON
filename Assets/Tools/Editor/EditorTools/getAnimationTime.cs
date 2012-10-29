using UnityEngine;
using System.Collections;
using UnityEditor;

public class getAnimationTime :ScriptableWizard {
   public AnimationClip animationToTest;
	
    void OnWizardUpdate () {
        helpString = "animation which time you want";
        isValid = (animationToTest != null);
    }
    
    void OnWizardCreate () {
		Debug.Log("Animation " + animationToTest.name + " is " + animationToTest.length + " long");
    }
    
    [MenuItem("UniworldTools/get animation time")]
    public static void replaceObjects () {
        ScriptableWizard.DisplayWizard<getAnimationTime>("get", "get!");
    }
}