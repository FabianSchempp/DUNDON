using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]

public class _storm_camera : MonoBehaviour {

 	private RenderTexture _strom_render_texture;
	private GameObject _strom_camera_camera;
	private Camera _strom_camera_camera_component;
	private int _mask = 1;
	
	public RenderTexture mask_render_texture{
		get{return _strom_render_texture; }
	}
	
	void Awake () {
		if (_strom_camera_camera == null){
			//create clipping mask for strom;
			_mask = 1 << 24;
			
			//create a new camera;
			_strom_camera_camera =  new GameObject("strom_camera");
			//add camera and copy attributes of original camera
			_strom_camera_camera_component = _strom_camera_camera.AddComponent<Camera>();
			_strom_camera_camera.AddComponent<color_distortion_blur>();
			_strom_camera_camera_component.CopyFrom(camera);
			_strom_camera_camera_component.depth = 2;
					
			_strom_camera_camera_component.cullingMask = _mask;
			_strom_camera_camera_component.backgroundColor = Color.black;
			_strom_camera_camera_component.clearFlags = CameraClearFlags.SolidColor;
			
			
			
			//parent and reset it to original camera;
			_strom_camera_camera.transform.rotation = transform.rotation;
			_strom_camera_camera.transform.position = transform.position;
			_strom_camera_camera.transform.parent = transform;
		
			
			//create a new Render Texture for the strom pass and assign it to the Camera
			_strom_render_texture = new RenderTexture(Screen.width,Screen.height,8,RenderTextureFormat.ARGBHalf);
			_strom_render_texture.name = "strom_pass";
			_strom_camera_camera_component.targetTexture = _strom_render_texture;
			
			//set mask Texture global for all shaders
		Shader.SetGlobalTexture("_strom_texture", _strom_render_texture);
		}
	}
	
	void OnApplicationQuit (){
		DestroyImmediate(_strom_camera_camera);
	}
}