using UnityEngine;
using System.Collections;

public class flee_shader : MonoBehaviour {

	void OnTriggerStay(Collider collider) {
		renderer.material.SetVector("_ref_vector", collider.transform.position);
	}
	
	void OnTriggerEnter(Collider collider) {
		renderer.material.SetVector("_ref_vector", collider.transform.position);
	}
}
