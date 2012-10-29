using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (BThirdPerson))]
public class ControllerKnight : Controller {
	
	private float _awarenessRadius = 0;
	public float _awarenessSpeed = 1;
	
	public static ControllState _KnightIDLE;
	public static ControllState _KnightIDLEONITEM;
	public static ControllState _KnightIDLEONFIRE;
	public static ControllState _KnightRUN;
	public static ControllState _KnightWALK;
	public static ControllState _KnightSLIDE;
	public static ControllState _KnightJUMP;
	public static ControllState _KnightFALL;
	public static ControllState _KnightWAIT;
	public static ControllState _KnightDIE;
	public static ControllState _KnightATTACK;
	public static ControllState _KnightATTACKB;
	public static ControllState _KnightATTACKC;
	public static ControllState _KnightSIT;
	public static ControllState _KnightSTANDUP;
	public static ControllState _KnightGETHIT;
	public static CharacterInventory KnightInventory;
	public static CharacterStats KnightStats;
	public static Vector3 KnightPosition;

	void Start () {
		//setup Controllstates
		_KnightIDLE = new KnightIDLE();
		_KnightIDLEONFIRE = new KnightIDLEONFIRE();
		_KnightRUN = new KnightRUN();
		_KnightSLIDE = new KnightSLIDE();
		_KnightJUMP = new KnightJUMP();
		_KnightFALL = new KnightFALL();
		_KnightWAIT = new KnightWAIT();
		_KnightDIE = new KnightDIE();
		_KnightATTACK = new KnightATTACK();
		_KnightATTACKB = new KnightATTACKB();
		_KnightATTACKC = new KnightATTACKC();
		_KnightSIT = new KnightSIT();
		_KnightSTANDUP = new KnightSTANDUP();
		_KnightGETHIT = new KnightGETHIT();
		
		state = _KnightSIT;
		KnightInventory = gameObject.GetComponent<CharacterInventory>();
		KnightStats = stats;
		Initialize();
		stats.healthMax = 5;
		stats.healthCurrent = 5;
		stats.enduranceMax = 100;
		stats.enduranceCurrent = 50;
	}
	public override void onHit(OStroke stroke){
		if(stroke){
			stats.reduceHealthImpact(stroke.strength);
		}
		else{
			stats.reduceHealthImpact(5f);
		}	
	}
	
	public override void onDie(){
		Application.LoadLevel(0);
	}
	
	void FixedUpdate () {
		UpdateState();
		doCommon();
	}
	
	void doCommon(){
		KnightPosition = transform.position;
		updateAwarenss();
		Shader.SetGlobalVector("_characterPos",transform.position);
	}
	void updateAwarenss(){
		if(_BThirdPerson.isStill){
			_awarenessRadius = Mathf.Min(_awarenessRadius + _awarenessSpeed * Time.deltaTime, 50);
		}
		else{
			_awarenessRadius = Mathf.Max(_awarenessRadius - (_awarenessSpeed*3) * Time.deltaTime,0);
		}
		
		AwarenessManager.updateAwareness(transform.position, _awarenessRadius);
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

