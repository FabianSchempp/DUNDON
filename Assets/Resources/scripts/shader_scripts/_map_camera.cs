using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]

public class _map_camera : MonoBehaviour {

 	private RenderTexture _map_render_texture;
	private GameObject _map_camera_camera;
	private Camera _map_camera_camera_component;
	private int _mask = 1;
	
	public RenderTexture mask_render_texture{
		get{return _map_render_texture; }
	}
	
	void Awake () {
		if (_map_camera_camera == null){
			//create clipping mask for map;
			_mask = 1 << 23 | 1 << 19;
			
			//create a new camera;
			_map_camera_camera =  new GameObject("map_camera");
			//add camera and copy attributes of original camera
			_map_camera_camera_component = _map_camera_camera.AddComponent<Camera>();
			_map_camera_camera_component.CopyFrom(camera);
			_map_camera_camera_component.depth = 2;
					
			_map_camera_camera_component.cullingMask = _mask;
			_map_camera_camera_component.backgroundColor = Color.black;
			_map_camera_camera_component.clearFlags = CameraClearFlags.SolidColor;

			
			
			//parent and reset it to original camera;
			_map_camera_camera.transform.rotation = transform.rotation;
			_map_camera_camera.transform.position = transform.position;
			_map_camera_camera.transform.parent = transform;
		
			
			//create a new Render Texture for the map pass and assign it to the Camera
			_map_render_texture = new RenderTexture(Screen.width,Screen.height,8,RenderTextureFormat.ARGBHalf);
			_map_render_texture.name = "map_pass";
			_map_camera_camera_component.targetTexture = _map_render_texture;
			
			//set mask Texture global for all shaders
		Shader.SetGlobalTexture("_map_texture", _map_render_texture);
		}
	}
	
	void OnApplicationQuit (){
		DestroyImmediate(_map_camera_camera);
	}
}