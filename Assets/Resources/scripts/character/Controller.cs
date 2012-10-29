using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BThirdPerson))]
public class Controller: MonoBehaviour {
	
	protected CharacterStats _stats = new CharacterStats(100);
	public CharacterStats stats{
		set {_stats = value;}
		get{return _stats;}
	}
	protected float stateTime = 0;
	protected ControllState _state;
	protected ControllState state{
		get{return _state;}
		set{
			if(value != state){
				stateTime = 0;
			}
			_state = value;
			}
	}
	
	protected BThirdPerson _BThirdPerson;
	protected BThirdPersonMotionSystem _BThirdPersonMotionSystem;
	protected BAudioPlayer _BAudioPlayer;
	public BAudioPlayer BAudioPlayer{
		get{return _BAudioPlayer;}
	}
	public GameObject[] slots;
	public Transform handRight;
	public Transform handLeft;
	public GameObject slotRightHand;
	public GameObject slotLeftHand;
	
	public Renderer renderer;
	public Material material;
	
	protected void Initialize(){
		_BThirdPerson = gameObject.GetComponent<BThirdPerson>();
		_BThirdPersonMotionSystem = gameObject.GetComponent<BThirdPersonMotionSystem>();
		_BThirdPerson.controller = this;
		_BAudioPlayer = gameObject.GetComponent<BAudioPlayer>();
		material = renderer.material;
		setSlotsHand();
	}
	
	protected void UpdateState(){
		state =  state.handleInput(_BThirdPerson,stats);
		state =  state.handleAnimation(_BThirdPersonMotionSystem);
		stateTime += Time.deltaTime;
		stats.updateStats();
		_BThirdPerson.stateTime = stateTime;
		_BThirdPersonMotionSystem.stateTime = stateTime;
	}
	
	public void setSlotsHand(){
		if(slotRightHand){
			slotRightHand.transform.parent = handRight;
			slotRightHand.transform.localPosition = Vector3.zero;
			slotRightHand.transform.localRotation = Quaternion.identity;
		}
		
		if(slotLeftHand){
			slotLeftHand.transform.parent = handLeft;
			slotLeftHand.transform.localPosition = Vector3.zero;
			slotLeftHand.transform.localRotation = Quaternion.identity;
		}
	}
	
	protected void collisionEnter(Collision collision){
		state = state.handleCollissionEnter(collision,_BThirdPerson, stats);
	}
	protected void collisionStay(Collision collision){
		state = state.handleCollissionStay(collision,_BThirdPerson, stats);
	}
	protected void triggerEnter(Collider collider){
		state = state.handleTriggerEnter(collider,_BThirdPerson, stats);
	}
	protected void triggerStay(Collider collider){
		state = state.handleTriggerStay(collider,_BThirdPerson, stats);
	}
	protected void triggerExit(Collider collider){
		state = state.handleTriggerExit(collider,_BThirdPerson, stats);
	}
	
	public virtual void  onHit(OStroke stroke){
	}
	
	public virtual void onDie(){
	}
	
}
