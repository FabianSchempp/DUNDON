using UnityEngine;
using System.Collections;

public class CrowIDLE : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		if(controller.getAttention()){
			controller.controller.BAudioPlayer.Play("crow_talk");
			return ControllerCrow._CrowFLY;
		}
		
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		_BThirdPersonMotionSystem.Loop("pick");
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
public class CrowFLY : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
		if(controller.stateTime > 5.625){
			
		}
		return this;
	}
	public override ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem){
		if(_BThirdPersonMotionSystem.stateTime < 0.5f){
			_BThirdPersonMotionSystem.CrossFade("start_fly");
		}
		else{
			_BThirdPersonMotionSystem.CrossFade("fly");
		}
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
public class CrowDIE : ControllState {
	public override ControllState handleInput(BThirdPerson controller, CharacterStats stats){
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