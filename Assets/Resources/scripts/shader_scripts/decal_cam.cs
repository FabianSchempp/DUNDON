using UnityEngine;
using System.Collections;

public class decal_cam : MonoBehaviour {
	
	public Material[] decal_materials;
 	private RenderTexture decal_pass;
	
	void Awake () {
		//create a new Render Texture for the decal pass and assign it to the Camera
		decal_pass = new RenderTexture(Screen.width,Screen.height,16);
		decal_pass.name = "decal_pass";
		camera.targetTexture = decal_pass;
		
		foreach (Material mat in decal_materials){
			mat.SetTexture("_normal_decal",decal_pass);
		}
	}
}
