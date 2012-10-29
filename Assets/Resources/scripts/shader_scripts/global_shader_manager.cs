using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class global_shader_manager : MonoBehaviour {
	
	public Texture _shading_ramp;
	
	public Texture _sand_far;
	public Texture _sand_near;
	public Texture _sand_specular;
	public float _sand_texture_scale = 0.5f;
	
	public float _distance_contrast = 0.02f;
	public float _distance_brightness = 5.7f;
	
	public bool update_realtime = false;
	
	public float sandstorm_scale = 0.1f;
	public float sandstorm_speed = 25;
	
	// Use this for initialization
	void Start () {
		Shader.SetGlobalTexture("_shading_ramp", _shading_ramp);
		Shader.SetGlobalTexture("_sand_diffuse", _sand_near);
		Shader.SetGlobalTexture("_sand_specular", _sand_specular);
		Shader.SetGlobalTexture("_sand_diffuse_far", _sand_far);

	}
	
	// Update is called once per frame
	void Update () {
		Shader.SetGlobalFloat("_distance_fog_contrast", _distance_contrast);
		Shader.SetGlobalFloat("_distance_fog_brightness", _distance_brightness);
		Shader.SetGlobalFloat("_sand_texture_scale", _sand_texture_scale);
		Shader.SetGlobalMatrix("_UNITY_MATRIX_V",Camera.mainCamera.worldToCameraMatrix);
		Shader.SetGlobalFloat("_dust_storm_scale",sandstorm_scale);
		Shader.SetGlobalFloat("_sand_storm_speed",sandstorm_speed);
		
		if(update_realtime){
			Shader.SetGlobalTexture("_shading_ramp", _shading_ramp);
			Shader.SetGlobalTexture("_sand_diffuse", _sand_near);
			Shader.SetGlobalTexture("_sand_specular", _sand_specular);
			Shader.SetGlobalTexture("_sand_diffuse_far", _sand_far);
		}
	}
}
