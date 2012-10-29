using UnityEngine;
using System.Collections;

public class CaravanPostFXManager : MonoBehaviour {

	private Material median_filter_material;
	private Material combine_map_material;
	private Material heat_distortion_effect_material;
	private Material color_gradeing_material;
	private Material bloom_blur_material;
	private Material bloom_combine_material;
	
	private RenderTexture filter_buffer;
	private RenderTexture bloom_buffer;
	
	public int median_filter_iterations = 4;
	public float heat_distortion_contrast = 50f;
	public float heat_distortion_brightness = -48.8f;
	public float bloom_brightness = -0.33f;
	public float bloom_conrast = 2.43f;
	public float bloom_intesity = 0.35f;
	public float bloom_spread_per_iteration = 310;
	private int bloom_texture_splitter = 4;
	public int bloom_iterations = 8;

	
	protected void OnEnable() {
		camera.depthTextureMode |= DepthTextureMode.Depth;		
	}
	
	protected void Start()
	{
		median_filter_material = (Material)Resources.Load("materials/postFX/median_filter");
		combine_map_material = (Material)Resources.Load("materials/postFX/display_map");
		heat_distortion_effect_material = (Material)Resources.Load("materials/postFX/heat_distortion");
		color_gradeing_material = (Material)Resources.Load("materials/postFX/color_gradeing");
		bloom_blur_material = (Material)Resources.Load("materials/postFX/color_distortion_blur");
		bloom_combine_material = (Material)Resources.Load("materials/postFX/color_distortion_bloom");
		
		//set exposure controll
		Shader.SetGlobalFloat("_exposure_control", 1);
		
		// Disable if we don't support image effects
		if (!SystemInfo.supportsImageEffects) {
			enabled = false;
			return;
		}
	}

	void OnRenderImage (RenderTexture source, RenderTexture destination) {	
		// initialize filter_buffer
		if (filter_buffer == null || filter_buffer.width != source.width || filter_buffer.height != source.height)
		{
			DestroyImmediate(filter_buffer);
			filter_buffer = new RenderTexture(source.width, source.height, 0);
			filter_buffer.hideFlags = HideFlags.HideAndDontSave;
			Graphics.Blit( source, filter_buffer );
		}
		
		//apply filters
		median_filter(source);
		combine_map(filter_buffer);
		heat_distortion(filter_buffer);
		color_gradeing(filter_buffer);
		bloom(filter_buffer);

		//pass to screen
		Graphics.Blit(filter_buffer, destination);
	}
	
	void median_filter (RenderTexture source) {	
		median_filter_material.SetTexture("_source", source);
		Graphics.Blit(source,filter_buffer, median_filter_material);
		
		for (int i = 0; i< median_filter_iterations; i++){
			median_filter_material.SetTexture("_source", filter_buffer);
			Graphics.Blit(filter_buffer, filter_buffer, median_filter_material);
		}
	}
	
	void combine_map (RenderTexture source) {	
		combine_map_material.SetTexture("_source", source);
		Graphics.Blit(source,filter_buffer, combine_map_material);
	}
	
	void heat_distortion (RenderTexture source) {	
		heat_distortion_effect_material.SetFloat("_contrast",heat_distortion_contrast);
		heat_distortion_effect_material.SetFloat("_brightness",heat_distortion_brightness);
		heat_distortion_effect_material.SetTexture("_source", source);
		Graphics.Blit(source,filter_buffer, heat_distortion_effect_material);
	}
	
	void color_gradeing (RenderTexture source) {	
		color_gradeing_material.SetTexture("_source", source);
		Graphics.Blit(source,filter_buffer, color_gradeing_material);
	}
	
	void bloom(RenderTexture source) {	
		// initialize motion_buffer
		if (bloom_buffer == null || bloom_buffer.width != source.width || bloom_buffer.height != source.height)
		{
			DestroyImmediate(bloom_buffer);
			bloom_buffer = new RenderTexture(source.width/bloom_texture_splitter, source.height/bloom_texture_splitter, 0);
			bloom_buffer.hideFlags = HideFlags.HideAndDontSave;
			Graphics.Blit( source, bloom_buffer );
		}
					

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
		Shader.SetGlobalTexture("_last_frame", bloom_buffer);

		Graphics.Blit(source, filter_buffer, bloom_combine_material);
	}
	
}	