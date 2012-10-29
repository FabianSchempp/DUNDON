using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]

public class normal_camera : MonoBehaviour {
 	private RenderTexture _normal_render_texture;
	public Shader _display_normal_shader;
	private GameObject _normal_camera;
	private Camera _normal_camera_camera_component;
	private normal_blur _normal_blur_component;
	private int _normal_mask = 1;
	
	public RenderTexture normal_render_texture{
		get{return _normal_render_texture; }
	}
	
	void Awake () {
		if (_normal_camera == null){
			//create clipping mask for normals;
			_normal_mask =
					1 << 1 |
					1 << 2 | 
					1 << 3 | 
					1 << 4 | 
					1 << 5 | 
					1 << 6 | 
					1 << 7 | 
					1 << 8 | 
					1 << 9 | 
					1 << 10 | 
					1 << 11 | 
					1 << 12 | 
					1 << 13 |
					1 << 14 |
	//				1 << 15 | 
	//				1 << 16 | 
					1 << 17 |
	//				1 << 18;
					1 << 22;
			
	
			
			//create a new camera;
			_normal_camera =  new GameObject("normal_camera");
			
			//add camera and copy attributes of original camera
			_normal_camera_camera_component = _normal_camera.AddComponent<Camera>();
			_normal_camera_camera_component.CopyFrom(camera);
			_normal_camera_camera_component.depth = -1;
			_normal_camera_camera_component.cullingMask = _normal_mask;
			
			//add blur shader
			_normal_blur_component = _normal_camera.AddComponent<normal_blur>();		
			
			//set_background to null normal
			_normal_camera_camera_component.clearFlags = CameraClearFlags.SolidColor;
			_normal_camera_camera_component.backgroundColor = new Color(0.5f,0.5f,1.0f);
			_normal_camera_camera_component.camera.depthTextureMode = DepthTextureMode.DepthNormals;
			
			//sets replacementshader to render normals
			_normal_camera_camera_component.SetReplacementShader(_display_normal_shader,"");
			
			//parent and reset it to original camera;
			_normal_camera.transform.rotation = transform.rotation;
			_normal_camera.transform.position = transform.position;
			_normal_camera.transform.parent = transform;
	
			
			//create a new Render Texture for the normal pass and assign it to the Camera
			_normal_render_texture = new RenderTexture(Screen.width,Screen.height,32,RenderTextureFormat.ARGBHalf);
			_normal_render_texture.name = "normal_pass";
			_normal_camera_camera_component.targetTexture = _normal_render_texture;
	
			//set normal Texture global for all shaders
			Shader.SetGlobalTexture("_objectNormalTexture", _normal_render_texture);
		}
	}
	
	void OnApplicationQuit(){
		DestroyImmediate(_normal_camera);
	}
}