using UnityEngine;
using System.Collections;

public class BLookAtMe : MonoBehaviour {

	public float duration = 2;
	public GameObject target;
	public float speed = 1;
	void OnTriggerEnter (Collider collider) {
		if(collider.tag == "Player"){
			ControllerCamera.lookAt(target,duration,speed);
		}
	}
}
