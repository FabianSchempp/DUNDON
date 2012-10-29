using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]
public class BThirdPerson : MonoBehaviour {
	//EXPOSED
	public GameObject attentionTester;
	public float runAcceleration = 100;
	public float maxRunSpeed = 10;
	public float rotationSpeed = 5;
	public float jumpStrength = 250;
	public float attachSpeed = 1f;
	public Transform[] referenceTransforms;
	
	
	[HideInInspector]
	public List<Vector3> referencePoints = new List<Vector3>();
	//
	
	private float attachFactor = 0;
	
	private Quaternion lookRotation;
	private bool _isFalling = false;
	private bool _isJumping = false;
	private bool _isStill = false;
	private bool _isGrounded = false;
	private bool _isSolidOverHead = false;
	private bool _isOnWall = false;
	private string _groundTag = "";
	private Vector3 _groundPoint = Vector3.zero;
	private Vector3 _wallNormal = Vector3.zero;
	private Vector3 _lastValidGroundPoint = Vector3.zero;
	
	private string _wallTag = "";
	private float _speed;
	private GameObject _attachObject;
	private float _stateTime;
	private float colliderHeight;
	private Vector3 _position;
	private Transform _t;

	private Controller _controller;
	public Controller controller{
		get{return _controller;}
		set{_controller = value;}
	}
	public float stateTime{
		get{return _stateTime;}
		set{_stateTime = value;}
	}
	public bool isFalling{
		get{return _isFalling;}
	}
	public bool isStill{
		get{
			if(_speed < 0.1f){
				return true;
			}
			else{
				return false;
			}
			}
	}
	
	public Vector3 position{
		get{return _position;}
	}
	public Transform t{
		get{return _t;}
	}
	
	public bool isJumping{
		get{return _isJumping;}
	}
	public bool isGrounded{
		get{return _isGrounded;}
	}
	public bool isSolidOverHead{
		get{return _isSolidOverHead;}
	}
	public bool isOnWall{
		get{return _isOnWall;}
	}
	public float speed{
		get{return _speed;}
	}
	public string groundTag{
		get{return _groundTag;}
		set{_groundTag= value;}
	}
	public Vector3 groundPoint{
		get{return _groundPoint;}
		set{_groundPoint= value;}
	}
	public Vector3 wallNormal{
		get{return _wallNormal;}
		set{_wallNormal= value;}
	}
	public Vector3 lastValidGroundPoint{
		get{return _lastValidGroundPoint;}
		set{_lastValidGroundPoint = value;}
	}
	public Vector3 getReferencePoints(int i){
		return _t.rotation * referencePoints[i];
	}
	public GameObject attachObject{
		get{return _attachObject;}
		set{_attachObject= value;}
	}
	public float distanceToReferenceObject{
		get{return Mathf.Abs((attachObject.transform.position - _t.position).z);}
	}
	
	void Awake(){
		CapsuleCollider c = (CapsuleCollider) collider;
		colliderHeight = c.height;
		foreach(Transform t in referenceTransforms){
			Transform tp = t.parent;
			t.parent = null;
			referencePoints.Add(t.position - _t.position);
			t.parent = tp;
		}
		_t = transform;
	}
	void FixedUpdate () {
		_speed = rigidbody.velocity.magnitude;
		_position = _t.position;
		
		//test for isJumping and isFalling
		if(rigidbody.velocity.y < 0 && !isGrounded) {
			_isFalling = true;
			_isJumping = false;
		}
		else{
			_isFalling = false;
			_isJumping = true;
		}
		
		
		//test for isGrounded
		_groundPoint = _position;
		_groundTag = Helper.raycastTestGround(ref _groundPoint,collider.bounds.size.y/2);
		
		if(groundTag == "Solid"){
			_lastValidGroundPoint = _groundPoint;
		}
		
		if(_groundTag != ""){
			_isGrounded = true;
		}
		else{
			_isGrounded = false;
		}
		
		_isSolidOverHead = Helper.raycastTestOverHead(_position, 2.3f);
		//test for isWall
		_wallTag = Helper.raycastTestWall(_position,ref _wallNormal,collider.bounds.size.x,_t.forward);
		if(_wallTag != ""){
			_isOnWall = true;
		}
		else{
			_isOnWall = false;
		}
	}
	
	public void jumpSoft(){
		rigidbody.AddForce(Vector3.up * jumpStrength);
		rigidbody.isKinematic = false;
		rigidbody.useGravity = true;
	}
	public void jumpHard(){
		rigidbody.AddForce(Vector3.up * jumpStrength,ForceMode.Impulse);
		rigidbody.isKinematic = false;
		rigidbody.useGravity = true;
	}
	public void jumpSoft(float force){
		rigidbody.AddForce(Vector3.up * force);
		rigidbody.useGravity = true;
	}
	public void drop(){
		rigidbody.useGravity = true;
	}
	public void stop(){
		rigidbody.velocity = Vector3.zero;
		rigidbody.useGravity = false;
	}
	public void attachTo(GameObject go,float time){
		attachFactor = Mathf.Clamp01(time * attachSpeed);
		rigidbody.MovePosition(Vector3.Lerp(rigidbody.position, go.transform.position, attachFactor));
		collider.enabled = false;
		rigidbody.useGravity = false;
	}
	public void attachTo(GameObject go, Vector3 offset,float time){
		attachFactor = Mathf.Clamp01(time);
		rigidbody.MovePosition(Vector3.Lerp(rigidbody.position, go.transform.position - offset, attachFactor));
		collider.enabled = false;
		rigidbody.useGravity = false;
		rigidbody.isKinematic = true;
	}
	public void attachTo(Vector3 go, Vector3 offset,float time){
		attachFactor = Mathf.Clamp01(time);
		rigidbody.MovePosition(Vector3.Lerp(rigidbody.position, go - offset, attachFactor));
		collider.enabled = false;
		rigidbody.useGravity = false;
		rigidbody.isKinematic = true;
	}
	public void attachTo(Vector3 go, Quaternion rotation, Vector3 offset,float time){
		attachFactor = Mathf.Clamp01(time);
		rigidbody.MovePosition(Vector3.Lerp(rigidbody.position, go - offset, attachFactor));
		rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation, rotation, attachFactor);
		collider.enabled = false;
		rigidbody.useGravity = false;
		rigidbody.isKinematic = true;
	}
	public void alignX(GameObject go){
		Vector3 target = _t.position;
		Vector3 transformedTarget = go.transform.InverseTransformPoint(target);
		transformedTarget.x = 0;
		target = go.transform.TransformPoint(transformedTarget);
		rigidbody.MovePosition(Vector3.Lerp(_t.position, target, Time.deltaTime));
	}
	public void wallSlide(){
		rigidbody.AddForce(Vector3.up * -2f);
	}
	public void hitBack(float force){
		rigidbody.AddForce(_t.forward * -force * Time.deltaTime * 50,ForceMode.Impulse);
	}
	public void hitBack(Vector3 force, GameObject center){
		rigidbody.AddForce(force * 50,ForceMode.Impulse);
	}
	public void turn(Vector3 target){
		if (Vector3.Distance(target, _t.position) >= 0.2f && target != Vector3.zero) {
			lookRotation = Quaternion.LookRotation(Helper.floorVector3Normalized(target,_t.position));
			rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation, lookRotation, rotationSpeed * Time.deltaTime);
		}
	}
	public void attachRotation(Quaternion rotation, float factor){
		rigidbody.rotation = Quaternion.Lerp(rigidbody.rotation,rotation,factor);
	}
	public void run(float s){	
		Vector3 targetVelocity = _t.forward * runAcceleration * s;
	  	Vector3 velocity = rigidbody.velocity;
	   	Vector3 velocityChange = (targetVelocity - velocity);
	   	velocityChange.x = Mathf.Clamp(velocityChange.x, -maxRunSpeed, maxRunSpeed);
	  	velocityChange.z = Mathf.Clamp(velocityChange.z, -maxRunSpeed, maxRunSpeed);
	   	velocityChange.y = 0;
	   	rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
	
		rigidbody.useGravity = true;
	}
	public bool runToReferenceObject(float s){
		if(attachObject){
			turn(attachObject.transform.position);
			run(s);
			return true;
		}
		else{
			return false;
		}
	}
	public bool turnToReferenceObject(){
		if(attachObject){
			turn(attachObject.transform.position);
			return true;
		}
		else{
			return false;
		}
	}
	public void reactivateCollider(){
		collider.enabled = true;
		rigidbody.useGravity = true;
		rigidbody.isKinematic = false;
	}
	public void setCollider(float height){
		CapsuleCollider c = (CapsuleCollider) collider;
		if(c.height != height){
			c.height = height;
			c.center = new Vector3(0,height/2 - 0.05f,0);
		}
	}
	public void resetCollider(){
		CapsuleCollider c = (CapsuleCollider) collider;
		c.height = colliderHeight;
		c.center = new Vector3(0,colliderHeight/2 - 0.05f ,0);
	}
	public GameObject getAttention(){
		GameObject a = attentionTester.GetComponent<OAttentionRadius>().attentionObject;
		if(a) attachObject = a;
		return a;
	}
	public bool isInAttackDistance(float distance){
		if(Vector3.Distance(_t.position,attachObject.transform.position) < distance){
			return true;
		}
		else{ 
			return false;
		}
	}
}

