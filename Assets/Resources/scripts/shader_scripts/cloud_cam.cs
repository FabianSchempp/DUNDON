using UnityEngine;
using System.Collections;

public class cloud_cam : MonoBehaviour {
	
 	private RenderTexture mask_pass;
	public Shader mask_shader;
	
	void Start () {
		//create a new Render Texture for for the cloud_masks, then overrides the shaders and overrides the color on the override shader for all clouds
		mask_pass = new RenderTexture(Screen.width,Screen.height,16);
		mask_pass.name = "mask_pass";
		camera.targetTexture = mask_pass;
		camera.SetReplacementShader(mask_shader,"");
		//Messenger.Broadcast("set_render_masks");
	}
	
//	void OnPreCull () {	
//		//create a new Render Texture for for the cloud_masks, then overrides the shaders and overrides the color on the override shader for all clouds
//		camera.targetTexture = mask_pass;
//		camera.SetReplacementShader(mask_shader,"");
//		Messenger.Broadcast("set_render_masks");
//
//	}
}
