using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]

public class decal_camera : MonoBehaviour {
	
 	private RenderTexture _decal_render_texture;
	private GameObject _decal_camera;
	private Camera _decal_camera_camera_component;
	private int _decal_mask = 1;
	
	public RenderTexture decal_render_texture{
		get{return _decal_render_texture; }
	}
	
	void Awake () {
		if (_decal_camera == null){
			//create clipping mask for decals;
			_decal_mask = 1 << 15;
			
			//create a new camera;
			_decal_camera =  new GameObject("decal_camera");
			
			//add camera and copy attributes of original camera
			_decal_camera_camera_component = _decal_camera.AddComponent<Camera>();
			_decal_camera_camera_component.fieldOfView  = camera.fieldOfView;
			_decal_camera_camera_component.nearClipPlane = camera.nearClipPlane;
			_decal_camera_camera_component.farClipPlane = camera.farClipPlane;
			_decal_camera_camera_component.cullingMask = _decal_mask;
			_decal_camera_camera_component.depth = -1;
			
			//set_background to null normal
			_decal_camera_camera_component.clearFlags = CameraClearFlags.SolidColor;
			_decal_camera_camera_component.backgroundColor = new Color(0.5f,0.5f,1.0f);
				
			//parent and reset it to original camera;
			_decal_camera.transform.rotation = transform.rotation;
			_decal_camera.transform.position = transform.position;
			_decal_camera.transform.parent = transform;
	
			
			//create a new Render Texture for the decal pass and assign it to the Camera
			_decal_render_texture = new RenderTexture(Screen.width,Screen.height,8);
			_decal_render_texture.name = "decal_pass";
			_decal_camera_camera_component.targetTexture = _decal_render_texture;
			
			//set Decal Texture global for all shaders
			Shader.SetGlobalTexture("_decalTexture", _decal_render_texture);
		}
	}
	
	void OnDisable(){
		 DestroyImmediate(_decal_camera);
	}
}
