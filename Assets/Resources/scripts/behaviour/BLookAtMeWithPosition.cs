using UnityEngine;
using System.Collections;

public class BLookAtMeWithPosition : MonoBehaviour {

	public float duration = 2;
	public GameObject target;
	public GameObject positionTarget;
	public float speed = 1;
	public bool directional = false;
	
	void OnTriggerEnter (Collider collider) {
		if(collider.tag == "Player"){
			if(directional){
				if(Helper.isPointInFrontOfObject(collider.gameObject,target.transform.position)){
					ControllerCamera.lookAtWithPosition(target,positionTarget,duration,speed);
				}
			}
			else{
				ControllerCamera.lookAtWithPosition(target,positionTarget,duration,speed);
			}
		}
	}
}
