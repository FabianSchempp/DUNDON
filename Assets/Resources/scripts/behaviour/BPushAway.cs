using UnityEngine;
using System.Collections;

public class BPushAway : MonoBehaviour {
	
	public Transform shearBone;
	public Transform shearTarget;
	public Transform rotate;

	void OnTriggerStay(Collider collider){
		if(collider.tag == "Player"){
			rotate.transform.rotation = Quaternion.Lerp(rotate.transform.rotation, Quaternion.LookRotation(collider.transform.position - transform.position), Time.deltaTime * 5);
			shearBone.transform.position = Vector3.Lerp(shearBone.transform.position, shearTarget.transform.position, Time.deltaTime * 5);
		}
	}
	
	void OnTriggerExit(Collider collider){
		if(collider.tag == "Player"){
			StartCoroutine(swingBack());
		}
	}
	
	IEnumerator swingBack(){
		float force = 100;
		while(Quaternion.Angle(rotate.transform.localRotation, Quaternion.identity) > 0.5f){
			rotate.transform.localRotation = Quaternion.Lerp(rotate.transform.localRotation, Quaternion.identity, Time.deltaTime * 5);
			shearBone.transform.position = Vector3.Lerp(shearBone.transform.position, shearTarget.transform.position, Time.deltaTime * 5);
			yield return null;
		}
	}
}

//public class BPushAway : MonoBehaviour {
//	
//	string direction;
//
//	void OnTriggerEnter(Collider collider){
//		if(collider.tag == "Player"){
//			direction = Helper.getRelativQuadPosition(gameObject,collider.transform.position);
//			if(direction == "right"){
//				animation.CrossFade("reed_swing_right");
//			}
//			else if(direction == "left"){
//				animation.CrossFade("reed_swing_left");
//			}
//			else if(direction == "forward"){
//				animation.CrossFade("reed_swing_forward");
//			}
//			else if(direction == "backwards"){
//				animation.CrossFade("reed_swing_back");
//			}
//		}
//	}
//	
//	void OnTriggerExit(Collider collider){
//		if(collider.tag == "Player"){
//			if(direction == "right"){
//				animation.CrossFade("reed_swing_back_right");
//			}
//			else if(direction == "left"){
//				animation.CrossFade("reed_swing_back_left");
//			}
//			else if(direction == "forward"){
//				animation.CrossFade("reed_swing_back_forward");
//			}
//			else if(direction == "backwards"){
//				animation.CrossFade("reed_swing_back_back");
//			}
//		}
//	}
//	
//	void Update(){
//		Debug.DrawRay(transform.position,transform.right);
//		Debug.DrawRay(transform.position,transform.forward);
//	}
//}
//public class BPushAway : MonoBehaviour {
//	
//	Vector3 startRotation;
//	
//	void Start(){
//		startRotation = transform.localEulerAngles;
//	}
//
//	void OnTriggerStay(Collider collider){
//		if(collider.tag == "Player"){
////			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position - collider.transform.position),2 * Time.deltaTime);
//			Vector3 pos = collider.gameObject.transform.position;
//			Vector3 localRotation = transform.localEulerAngles;
//			localRotation.x = Helper.getRelativAngleOnX(gameObject,pos);
//			localRotation.z = Helper.getRelativAngleOnZ(gameObject,pos);
//			transform.localEulerAngles =  Vector3.Lerp(transform.localEulerAngles,localRotation,Time.deltaTime);
//			Debug.Log("euler " + localRotation);
//		}
//	}
//	
//	void OnTriggerExit(Collider collider){
//		if(collider.tag == "Player"){
//			StartCoroutine(swingBack());
//		}
//	}
//	
//	IEnumerator swingBack(){
//		float force = 100;
//		bool swingBack = true;
//		while(swingBack){
//			bool swingX = true;
//			bool swingZ = true;
//			Vector3 localRotation = transform.localEulerAngles;
//			Vector3 localRotationTarget = localRotation - startRotation;
//			if(Mathf.Abs(localRotationTarget.x) > 0.5f){
//				localRotation.x += force*Time.deltaTime * Mathf.Sign(localRotationTarget.x);
//			}
//			else{
//				swingX = false;
//			}
//			if(Mathf.Abs(localRotationTarget.z) > 0.5f){
//				localRotation.z += force*Time.deltaTime * Mathf.Sign(localRotationTarget.z);
//			}
//			else{
//				swingZ = false;
//			}
//			if(!swingX && !swingZ){
//				swingBack = false;
//			}
//			
//			Debug.Log("euler " + localRotationTarget);
//			transform.localEulerAngles =  localRotation;
//			force = Mathf.Clamp(force - Time.deltaTime*10, 0.25f, 100);
//			yield return null;
//		}
//	}
//}