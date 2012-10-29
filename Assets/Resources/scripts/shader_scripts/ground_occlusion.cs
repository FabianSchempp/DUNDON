using UnityEngine;
using System.Collections;

public class ground_occlusion : MonoBehaviour {

	public GameObject ground_point;
	
	// pass position to material
	void Update () {
		renderer.material.SetVector("_reference_vector", ground_point.transform.position);
	}
}
