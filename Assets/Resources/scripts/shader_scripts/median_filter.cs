using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]

public class median_filter: MonoBehaviour
{	
	private Material effect_material;
	public int iterations = 4;
	private RenderTexture filter_buffer;

	protected void OnEnable() {
		camera.depthTextureMode |= DepthTextureMode.Depth;		
	}
	
	protected void Start()
	{
		effect_material = (Material)Resources.Load("materials/median_filter");
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
		
//		//apply filter effect for iteration times
		effect_material.SetTexture("_source", source);
		Graphics.Blit(source,filter_buffer, effect_material);
		
		for (int i = 0; i< iterations; i++){
			effect_material.SetTexture("_source", filter_buffer);
			Graphics.Blit(filter_buffer, filter_buffer, effect_material);
		}
//		
		Graphics.Blit(filter_buffer, destination);
//		Graphics.Blit(source,destination, effect_material);
	}
	
}	