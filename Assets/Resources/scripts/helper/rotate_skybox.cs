using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Skybox))]
[RequireComponent (typeof(Camera))]

public class rotate_skybox : MonoBehaviour {
	public float speed = 0.7f;
	public float speed_detail = 1.5f;
	public GameObject sun;
	private float _rotation = 0;
	private float _rotation_detail = 0;

	//set global Matrix _Rotation for Skybox rotation used by Skybox Animated
	void Start(){
		if (sun == null) {
			Debug.LogError("sun is not assignt to rotate_skybox on the Camera near you!");
		}	
	}
	
	void Update () {
        // Construct a rotation matrix and set it for the shader
		_rotation += speed*Time.deltaTime;
		_rotation_detail += speed_detail*Time.deltaTime;

		
        Quaternion rot = Quaternion.Euler (0f, _rotation, 0f);
		Quaternion rot_detail = Quaternion.Euler (0f, _rotation_detail, 0f);

        Matrix4x4 m = Matrix4x4.TRS (Vector3.zero, rot, new Vector3(1,1,1));
        Matrix4x4 m_detail = Matrix4x4.TRS (Vector3.zero, rot_detail, new Vector3(1,1,1));
        Matrix4x4 m_sun = Matrix4x4.TRS (Vector3.zero, Quaternion.Inverse(sun.transform.rotation), new Vector3(1,1,1));

		GetComponent<Skybox>().material.SetMatrix ("_skybox_rotation", m);
		GetComponent<Skybox>().material.SetMatrix ("_skybox_rotation_detail", m_detail);
		GetComponent<Skybox>().material.SetMatrix ("_skybox_rotation_sun", m_sun);

	}
}
