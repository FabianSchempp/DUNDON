using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]

public class _mask_camera : MonoBehaviour {

 	private RenderTexture _mask_render_texture;
	private GameObject _mask_camera_camera;
	private Camera _mask_camera_camera_component;
	private int _mask = 1;
//	public Shader gray_unlit_shader;
	
	public RenderTexture mask_render_texture{
		get{return _mask_render_texture; }
	}
	
	void Awake () {
		//create clipping mask for masks;
		_mask =
//				1 << 1 |
//				1 << 2 | 
//				1 << 3 | 
//				1 << 4 | 
//				1 << 5 | 
//				1 << 6 | 
//				1 << 7 | 
//				1 << 8 | 
//				1 << 9 | 
//				1 << 10 | 
//				1 << 11 | 
//				1 << 12 | 
//				1 << 13 |
//				1 << 14 |
//				1 << 15 | 
//				1 << 16 | 
//				1 << 17 |
				1 << 18;
		
		//create a new camera;
		_mask_camera_camera =  new GameObject("mask_camera");
		
		//add camera and copy attributes of original camera
		_mask_camera_camera_component = _mask_camera_camera.AddComponent<Camera>();
		_mask_camera_camera_component.CopyFrom(camera);
		_mask_camera_camera_component.depth = -2;
		
//		//sets replacementshader to render normals
//		_mask_camera_camera_component.SetReplacementShader(gray_unlit_shader ,"Opaque");
		
		//set_background to null mask
		_mask_camera_camera_component.clearFlags = CameraClearFlags.SolidColor;
		_mask_camera_camera_component.backgroundColor = new Color(1f,1f,1f);
		_mask_camera_camera_component.cullingMask = _mask;
		
		//parent and reset it to original camera;
		_mask_camera_camera.transform.rotation = transform.rotation;
		_mask_camera_camera.transform.position = transform.position;
		_mask_camera_camera.transform.parent = transform;
		
		//set hdr rendering to on
		_mask_camera_camera_component.hdr = true;
		
		//create a new Render Texture for the mask pass and assign it to the Camera
		_mask_render_texture = new RenderTexture(Screen.width,Screen.height,8,RenderTextureFormat.ARGBHalf);
		_mask_render_texture.name = "mask_pass";
		_mask_camera_camera_component.targetTexture = _mask_render_texture;
		
		//set mask Texture global for all shaders
		Shader.SetGlobalTexture("_mask_Texture", _mask_render_texture);
	}
}