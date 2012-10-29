using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (BThirdPerson))]
public class ControllerZombie : Controller {

	public static ControllState _ZombieIDLE;
	public static ControllState _ZombieIDLEDEATH;
	public static ControllState _ZombieSPAWN;
	public static ControllState _ZombieRUN;
	public static ControllState _ZombieWALK;
	public static ControllState _ZombieSLIDE;
	public static ControllState _ZombieJUMP;
	public static ControllState _ZombieFALL;
	public static ControllState _ZombieWAIT;
	public static ControllState _ZombieDIE;
	public static ControllState _ZombieATTACK;
	public static ControllState _ZombieATTACKB;
	public static ControllState _ZombieATTACKC;
	public static ControllState _ZombieGETHIT;
	
	void Start () {
		//setup Controllstates
		_ZombieIDLEDEATH = new ZombieIDLEDEATH();
		_ZombieSPAWN = new ZombieSPAWN();
		_ZombieIDLE = new ZombieIDLE();
		_ZombieRUN = new ZombieRUN();
		_ZombieSLIDE = new ZombieSLIDE();
		_ZombieJUMP = new ZombieJUMP();
		_ZombieFALL = new ZombieFALL();
		_ZombieWAIT = new ZombieWAIT();
		_ZombieDIE = new ZombieDIE();
		_ZombieATTACK = new ZombieATTACK();
		_ZombieATTACKB = new ZombieATTACKB();
		_ZombieATTACKC = new ZombieATTACKC();
		_ZombieGETHIT = new ZombieGETHIT();
		state = _ZombieIDLEDEATH;
		Initialize();
		
		stats.healthMax = 3;
		stats.healthCurrent = 3;
		stats.enduranceMax = 100;
		stats.enduranceCurrent = 50;
	}
	public override void onHit(OStroke stroke){
		ParticleManager.spawnParticles("sparks",transform.position);
		stats.reduceHealth(stroke.strength);
	}
	
	public override void onDie(){
		OScoreBar.AddScore(100);
		BExplode e = GetComponent<BExplode>();
		if(e) e.explode();
		foreach(GameObject g in slots){
			GameObject gx = (GameObject) GameObject.Instantiate(g,transform.position,transform.rotation);
			gx.rigidbody.AddExplosionForce(1.5f,transform.position,3);
		}
	}
	
	void FixedUpdate () {
		UpdateState();
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

