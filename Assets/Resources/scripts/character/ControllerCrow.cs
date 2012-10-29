using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (BThirdPerson))]
public class ControllerCrow : Controller {
	
	//EXPOSED VARIBALES
	public float waterConsumptionRate = 0.1f;
	private float _awarenessRadius = 0;
	public float _awarenessSpeed = 1;
	//
	
	public static ControllState _CrowIDLE;
	public static ControllState _CrowFLY;
	public static ControllState _CrowDIE;
		
	void Start () {
		//setup Controllstates
		_CrowIDLE = new CrowIDLE();
		_CrowFLY = new CrowFLY();
		_CrowDIE = new CrowDIE();
		
		state = _CrowIDLE;
		Initialize();
	}
	public static void onHit(BThirdPerson controller, Collider collider, CharacterStats stats){
		OStroke stroke = collider.gameObject.GetComponent<OStroke>();
		if(stroke){
			controller.hitBack(stroke.strokeForce, collider.gameObject);
			stats.reduceHealthImpact(stroke.strength);
		}
		else{
			stats.reduceHealthImpact(5f);
		}	
	}
	
	public static void onDie(BThirdPerson controller, CharacterStats stats){
	}
	
	void FixedUpdate () {
		UpdateState();
		doCommon();
	}
	
	void doCommon(){
		//Debug.Log("state"+ _BThirdPersonMotionSystem.getAnimationTime("fly"));
	}
	
	void OnCollisionEnter(Collision collision){
		collisionEnter(collision);
	}
	void OnCollisionStay(Collision collision){
		collisionStay(collision);
	}
	void OnTriggerEnter(Collider collider){
		triggerEnter(collider);
	}
	void OnTriggerStay(Collider collider){
		triggerStay(collider);
	}
	void OnTriggerExit(Collider collider){
		triggerExit(collider);
	}
}

