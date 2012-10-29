using UnityEngine;
using System.Collections;

public class KnightSIT : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		
		ControllerCamera.lookAtWithPosition(CameraPointManager.Targets["title_target"],CameraPointManager.Positions["title_position"],0.5f,5);
		stats.healthRegenerationRate = 0f;
		stats.enduranceRegenerationRate = 0f;
		
		controller.controller.BAudioPlayer.Loop("idle_breath_a");
		
		if(Input.GetButtonDown("attack")){
			return ControllerKnight._KnightSTANDUP;
		}
		else if(Input.GetButtonDown("open_map")){
			return ControllerKnight._KnightSTANDUP;

		}
		else if(Input.GetButton("action")){
			return ControllerKnight._KnightSTANDUP;
		}
		
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.CrossFade("sit");
		return this;
	}
	public override ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class KnightSTANDUP : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		controller.controller.BAudioPlayer.Play("stand_up_from_sit");
		if(controller.stateTime > 1.65f){
			return ControllerKnight._KnightIDLE;
		}
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.CrossFade("stand_up_from_sit");
		return this;
	}
	public override ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class KnightIDLE : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		Shader.SetGlobalFloat("_map_fade", Mathf.Clamp01(controller.stateTime * 5));
		Shader.SetGlobalFloat("trailDust", Mathf.Clamp01(1 - controller.stateTime * 2.5f));
		stats.healthRegenerationRate = -0.3f;
		stats.enduranceRegenerationRate = 50f;
		
		controller.controller.BAudioPlayer.Loop("idle_breath_b");
		
		if(stats.healthFactor <= 0){
			return ControllerKnight._KnightDIE;
		}
		else if(Input.GetButtonDown("attack") && stats.reduceEndurance(35) && controller.stateTime > 0.2f){
			return ControllerKnight._KnightATTACK;
		}
		else if(Input.GetButtonDown("open_map") && controller.stateTime > 0.7f){
		
		}
		else if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.5f  || Mathf.Abs(Input.GetAxis("Vertical")) > 0.5f){
			return ControllerKnight._KnightRUN;
		}
		
		
		
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.CrossFade("idle");
		return this;
	}
	public override ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats){
		if(collider.tag == "Pile"){
			return ControllerKnight._KnightIDLEONFIRE;
		}
		if(collider.tag == "Item"){
			OItem i = 	collider.GetComponent<OItem>();
			if(i) i.collectItem(controller.controller);
		}
		if(collider.tag == "StrokeBad"){
			controller.controller.onHit(collider.GetComponent<OStroke>());
			return ControllerKnight._KnightGETHIT;
		}
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class KnightIDLEONFIRE : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		Shader.SetGlobalFloat("_map_fade", Mathf.Clamp01(controller.stateTime * 5));
		Shader.SetGlobalFloat("trailDust", Mathf.Clamp01(1 - controller.stateTime * 2.5f));
		
		stats.healthRegenerationRate = 5f;
		stats.enduranceRegenerationRate = 50f;
		
		controller.controller.BAudioPlayer.Loop("idle_breath_b");
		
		if(stats.healthFactor <= 0){
			return ControllerKnight._KnightDIE;
		}
		else if(Input.GetButtonDown("attack") && stats.reduceEndurance(35) && controller.stateTime > 0.2f){
			return ControllerKnight._KnightATTACK;
		}
		else if(Input.GetButtonDown("open_map") && controller.stateTime > 0.7f){
		
		}
		else if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0  || Mathf.Abs(Input.GetAxis("Vertical")) > 0){
			return ControllerKnight._KnightRUN;
		}
		
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.Loop("idle");
		return this;
	}
	public override ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats){
		if(collider.tag == "StrokeBad"){
			controller.controller.onHit(collider.GetComponent<OStroke>());
			return ControllerKnight._KnightGETHIT;
		}
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class KnightIDLEONITEM : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		Shader.SetGlobalFloat("_map_fade", Mathf.Clamp01(controller.stateTime * 5));
		Shader.SetGlobalFloat("trailDust", Mathf.Clamp01(1 - controller.stateTime * 2.5f));
		
		
		if(Input.GetButton("jump")){
			controller.jumpHard();
			return ControllerKnight._KnightJUMP;
		}
		
		if(Input.GetButtonDown("open_map") && controller.stateTime > 0.7f){
//			controller.referenceTransforms[5].gameObject.GetComponent<OMap>().unfold();
//			return ControllerKnight._KnightMAP;
		}
		
		if(Input.GetButton("action")){
			return ControllerKnight._KnightRUN;
		}
		
		if(stats.healthFactor <= 0){
			return ControllerKnight._KnightDIE;
		}
		
//		if(controller.isOnWall){
//			return ControllerKnight._KnightIDLEWALL;
//		}
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.Loop("idle");
		return this;
	}
	public override ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats){
		if(collider.tag == "fruit" || collider.tag == "throwStuff"){
			controller.attachObject = collider.gameObject;
			return ControllerKnight._KnightIDLEONITEM;
		}
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class KnightSLIDE : ControllState {	
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		
		controller.controller.BAudioPlayer.Loop("idle_breath_b");
		
		stats.healthRegenerationRate = -0.2f;
		stats.enduranceRegenerationRate = 40f;
		
		if(stats.healthFactor <= 0){
			return ControllerKnight._KnightDIE;
		}
		else if(Input.GetButtonDown("attack") && stats.reduceEndurance(35) && controller.stateTime > 0.2f){
			return ControllerKnight._KnightATTACK;
		}
		else if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.5f  || Mathf.Abs(Input.GetAxis("Vertical")) > 0.5f){
			return ControllerKnight._KnightRUN;
		}
		
		if(controller.isStill){
			return ControllerKnight._KnightIDLE;
		}
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.CrossFade("slide");
		return this;
	}
	public override ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats){
		if(collider.tag == "Item"){
			OItem i = 	collider.GetComponent<OItem>();
			if(i) i.collectItem(controller.controller);
		}
		if(collider.tag == "StrokeBad"){
			controller.controller.onHit(collider.GetComponent<OStroke>());
			return ControllerKnight._KnightGETHIT;
		}
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class KnightRUN : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
		Vector3 inputVectorTransformed = new Vector3(inputVector.x,0,-inputVector.y);
		Vector3 lookTarget = controller.position + (ControllerCamera.hingeUp.transform.rotation*inputVectorTransformed);
		
		Debug.Log(inputVector.sqrMagnitude);
		controller.controller.BAudioPlayer.Loop("run_breath_a");
		
		stats.healthRegenerationRate = -0.1f;
		stats.enduranceRegenerationRate = 5f;
		
		if(Input.GetButtonDown("attack") && stats.reduceEndurance(35)){
			return ControllerKnight._KnightATTACK;
		}
		if(stats.healthFactor <= 0){
			return ControllerKnight._KnightDIE;
		}
		if(inputVector.sqrMagnitude < 0.5f && controller.stateTime > 0.2f){
			return ControllerKnight._KnightSLIDE;
		}
		
		controller.run(0.07f);
		controller.turn(lookTarget);
		return ControllerKnight._KnightRUN;

		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
			_BThirdPersonMotionSystem.Play("run");
		return this;
	}
	public override ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats){
		if(collider.tag == "Item"){
			OItem i = 	collider.GetComponent<OItem>();
			if(i) i.collectItem(controller.controller);
		}
		if(collider.tag == "StrokeBad"){
			controller.controller.onHit(collider.GetComponent<OStroke>());
			return ControllerKnight._KnightGETHIT;
		}
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class KnightATTACK: ControllState {
	bool shot = false;
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		Vector3 mousePoint = Vector3.zero;
		stats.healthRegenerationRate = 0f;
		stats.enduranceRegenerationRate = 0f;
		
		if(controller.stateTime < 0.1){
			controller.controller.slotRightHand.GetComponent<OWeapon>().enable();
			shot = false;
			Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
			Vector3 inputVectorTransformed = new Vector3(inputVector.x,0,-inputVector.y);
			Vector3 lookTarget = controller.position + (ControllerCamera.hingeUp.transform.rotation*inputVectorTransformed);
			controller.turn(lookTarget);
		}
		
		if(Input.GetButtonDown("attack") && stats.reduceEndurance(7) && !shot){
			shot = true;
		}
		
		if(controller.stateTime > 0.22f){
			if(shot){
				return ControllerKnight._KnightATTACKB;
			}
			else{
				controller.controller.slotRightHand.GetComponent<OWeapon>().disable();
				return ControllerKnight._KnightIDLE;
			}
		}
		
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.Play("attack_sword_one_handed");
		return this;
	}
	public override ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class KnightATTACKB: ControllState {
	bool shot = false;
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		Vector3 mousePoint = Vector3.zero;
		stats.healthRegenerationRate = 0f;
		stats.enduranceRegenerationRate = 0f;
		
		if(controller.stateTime < 0.1){
			controller.controller.slotRightHand.GetComponent<OWeapon>().enable();
			shot = false;
			Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
			Vector3 inputVectorTransformed = new Vector3(inputVector.x,0,-inputVector.y);
			Vector3 lookTarget = controller.position + (ControllerCamera.hingeUp.transform.rotation*inputVectorTransformed);
			controller.turn(lookTarget);
		}
		if(Input.GetButtonDown("attack") && stats.reduceEndurance(35) && !shot){
			shot = true;
		}

		if(controller.stateTime > 0.22f){
			if(shot){
				return ControllerKnight._KnightATTACKC;
			}
			else{
				controller.controller.slotRightHand.GetComponent<OWeapon>().disable();
				return ControllerKnight._KnightIDLE;
			}
		}
		
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.Play("attack_sword_one_handed_b");
		return this;
	}
	public override ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class KnightATTACKC: ControllState {
	bool shot = false;
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		Vector3 mousePoint = Vector3.zero;
		stats.healthRegenerationRate = 0f;
		stats.enduranceRegenerationRate = 0f;
		
		if(controller.stateTime < 0.1){
			controller.controller.slotRightHand.GetComponent<OWeapon>().enable();
			shot = false;
			Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
			Vector3 inputVectorTransformed = new Vector3(inputVector.x,0,-inputVector.y);
			Vector3 lookTarget = controller.position + (ControllerCamera.hingeUp.transform.rotation*inputVectorTransformed);
			controller.turn(lookTarget);
		}
		
		if(controller.stateTime > 0.18f){
			controller.controller.slotRightHand.GetComponent<OWeapon>().disable();
			return ControllerKnight._KnightIDLE;
		}
		
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.Play("attack_sword_one_handed_c");
		return this;
	}
	public override ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class KnightJUMP : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		Shader.SetGlobalFloat("trailDust", Mathf.Clamp01(1 - controller.stateTime * 2.5f));
		if(controller.stateTime < 0.2f){
		}
		else{
			if(Input.GetButton("action")){
				controller.run(0.06f);
			}
			if(controller.isGrounded){
				return ControllerKnight._KnightSLIDE;
			}
			
			if(controller.isFalling){
				return ControllerKnight._KnightFALL;
			}	
		}
		
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.CrossFade("jump");
		return this;
	}
	public override ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class KnightFALL : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		if(controller.position.y < -50){
			return ControllerKnight._KnightDIE;
		}
		
		if(controller.isGrounded){
			return ControllerKnight._KnightSLIDE;
		}
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.CrossFade("fall");
		return this;
	}
	public override ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class KnightGETHIT: ControllState {
	private string[] hitSounds = new string[3]{"get_hit_a","get_hit_b","get_hit_c"};
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		controller.controller.BAudioPlayer.PlayRandom(hitSounds);
		controller.controller.material.SetColor("_Color",Color.Lerp(Color.black,Color.red,Mathf.PingPong(Time.timeSinceLevelLoad*10,1)));
		if(stats.healthFactor <= 0){
		controller.controller.material.SetColor("_Color",Color.red);
			return ControllerKnight._KnightDIE;
		}
		if(controller.stateTime > 0.75f){
		controller.controller.material.SetColor("_Color",Color.black);
			return ControllerKnight._KnightIDLE;
		}
		
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.Play("get_hit");
		return this;
	}
	public override ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class KnightDIE : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		controller.controller.BAudioPlayer.Play("die");
		controller.controller.material.SetColor("_Color",Color.Lerp(Color.black,Color.red,Mathf.PingPong(Time.timeSinceLevelLoad*10,1)));
		if(controller.stateTime > 1 && controller.stateTime < 2.5f){
		}
		else if(controller.stateTime > 2.5f){
			if(Input.GetButton("action")){
				Debug.Log("sendDeath " + controller.lastValidGroundPoint);
				die.sendDeath(controller.lastValidGroundPoint,111);
				Application.LoadLevel(0);
			}
		}
		
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.CrossFade("die");
		return this;
	}
	public override ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class KnightWAIT : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		return this;
	}
	public override ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats){
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}