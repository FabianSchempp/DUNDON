using UnityEngine;
using System.Collections;

public class exposure_test_camera : MonoBehaviour {

 	private RenderTexture _exposure_test_render_texture;
	private GameObject _exposure_test_camera;
	private Camera _exposure_test_camera_camera_component;
//	private int _exposure_test_mask = 1;
	
	public RenderTexture exposure_test_render_texture{
		get{return _exposure_test_render_texture; }
	}
	
	void Awake () {
//		//create clipping mask for wavess;
//		_waves_mask = 1 << 16;
//		
		//create a new camera;
		_exposure_test_camera =  new GameObject("exposure_test_camera");
		
		//add exposure_test_component;
		_exposure_test_camera.AddComponent("dynamic_exposure_test");
		
		//add camera and copy attributes of original camera
		_exposure_test_camera_camera_component = _exposure_test_camera.AddComponent<Camera>();
		_exposure_test_camera_camera_component.fieldOfView  = camera.fieldOfView;
		_exposure_test_camera_camera_component.nearClipPlane = camera.nearClipPlane;
		_exposure_test_camera_camera_component.farClipPlane = camera.farClipPlane;
//		_exposure_test_camera_camera_component.cullingMask = _waves_mask;
		
		//parent and reset it to original camera;
		_exposure_test_camera.transform.rotation = transform.rotation;
		_exposure_test_camera.transform.position = transform.position;
		_exposure_test_camera.transform.parent = transform;

		
		//create a new Render Texture for the waves pass and assign it to the Camera
		_exposure_test_render_texture = new RenderTexture(Screen.width/100,Screen.height/100,8);
		_exposure_test_render_texture.name = "exposure_test_pass";
		_exposure_test_camera_camera_component.targetTexture = _exposure_test_render_texture;
	}
}