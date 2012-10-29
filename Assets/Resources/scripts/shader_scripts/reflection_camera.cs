using UnityEngine;
using System.Collections;

public class reflection_camera : MonoBehaviour {

 	private RenderTexture _reflection_cubemap;
	private GameObject _reflection_camera;
	private Camera _reflection_camera_camera_component;
	private int _reflection_mask = 1;
	
	public int cubmap_size = 128;
	public bool all_faces = true;
	
	public RenderTexture reflection_cubemap{
		get{return _reflection_cubemap; }
	}
	
	void Awake () {
		
		//create a new camera;
		_reflection_camera =  new GameObject("reflection_camera");
		
		//add camera and copy attributes of original camera
		_reflection_camera_camera_component = _reflection_camera.AddComponent<Camera>();
		_reflection_camera_camera_component.nearClipPlane = 0.5f;
		_reflection_camera_camera_component.farClipPlane = 1000;
		_reflection_camera_camera_component.depth = -1;
		_reflection_camera_camera_component.cullingMask = camera.cullingMask;
		
		//parent and reset it to original camera;
		_reflection_camera.transform.rotation = transform.rotation;
		_reflection_camera.transform.position = transform.position;
		_reflection_camera.transform.parent = transform;
		//disable camera
		_reflection_camera_camera_component.enabled = false;
		
		//initialize cubemap
		_reflection_cubemap = new RenderTexture(cubmap_size, cubmap_size, 16);
		_reflection_cubemap.isPowerOfTwo = true;
		_reflection_cubemap.isCubemap = true;
		_reflection_cubemap.hideFlags = HideFlags.HideAndDontSave;
	}
	
	void Update(){
		//render only one face per frame
		int cube_face = Time.frameCount % 6;
		int face_mask = 1 << cube_face;
		render_cubemapface(face_mask);
	}
	
	void render_cubemapface(int face){
		if (all_faces) _reflection_camera_camera_component.RenderToCubemap(_reflection_cubemap);
		else _reflection_camera_camera_component.RenderToCubemap(_reflection_cubemap,face);

		Shader.SetGlobalTexture("_reflectionTexture", _reflection_cubemap);
	}
}
