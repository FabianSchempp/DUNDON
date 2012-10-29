using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]

public class waves_camera : MonoBehaviour {

 	private RenderTexture _waves_render_texture;
	private GameObject _waves_camera;
	private Camera _waves_camera_camera_component;
	private int _waves_mask = 1;
	
	public RenderTexture waves_render_texture{
		get{return _waves_render_texture; }
	}
	
	void Awake () {
		if (_waves_camera == null){
			//create clipping mask for wavess;
			_waves_mask = 1 << 16;
			
			//create a new camera;
			_waves_camera =  new GameObject("waves_camera");
			
			//add camera and copy attributes of original camera
			_waves_camera_camera_component = _waves_camera.AddComponent<Camera>();
			_waves_camera_camera_component.fieldOfView  = camera.fieldOfView;
			_waves_camera_camera_component.nearClipPlane = camera.nearClipPlane;
			_waves_camera_camera_component.farClipPlane = camera.farClipPlane;
			_waves_camera_camera_component.cullingMask = _waves_mask;
			
			//set_background to null normal
			_waves_camera_camera_component.clearFlags = CameraClearFlags.SolidColor;
			_waves_camera_camera_component.backgroundColor = new Color(0.5f,0.5f,1.0f);
				
			//parent and reset it to original camera;
			_waves_camera.transform.rotation = transform.rotation;
			_waves_camera.transform.position = transform.position;
			_waves_camera.transform.parent = transform;
	
			
			//create a new Render Texture for the waves pass and assign it to the Camera
			_waves_render_texture = new RenderTexture(Screen.width,Screen.height,8);
			_waves_render_texture.name = "waves_pass";
			_waves_camera_camera_component.targetTexture = _waves_render_texture;
			
			//set waves Texture global for all shaders
			Shader.SetGlobalTexture("_wavesTexture", _waves_render_texture);
		}
	}
	
	void OnDisable(){
		 DestroyImmediate(_waves_camera);
	}
}
