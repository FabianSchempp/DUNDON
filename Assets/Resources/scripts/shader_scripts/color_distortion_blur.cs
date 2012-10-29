using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]

public class color_distortion_blur: MonoBehaviour
{	
	private Material effect_material;
	public int iterations = 8;
	private RenderTexture bloom_buffer;

//	private Vector2[] offsets = new Vector2[]{
//		new Vector2(
//	};
	
	protected void OnEnable() {
		camera.depthTextureMode |= DepthTextureMode.Depth;		
	}
	
	protected void Start()
	{
		effect_material = (Material)Resources.Load("materials/masked_blur");
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
//	void find_average_value(RenderTexture t){
//		RenderTexture average_value = new RenderTexture(t.width/10, t.height/10,1);
//		Graphics.Blit(t, average_value);
//		Debug.Log("average_brightness_value: ");
//	}
//	
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

		effect_material.SetTexture("_source", source);
		
		Graphics.Blit(source,bloom_buffer, effect_material);
		
		for (int i = 0; i< iterations; i++){
			effect_material.SetTexture("_source", bloom_buffer);
			Graphics.Blit(bloom_buffer, bloom_buffer, effect_material);
		}
		
		Graphics.Blit(bloom_buffer, destination);
	}
}	

//		o.offsets_even[0] = float4(-1.0, -1.0, 0, 1.0/16);
//		o.offsets_even[1] = float4(-1.0, 1.0, 0, 1.0/16);
//		o.offsets_even[2] = float4(1.0, -1.0, 0, 1.0/16);
//		o.offsets_even[3] = float4(1.0, 1.0, 0, 1.0/16);
//		o.offsets_uneven[0] = float4(-1.0, 0.0, 0, 2.0/16);
//		o.offsets_uneven[1] = float4(1.0, 0.0, 0, 2.0/16);
//		o.offsets_uneven[2] = float4(0.0, -1.0, 0, 2.0/16);
//		o.offsets_uneven[3] = float4(0.0, 1.0, 0, 2.0/16);

//	float4[] samples[9] = {
//	-1.0, -1.0, 0, 1.0/16,
//	-1.0, 1.0, 0, 1.0/16,
//	 1.0, -1.0, 0, 1.0/16,
//	 1.0, 1.0, 0, 1.0/16,
//	 -1,0, 0.0, 0, 2.0/16,
//	 1.0, 0.0, 0, 2.0/16,
//	 0.0, -1,0, 0, 2.0/16,
//	 0.0, 1.0, 0, 2.0/16,
//	 0.0, 0.0, 0, 4.0/16
//	};