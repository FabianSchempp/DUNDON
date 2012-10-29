using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]

public class color_gradeing: MonoBehaviour
{	
	private Material effect_material;
	protected void OnEnable() {
		camera.depthTextureMode |= DepthTextureMode.Depth;		
	}
	
	protected void Start()
	{
		effect_material = (Material)Resources.Load("materials/color_gradeing");
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
		effect_material.SetTexture("_source", source);
		Graphics.Blit(source,destination, effect_material);
	}
}	