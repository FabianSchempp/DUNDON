using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]

public class heat_distortion: MonoBehaviour
{	
	
	public int iterations = 3;
	public float blurSpread = 0.6f;
	
	private Material heat_distortion_effect_material;
	public float contrast = 50f;
	public float brightness = -48.8f;

	protected void OnEnable() {
		camera.depthTextureMode |= DepthTextureMode.Depth;		
	}
	
	protected void Start()
	{
		heat_distortion_effect_material = (Material)Resources.Load("materials/heat_distortion");
		// Disable if we don't support image effects
		if (!SystemInfo.supportsImageEffects) {
			enabled = false;
			return;
		}
		// Disable if the shader can't run on the users graphics card
		if (!heat_distortion_effect_material.shader.isSupported) {
			enabled = false;
			return;
		}
	}
	
	// Called by the camera to apply the image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {	
		heat_distortion_effect_material.SetFloat("_contrast",contrast);
		heat_distortion_effect_material.SetFloat("_brightness",brightness);
		heat_distortion_effect_material.SetTexture("_source", source);
		Graphics.Blit(source,destination, heat_distortion_effect_material);
	}
}	