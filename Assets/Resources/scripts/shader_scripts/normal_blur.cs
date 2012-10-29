using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]

public class normal_blur : MonoBehaviour {
	
//	public static List<GameObject> custom_normal_map_objects = new List<GameObject>();
	public Material blur_material;
	public int iterations = 16;
	private RenderTexture bloom_buffer;

	protected void OnEnable() {
		camera.depthTextureMode = DepthTextureMode.DepthNormals;		
	}
	
	protected void Start(){
		blur_material = (Material)Resources.Load("materials/normal_blur",typeof(Material));
//		// Disable if we don't support image effects
//		if (!SystemInfo.supportsImageEffects) {
//			enabled = false;
//			return;
//		}
//		// Disable if the shader can't run on the users graphics card
//		if (!blur_material.shader.isSupported) {
//			enabled = false;
//			return;
//		}
		
	}

//	void OnPreRender () {
//	    foreach(GameObject g in custom_normal_map_objects){
//			g.SendMessage("set_custom_normalmap",SendMessageOptions.DontRequireReceiver);
//		}
//	}
	// Called by the camera to apply the image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {	
		// initialize motion_buffer
		if (bloom_buffer == null || bloom_buffer.width != source.width || bloom_buffer.height != source.height)
		{
			DestroyImmediate(bloom_buffer);
			bloom_buffer = new RenderTexture(source.width, source.height, 0);
			bloom_buffer.hideFlags = HideFlags.HideAndDontSave;
			Graphics.Blit( source, bloom_buffer );
			Shader.SetGlobalTexture("_last_frame", bloom_buffer);
		}

		blur_material.SetTexture("_source", source);
		
		Graphics.Blit(source,bloom_buffer, blur_material);
		
		for (int i = 0; i< iterations; i++){
			blur_material.SetTexture("_source", bloom_buffer);
			Graphics.Blit(bloom_buffer, bloom_buffer, blur_material);
		}
		
		Graphics.Blit(bloom_buffer, destination);
	}
}	