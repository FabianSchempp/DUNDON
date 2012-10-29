using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SphereCollider))]
public class OAttentionTester : MonoBehaviour {

	public float radius = 0;
	public GameObject attentionObject;
	
	void Start(){
		renderer.enabled = false;
		gameObject.name = "attention tester";
		gameObject.collider.isTrigger = true;
		gameObject.AddComponent<Rigidbody>();
		gameObject.rigidbody.isKinematic = true;
		gameObject.layer = 25;

	}
	
	void Update(){
		transform.localScale = Vector3.one * 2 * (radius/2);
	}
	
	void OnTriggerStay (Collider collider) {
		if(collider.tag == "attention object"){
			collider.gameObject.GetComponent<OAttentionObject>().attention(Vector3.Distance(transform.position,collider.transform.position),radius);	
		}
//		Debug.Log("attention tester: " + collider.name);
	}
	
	void OnTriggerExit (Collider collider) {
		if(collider.tag == "attention object"){
			collider.gameObject.GetComponent<OAttentionObject>().attention(1000,1000);
		}
//		Debug.Log("attention tester: " + collider.name);
	}
	
}
