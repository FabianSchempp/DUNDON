using UnityEngine;
using System.Collections;

public class CaravanPostFXCameras : MonoBehaviour {

private RenderTexture _normal_render_texture;
//	private Shader _display_normal_shader;
//	private GameObject _normal_camera;
//	private Camera _normal_camera_camera_component;
//	private normal_blur _normal_blur_component;
//	private int _normal_mask = 1;
	
//	private RenderTexture _mask_render_texture;
//	private GameObject _mask_camera_camera;
//	private Camera _mask_camera_camera_component;
//	private Shader _vertex_color_mask_shader;
//	private int _mask_mask = 1;
	
	private RenderTexture _map_render_texture;
	private GameObject _map_camera_camera;
	private Camera _map_camera_camera_component;
	private int _map_mask = 1;
	
	public RenderTexture map_render_texture{
		get{return _map_render_texture; }
	}
//	
//	public RenderTexture mask_render_texture{
//		get{return _mask_render_texture; }
//	}
	
//	public RenderTexture normal_render_texture{
//		get{return _normal_render_texture; }
//	}
	
	void Awake () {
//		create_normal_camera();
		create_map_camera();
//		create_mask_camera();
	}
	
//	void create_normal_camera () {
//		if (_normal_camera == null){
//			//create clipping mask for normals;
//			_normal_mask =
//					1 << 1 |
//					1 << 2 | 
//					1 << 3 | 
//					1 << 4 | 
//					1 << 5 | 
//					1 << 6 | 
//					1 << 7 | 
//					1 << 8 | 
//					1 << 9 | 
//					1 << 10 | 
//					1 << 11 | 
//					1 << 12 | 
//					1 << 13 |
//					1 << 14 |
//					1 << 17 |
//					1 << 22;
//			
//	
//			_display_normal_shader = (Shader)Resources.Load("shader/postFX/display_normals");
//			//create a new camera;
//			_normal_camera =  new GameObject("normal_camera");
//			
//			//add camera and copy attributes of original camera
//			_normal_camera_camera_component = _normal_camera.AddComponent<Camera>();
//			_normal_camera_camera_component.CopyFrom(camera);
//			_normal_camera_camera_component.depth = -1;
//			_normal_camera_camera_component.cullingMask = _normal_mask;
//			
//			//add blur shader
//			_normal_blur_component = _normal_camera.AddComponent<normal_blur>();		
//			
//			//set_background to null normal
//			_normal_camera_camera_component.clearFlags = CameraClearFlags.SolidColor;
//			_normal_camera_camera_component.backgroundColor = new Color(0.5f,0.5f,1.0f);
//			_normal_camera_camera_component.camera.depthTextureMode = DepthTextureMode.DepthNormals;
//			
//			//sets replacementshader to render normals
//			_normal_camera_camera_component.SetReplacementShader(_display_normal_shader,"");
//			
//			//parent and reset it to original camera;
//			_normal_camera.transform.rotation = transform.rotation;
//			_normal_camera.transform.position = transform.position;
//			_normal_camera.transform.parent = transform;
//	
//			
//			//create a new Render Texture for the normal pass and assign it to the Camera
//			_normal_render_texture = new RenderTexture(Screen.width,Screen.height,32,RenderTextureFormat.ARGBHalf);
//			_normal_render_texture.name = "normal_pass";
//			_normal_camera_camera_component.targetTexture = _normal_render_texture;
//	
//			//set normal Texture global for all shaders
//			Shader.SetGlobalTexture("_objectNormalTexture", _normal_render_texture);
//		}
//	}
//	void create_mask_camera () {
//		_vertex_color_mask_shader = (Shader)Resources.Load("shader/hdr_vertex_color_mask");
//
//		if (_mask_camera_camera == null){
//			//create clipping mask for masks;
//			_mask_mask =
//					1 << 0 |
//					1 << 1 |
//					1 << 2 | 
//					1 << 3 | 
//					1 << 4 | 
//					1 << 5 | 
//					1 << 6 | 
//					1 << 7 | 
//					1 << 8 | 
//					1 << 9 | 
//					1 << 10 | 
//					1 << 11 | 
//					1 << 12 | 
//					1 << 13 |
//					1 << 14 |
//					1 << 17 |
//					1 << 18;
//			
//			//create a new camera;
//			_mask_camera_camera =  new GameObject("mask_camera");
//			//add camera and copy attributes of original camera
//			_mask_camera_camera_component = _mask_camera_camera.AddComponent<Camera>();
//			_mask_camera_camera_component.CopyFrom(camera);
//			_mask_camera_camera_component.depth = -2;
//			
//	//		//sets replacementshader to render normals
//	//		_mask_camera_camera_component.SetReplacementShader(gray_unlit_shader ,"Opaque");
//			
//			//set_background to null mask
//			_mask_camera_camera_component.clearFlags = CameraClearFlags.SolidColor;
//			_mask_camera_camera_component.backgroundColor = new Color(0.5f,0.5f,0.5f);
//			_mask_camera_camera_component.cullingMask = _mask_mask;
//			
//			//set replacementshader
//			_mask_camera_camera_component.SetReplacementShader(_vertex_color_mask_shader,"");
//			
//			//parent and reset it to original camera;
//			_mask_camera_camera.transform.rotation = transform.rotation;
//			_mask_camera_camera.transform.position = transform.position;
//			_mask_camera_camera.transform.parent = transform;
//			
//			//set hdr rendering to on
//			_mask_camera_camera_component.hdr = true;
//			
//			//create a new Render Texture for the mask pass and assign it to the Camera
//			_mask_render_texture = new RenderTexture(Screen.width,Screen.height,8,RenderTextureFormat.ARGBHalf);
//			_mask_render_texture.name = "mask_pass";
//			_mask_camera_camera_component.targetTexture = _mask_render_texture;
//			
//			//set mask Texture global for all shaders
//		Shader.SetGlobalTexture("_mask_Texture", _mask_render_texture);
//		}
//	}
	void create_map_camera () {
		if (_map_camera_camera == null){
			//create clipping mask for map;
			_map_mask = 1 << 23 | 1 << 19;
			
			//create a new camera;
			_map_camera_camera =  new GameObject("map_camera");
			//add camera and copy attributes of original camera
			_map_camera_camera_component = _map_camera_camera.AddComponent<Camera>();
			_map_camera_camera_component.CopyFrom(camera);
			_map_camera_camera_component.depth = 2;
					
			_map_camera_camera_component.cullingMask = _map_mask;
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
	
//	void OnApplicationQuit(){
//		DestroyImmediate(_normal_camera);
//	}
}