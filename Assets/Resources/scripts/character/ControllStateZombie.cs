using UnityEngine;
using System.Collections;
public class ZombieIDLEDEATH : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		if(controller.getAttention()){
			return ControllerZombie._ZombieSPAWN;
		}
		
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.Loop("dead");
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
public class ZombieSPAWN : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		controller.controller.BAudioPlayer.Play("skeleton_idle");
		if(controller.stateTime > 1.4f){
			return ControllerZombie._ZombieIDLE;
		}
		
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.Loop("stand_up");
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
public class ZombieIDLE : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		controller.controller.BAudioPlayer.Loop("skeleton_idle");
		if(controller.stateTime > 1){
			if(Input.GetButton("attack")){
				return ControllerZombie._ZombieATTACK;
			}
			
			if(controller.getAttention()){
				return ControllerZombie._ZombieRUN;
			}
			
			if(stats.healthFactor <= 0){
				return ControllerZombie._ZombieDIE;
			}
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
		if(collider.tag == "Stroke"){
			controller.controller.onHit(collider.GetComponent<OStroke>());
			return ControllerZombie._ZombieGETHIT;
		}
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class ZombieSLIDE : ControllState {	
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		if(stats.healthFactor <= 0){
			return ControllerZombie._ZombieDIE;
		}
		
		if(controller.getAttention()){
			return ControllerZombie._ZombieRUN;
		}
		
		if(controller.isStill){
			return ControllerZombie._ZombieIDLE;
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
		if(collider.tag == "Stroke"){
			controller.controller.onHit(collider.GetComponent<OStroke>());
			return ControllerZombie._ZombieGETHIT;
		}
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class ZombieRUN : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		if(controller.isInAttackDistance(2.5f)){
			return ControllerZombie._ZombieATTACK;
		}
		
		if (!controller.getAttention()){
			return ControllerZombie._ZombieSLIDE;
		}
		
		if(stats.healthFactor <= 0){
			return ControllerZombie._ZombieDIE;
		}
	
		controller.runToReferenceObject(0.07f);
		
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
			_BThirdPersonMotionSystem.Loop("run");
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
	 	if(collider.tag == "Stroke"){
			controller.controller.onHit(collider.GetComponent<OStroke>());
			return ControllerZombie._ZombieGETHIT;
		}
		return this;
	}
	public override ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats){
		return this;
	}	
}
public class ZombieATTACK: ControllState {
	bool shot = false;
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		Vector3 mousePoint = Vector3.zero;
		stats.healthRegenerationRate = 0f;
		stats.enduranceRegenerationRate = 0f;
		
		if(controller.stateTime < 0.1){
			controller.turnToReferenceObject();
			controller.controller.slotRightHand.GetComponent<OWeapon>().enable();
			shot = false;
		}
		
		if(Random.Range(1,10) < 3){
			shot = true;
		}
		
		if(controller.stateTime > 0.22f){
			if(shot){
				return ControllerZombie._ZombieATTACKB;
			}
			else{
				controller.controller.slotRightHand.GetComponent<OWeapon>().disable();
				return ControllerZombie._ZombieIDLE;
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
public class ZombieATTACKB: ControllState {
	bool shot = false;
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		Vector3 mousePoint = Vector3.zero;
		stats.healthRegenerationRate = 0f;
		stats.enduranceRegenerationRate = 0f;
		
		if(controller.stateTime < 0.1){
			controller.turnToReferenceObject();
			controller.controller.slotRightHand.GetComponent<OWeapon>().enable();

		}
		
		if(Random.Range(1,10) < 3){
			shot = true;
		}
		
		if(controller.stateTime > 0.22f){
			if(shot){
				return ControllerZombie._ZombieATTACKC;
			}
			else{
				controller.controller.slotRightHand.GetComponent<OWeapon>().disable();
				return ControllerZombie._ZombieIDLE;
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
public class ZombieATTACKC: ControllState {
	bool shot = false;
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		Vector3 mousePoint = Vector3.zero;
		stats.healthRegenerationRate = 0f;
		stats.enduranceRegenerationRate = 0f;
		

		if(controller.stateTime < 0.1f){
			shot = false;
			controller.turnToReferenceObject();
			controller.controller.slotRightHand.GetComponent<OWeapon>().enable();
		}
		
		if(controller.stateTime > 0.18f){
			controller.controller.slotRightHand.GetComponent<OWeapon>().disable();
			return ControllerZombie._ZombieIDLE;
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
public class ZombieJUMP : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		if(controller.stateTime < 0.2f){
		}
		else{
			if(Input.GetButton("action")){
				controller.run(0.06f);
			}
			if(controller.isGrounded){
				return ControllerZombie._ZombieSLIDE;
			}
			
			if(controller.isFalling){
				return ControllerZombie._ZombieFALL;
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
public class ZombieFALL : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		if(controller.position.y < -50){
			return ControllerZombie._ZombieDIE;
		}
		
		if(controller.isGrounded){
			return ControllerZombie._ZombieSLIDE;
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
public class ZombieGETHIT: ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		if(controller.stateTime < 0.1){
			controller.controller.BAudioPlayer.PlayBreak("skeleton_hit_b");
		}
		controller.controller.material.SetColor("_Color",Color.Lerp(Color.black,Color.red,Mathf.PingPong(Time.timeSinceLevelLoad*10,1)));
		if(stats.healthFactor <= 0){
		controller.controller.material.SetColor("_Color",Color.red);
			return ControllerZombie._ZombieDIE;
		}
		if(controller.stateTime > 0.75f){
		controller.controller.material.SetColor("_Color",Color.black);
			return ControllerZombie._ZombieIDLE;
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
public class ZombieDIE : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		if(controller.stateTime < 0.1){
			controller.controller.BAudioPlayer.PlayBreak("skeleton_hit");
		}
		controller.controller.material.SetColor("_Color",Color.Lerp(Color.black,Color.red,Mathf.PingPong(Time.timeSinceLevelLoad*10,1)));
		controller.controller.onDie();
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
public class ZombieWAIT : ControllState {
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