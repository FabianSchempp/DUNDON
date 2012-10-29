using UnityEngine;
using System.Collections;

public abstract class ControllState{		
	
	public abstract ControllState handleInput(BThirdPerson controller, CharacterStats stats);
	public abstract ControllState handleAnimation(BThirdPersonMotionSystem _BThirdPersonMotionSystem);
	public abstract ControllState handleCollissionEnter(Collision collision, BThirdPerson controller, CharacterStats stats);
	public abstract ControllState handleCollissionStay(Collision collision, BThirdPerson controller, CharacterStats stats);
	public abstract ControllState handleTriggerEnter(Collider collider, BThirdPerson controller, CharacterStats stats);
	public abstract ControllState handleTriggerStay(Collider collider, BThirdPerson controller, CharacterStats stats);
	public abstract ControllState handleTriggerExit(Collider collider,BThirdPerson controller, CharacterStats stats);	
}

