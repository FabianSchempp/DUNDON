using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]

public class color_distortion_bloom: MonoBehaviour
{	
	private Material bloom_blur_material;
	private Material bloom_combine_material;
	
//	public float blur_spread = 0.002f;
	public float bloom_brightness = -0.33f;
	public float bloom_conrast = 2.43f;
	public float bloom_intesity = 0.35f;
	public float bloom_spread_per_iteration = 310;
	private int bloom_texture_splitter = 4;
	
	public int bloom_iterations = 8;
	private RenderTexture bloom_buffer;
//	private RenderTexture aa_buffer;

	protected void OnEnable() {
		camera.depthTextureMode |= DepthTextureMode.Depth;		
	}
	
	protected void Start()
	{
		bloom_blur_material = (Material)Resources.Load("materials/color_distortion_blur");
		bloom_combine_material = (Material)Resources.Load("materials/color_distortion_bloom");
		
		// Disable if we don't support image effects
		if (!SystemInfo.supportsImageEffects) {
			enabled = false;
			return;
		}
		// Disable if the shader can't run on the users graphics card
		if (!bloom_blur_material.shader.isSupported) {
			enabled = false;
			return;
		}
		
		//set exposure controll
		Shader.SetGlobalFloat("_exposure_control", 1);
	}

	// Called by the camera to apply the image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {	
		// initialize motion_buffer
		if (bloom_buffer == null || bloom_buffer.width != source.width || bloom_buffer.height != source.height)
		{
			DestroyImmediate(bloom_buffer);
			bloom_buffer = new RenderTexture(source.width/bloom_texture_splitter, source.height/bloom_texture_splitter, 0);
			bloom_buffer.hideFlags = HideFlags.HideAndDontSave;
			Graphics.Blit( source, bloom_buffer );
			Shader.SetGlobalTexture("_last_frame", bloom_buffer);
		}
		
//		blur_material.SetFloat("_blur_spread", blur_spread);
		bloom_blur_material.SetTexture("_source", source);
		
		Graphics.Blit(source,bloom_buffer, bloom_blur_material);
		
		for (int i = 0; i< bloom_iterations; i++){
			bloom_blur_material.SetTexture("_source", bloom_buffer);
			bloom_blur_material.SetFloat("_blur_iteration", (i*bloom_spread_per_iteration) +1);
			Graphics.Blit(bloom_buffer, bloom_buffer, bloom_blur_material);
		}
		
		bloom_combine_material.SetFloat("_glow_contrast", bloom_conrast);
		bloom_combine_material.SetFloat("_glow_brightness", bloom_brightness);
		bloom_combine_material.SetFloat("_glow_intensity", bloom_intesity);
		bloom_combine_material.SetTexture("_source", source);
		bloom_combine_material.SetTexture("_blured_source", bloom_buffer);
		Shader.SetGlobalTexture("_blured_buffer", bloom_buffer);
		Graphics.Blit(source, destination, bloom_combine_material);
//		Graphics.Blit(bloom_buffer, destination);
	}
}	
