using UnityEngine;
using System.Collections;
using UnityEditor;

public class RenderCubemapWizard :ScriptableWizard {
   public Transform renderFromPosition;
   public Cubemap cubemap;
   private JointLimits _jointLimits = new JointLimits();
	
    void OnWizardUpdate () {
        helpString = "give me a Transform and a Cubemap!";
        isValid = (renderFromPosition != null) && (cubemap != null);
    }
    
    void OnWizardCreate () {
        // create temporary camera for rendering
        GameObject go = new GameObject("CubemapCamera",typeof(Camera));
        // place it on the object
        go.transform.position = renderFromPosition.position;
        go.transform.rotation = Quaternion.identity;

        // render into cubemap        
        go.camera.RenderToCubemap( cubemap );
        
        // destroy temporary camera
        DestroyImmediate(go);
    }
    
    [MenuItem("UniworldTools/Render into Cubemap")]
    public static void RenderCubemap () {
        ScriptableWizard.DisplayWizard<RenderCubemapWizard>("Render cubemap", "Render!");
    }
}