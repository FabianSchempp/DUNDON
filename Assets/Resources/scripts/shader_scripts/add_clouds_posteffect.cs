using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]

public class add_clouds_posteffect : MonoBehaviour
{	
	
	public int iterations = 3;
	public float blurSpread = 0.6f;
	
	public Material effect_material;
	public Camera clouds_cam;
	
	protected void OnDisable() {
		if( effect_material ) {
			DestroyImmediate( effect_material.shader );
			DestroyImmediate( effect_material );
		}
	}	
	
	protected void Start()
	{
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
	
	// Called by the camera to apply the image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {	
		//RenderTexture buffer = RenderTexture.GetTemporary(source.width, source.height,16);
		effect_material.SetTexture("_base",source);
		effect_material.SetTexture("_clouds",clouds_cam.targetTexture);
		Graphics.Blit(source,destination, effect_material);
		//RenderTexture.ReleaseTemporary(buffer);
	}
}		//clouds_cam.targetTexture