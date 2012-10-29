using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]

public class color_distortion: MonoBehaviour
{	
	private Material effect_material;
	private RenderTexture motion_buffer;
	protected void OnEnable() {
		camera.depthTextureMode |= DepthTextureMode.Depth;		
	}
	
	protected void Start()
	{
		effect_material = (Material)Resources.Load("materials/color_distortion");
		// Disable if we don't support image effects
		if (!SystemInfo.supportsImageEffects) {
			enabled = false;
			return;
		}
		// Disable if the shader can't run on the users graphics card
		if (!effect_material.shader.isSupported) {
			enabled = false;
			return;
		}
		
		//shader needs global Variable set to work
		Shader.SetGlobalFloat("_current_health",1);
	}
//	void find_average_value(RenderTexture t){
//		RenderTexture average_value = new RenderTexture(t.width/10, t.height/10,1);
//		Graphics.Blit(t, average_value);
//		Debug.Log("average_brightness_value: ");
//	}
//	
	// Called by the camera to apply the image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {	
		// initialize motion_buffer
		if (motion_buffer == null || motion_buffer.width != source.width || motion_buffer.height != source.height)
		{
			DestroyImmediate(motion_buffer);
			motion_buffer = new RenderTexture(source.width, source.height, 0);
			motion_buffer.hideFlags = HideFlags.HideAndDontSave;
			Graphics.Blit( source, motion_buffer );
			Shader.SetGlobalTexture("_last_frame", motion_buffer);
		}

		effect_material.SetTexture("_last_frame", motion_buffer);
		effect_material.SetTexture("_source", source);
		Graphics.Blit(source,motion_buffer, effect_material);
		Graphics.Blit(motion_buffer, destination);
	}
}	