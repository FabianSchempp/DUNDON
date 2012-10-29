using UnityEngine;
using System.Collections;

public class ControllerCamera : MonoBehaviour {
	
	public GameObject player;
	public Vector2 fov = new Vector2(75, 20);
	public Vector3 distanceToCharacter = new Vector3(5,-2,-10);
	public Vector2 minMaxAngle = new Vector2(-1.6f,1.3f);
	public Vector3 cameraPositionOffset = new Vector3(0,0,0);
	public float startRotationOffset = 0;
	public float cameraFollowSpeed = 5f;
	public float cameraOrbitSpeed = 10f;
	
	public AnimationCurve offCenterRotationFalloff = new AnimationCurve(new Keyframe[5]
                                       {new Keyframe(-1,-10f),
										new Keyframe(0.5f,-2f),
                                        new Keyframe(0f,0f),
										new Keyframe(0.5f,2f),
                                        new Keyframe(1,10f)});
	
	public static AnimationCurve OffCenterRotationFalloff;
	
	public static Vector2 MinMaxAngle;
	public static float CameraOrbitSpeed;
	public static float CameraFollowSpeed;
	public static Vector3 DistanceToCharacter;
	public static Vector3 CameraPositionOffset;
//	private float _distanceModifier = 0;
	public static GameObject CameraTarget;
	public static Vector3 hingeUpPosition;
	public static GameObject hingeRight;
	public static GameObject hingeUp;
	
	private float _referenceScale = 0;
	
	public static float shakeStartTime = 0;
	public static Vector3 shakeStrength = Vector3.zero;
	public static Vector3 shakeFrequency = Vector3.one;
	public static float shakeDuration = 0;
	public static AnimationCurve shake_curve_x;
	public static  AnimationCurve shake_curve_y;
	public static  AnimationCurve shake_curve_z;
	
	public static GameObject _alternativeTarget;
	public static GameObject _alternativePosition;
	public static float _lookAtStart = 0;
	public static float _lookAtSpeed = 1;
	public static float _lookAtDuration = 0;
	public static Vector2 lastInputAxis;
	public static Quaternion _targetRotationUP;
	public static Quaternion _targetRotationRIGHT;
	
	public static CameraState _CameraOFFCENTERROTATION;
	public static CameraState _CameraLOOKAT;
	public static CameraState _CameraLOOKATWITHPOSITION;
	public static CameraState _CameraFORWARD;
	
	public static float stateTime = 0;
	private static  CameraState _state;
	public static CameraState state{
		get{return _state;}
		set{
			if(value != state){
				stateTime = 0;
			}
			_state = value;
			}
	}
	
	private Transform _t;
	private Transform _tHU;
	private Transform _tHR;
	
	void Awake () {
		InitializeShake();
		InitializeStates();
		Initialize();
	}
	void Initialize () {
		//get target
		CameraTarget = player;
		_t = transform;
		
		//create the hinge for the camera and parent the camera under the hinge
		hingeUp = new GameObject();
		hingeUp.transform.position = CameraTarget.transform.position + hingeUp.transform.rotation * cameraPositionOffset;
		hingeUpPosition = hingeUp.transform.position;
		hingeUp.name = "hinge_up";
		_tHU = hingeUp.transform;

		
		hingeRight = new GameObject();
		hingeRight.transform.position = _tHU.position;
		hingeRight.name = "hinge_right";
		hingeRight.transform.parent = _tHU;
		hingeRight.transform.Rotate(Vector3.right, 30,Space.Self);
		_tHR = hingeRight.transform;

		
		transform.parent = _tHR;

		//tag gameobject
		gameObject.tag = "Camera Helper";
		
		//position camera
		transform.position = _tHU.position;
		transform.transform.Translate(_tHU.forward * distanceToCharacter.z,Space.World);
		
		//rotate
		transform.rotation = Quaternion.LookRotation(_tHU.position-transform.position);
		
		_targetRotationUP = _tHU.rotation;
		_targetRotationRIGHT = _tHR.localRotation;
		
	}
	void InitializeStates(){
		_CameraOFFCENTERROTATION = new CameraOFFCENTERROTATE();
		_CameraLOOKAT = new CameraLOOKAT();
		_CameraLOOKATWITHPOSITION = new CameraLOOKATWITHPOSITION();
		state = _CameraOFFCENTERROTATION;
	}
	void InitializeShake(){
		shake_curve_x = new AnimationCurve(new Keyframe[3]
                                       {new Keyframe(0,-1f), 
                                        new Keyframe(0.5f,1f), 
                                        new Keyframe(1,-1f)});
	
		shake_curve_y = new AnimationCurve(new Keyframe[3]
                                       {new Keyframe(0,-1f), 
                                        new Keyframe(0.65f,1f), 
                                        new Keyframe(1.3f,-1f)});
	
		shake_curve_z = new AnimationCurve(new Keyframe[3]
                                       {new Keyframe(0,-1f), 
                                        new Keyframe(0.85f,1f), 
                                        new Keyframe(1.7f,-1f)});
		shake_curve_x.postWrapMode = WrapMode.Loop;
		shake_curve_x.preWrapMode = WrapMode.Loop;
		shake_curve_y.postWrapMode = WrapMode.Loop;
		shake_curve_y.preWrapMode = WrapMode.Loop;
		shake_curve_z.postWrapMode = WrapMode.Loop;
		shake_curve_z.preWrapMode = WrapMode.Loop; 	
	}
	
	void FixedUpdate () {
		MinMaxAngle = minMaxAngle;
		CameraOrbitSpeed = cameraOrbitSpeed;
		CameraFollowSpeed = cameraFollowSpeed;
		DistanceToCharacter = distanceToCharacter;
		CameraPositionOffset = cameraPositionOffset;
		OffCenterRotationFalloff = offCenterRotationFalloff;
		_referenceScale = transform.lossyScale.y;
		stateTime += Time.deltaTime;
		state = state.ControllCamera(_t,_tHU, _tHR , calculateShake(), stateTime);
	}
	
	public Vector3 calculateShake(){
			Vector3 shake = new Vector3(shake_curve_x.Evaluate(Time.timeSinceLevelLoad * shakeFrequency.x),
										shake_curve_y.Evaluate(Time.timeSinceLevelLoad * shakeFrequency.y),
										shake_curve_z.Evaluate(Time.timeSinceLevelLoad * shakeFrequency.z));
			Vector3 shakeScale = shakeStrength * Mathf.Clamp01(shakeStartTime + shakeDuration - Time.timeSinceLevelLoad);
			return Vector3.Scale(shake,shakeScale);
	}
	public static void lookAt(GameObject g, float duration, float speed){
		_alternativeTarget = g;
		_lookAtDuration = duration;
		_lookAtSpeed = speed;
		ControllerCamera.state = ControllerCamera._CameraLOOKAT;
	}
	public static void lookAtWithPosition(GameObject g, GameObject p, float duration, float speed){
		_alternativeTarget = g;
		_alternativePosition = p;
		_lookAtDuration = duration;
		_lookAtSpeed = speed;
		ControllerCamera.state = ControllerCamera._CameraLOOKATWITHPOSITION;
	}
	public static void shakeCamera(float duration,Vector3 frequency, Vector3 strength){
		shakeStartTime = Time.timeSinceLevelLoad;
		shakeStrength = strength;
		shakeDuration = duration;
		shakeFrequency = frequency;
	}
	public static void shakeCamera(float duration,float frequency, float strength){
		shakeStartTime = Time.timeSinceLevelLoad;
		shakeStrength = Vector3.one * strength;
		shakeDuration = duration;
		shakeFrequency = Vector3.one * frequency;
	}
}
public class CameraDRAGROTATE : CameraState {
	public override CameraState ControllCamera(Transform c, Transform hingeUp, Transform hingeRight, Vector3 offset, float stateTime){
	//get input right mouse
	Vector2 inputAxis = Vector2.zero;
	if (Input.GetButton("rotate_camera")){
		//input for mouse
		inputAxis.x = Input.GetAxis("mouse_x")*0.002f;
		inputAxis.y = (Input.GetAxis("mouse_y") + Input.GetAxis("mouse_wheel"))*0.002f;
	}
	
	//input for controller
		inputAxis.x += Input.GetAxis("rotate_camera_horizontal");
		inputAxis.y += Input.GetAxis("rotate_camera_vertical");
	

	//multiply input by speed make vertical speed a bit slower
	Vector2 orbitSpeed = Vector2.zero;
	orbitSpeed.x = Mathf.Clamp(inputAxis.x * ControllerCamera.CameraOrbitSpeed * Time.deltaTime, -80, 80);
	orbitSpeed.y = Mathf.Clamp(inputAxis.y * ControllerCamera.CameraOrbitSpeed * Time.deltaTime, -80, 80)*-0.75f;

	//update and restore_hinge_position
	ControllerCamera.hingeUpPosition = Vector3.Lerp(ControllerCamera.hingeUpPosition, ControllerCamera.CameraTarget.transform.position, ControllerCamera.CameraFollowSpeed*Time.deltaTime);
	hingeUp.position = ControllerCamera.hingeUpPosition + hingeUp.rotation * ControllerCamera.CameraPositionOffset;
	
	//save hingeRight for min max check
	Quaternion hingeRightRotationTMP = hingeRight.localRotation;
	
	ControllerCamera._targetRotationRIGHT *= Quaternion.AngleAxis(orbitSpeed.y,Vector3.right);
	ControllerCamera._targetRotationUP  *= Quaternion.AngleAxis(orbitSpeed.x,Vector3.up);
		
	hingeUp.rotation = Quaternion.Lerp(hingeUp .transform.rotation,ControllerCamera._targetRotationUP,ControllerCamera.CameraOrbitSpeed * Time.deltaTime);
	hingeRight.localRotation  = Quaternion.Lerp(hingeRight.localRotation,ControllerCamera._targetRotationRIGHT,ControllerCamera.CameraOrbitSpeed * Time.deltaTime);
	
	float hingeRightRotation = Mathf.Sin(hingeRight.localRotation.eulerAngles.x*Mathf.Deg2Rad);
	float hingeRightRotationFactor = Mathf.Abs((hingeRightRotation - ControllerCamera.MinMaxAngle.y) /( ControllerCamera.MinMaxAngle.x -  ControllerCamera.MinMaxAngle.y));
	//restrict rotation to limits
	if(hingeRightRotation >  ControllerCamera.MinMaxAngle.x || hingeRightRotation <  ControllerCamera.MinMaxAngle.y){
		hingeRight.localRotation = hingeRightRotationTMP;
	}
	
	//test for collsion
	Ray testRay = new  Ray(hingeUp.position,-hingeRight.forward);
	RaycastHit testHit;
	int mask = 1<<8;
	Physics.Raycast(testRay,out testHit,1000.0f,mask);

	//translate camera based on vertical rotation
	Vector3 zoomNormalPosition = hingeUp.position + (hingeRight.forward *  ControllerCamera.DistanceToCharacter.y);
	Vector3 zoomClosePosition = hingeUp.position + (hingeRight.forward *  ControllerCamera.DistanceToCharacter.z);
	Vector3 rayTestPosition = hingeUp.position + (hingeRight.forward * (-testHit.distance + 0.5f)); 

	
	Vector3 assamledPosition = Vector3.Lerp(zoomNormalPosition, zoomClosePosition, hingeRightRotationFactor) + offset;
	if(testHit.collider && Vector3.Distance(hingeRight.position, assamledPosition) > testHit.distance){
		assamledPosition = rayTestPosition;
	}
	//set camera to lerped targets
	c.transform.position = assamledPosition;
	return this;
	}
}
public class CameraOFFCENTERROTATE : CameraState {
	public override CameraState ControllCamera(Transform c, Transform hingeUp, Transform hingeRight, Vector3 offset, float stateTime){
	
	float factor = Mathf.Clamp01(stateTime * 0.5f);
	//get input right mouse
	Vector3 inputAxis = Vector2.zero;
	
//	inputAxis = (Camera.main.ScreenToViewportPoint(Input.mousePosition) - new Vector3(0.5f,0.5f,0.5f)) * 2;
	inputAxis = Vector2.zero;
	inputAxis.x = ControllerCamera.OffCenterRotationFalloff.Evaluate(Input.GetAxis("Horizontal"));
//	inputAxis.y =  0;
	//multiply input by speed make vertical speed a bit slower
	Vector2 orbitSpeed = Vector2.zero;
	orbitSpeed.x = Mathf.Clamp(inputAxis.x * ControllerCamera.CameraOrbitSpeed * Time.deltaTime, -80, 80);
//	orbitSpeed.y = Mathf.Clamp(inputAxis.y * ControllerCamera.CameraOrbitSpeed * Time.deltaTime, -80, 80)*-0.75f;
	
	//update and restore_hinge_position
	ControllerCamera.hingeUpPosition = Vector3.Lerp(ControllerCamera.hingeUpPosition, ControllerCamera.CameraTarget.transform.position, ControllerCamera.CameraFollowSpeed*Time.deltaTime);
	hingeUp.position = ControllerCamera.hingeUpPosition + (hingeUp.rotation * ControllerCamera.CameraPositionOffset* factor);
	
	//save hingeRight for min max check
//	Quaternion hingeRightRotationTMP = hingeRight.localRotation;

//	ControllerCamera._targetRotationRIGHT *= Quaternion.AngleAxis(orbitSpeed.y,Vector3.right);
	ControllerCamera._targetRotationUP  *= Quaternion.AngleAxis(orbitSpeed.x,Vector3.up);
		
	hingeUp.rotation = Quaternion.Lerp(hingeUp .transform.rotation,ControllerCamera._targetRotationUP,ControllerCamera.CameraOrbitSpeed * Time.deltaTime);
//	hingeRight.localRotation  = Quaternion.Lerp(hingeRight.localRotation,ControllerCamera._targetRotationRIGHT,ControllerCamera.CameraOrbitSpeed * Time.deltaTime);
	hingeRight.localRotation  =  Quaternion.AngleAxis(45, Vector3.right);

//	float hingeRightRotation = Mathf.Sin(hingeRight.localRotation.eulerAngles.x*Mathf.Deg2Rad);
//	float hingeRightRotationFactor = Mathf.Abs((hingeRightRotation - ControllerCamera.MinMaxAngle.y) /( ControllerCamera.MinMaxAngle.x -  ControllerCamera.MinMaxAngle.y));

	//restrict rotation to limits
//	if(hingeRightRotation >  ControllerCamera.MinMaxAngle.x || hingeRightRotation <  ControllerCamera.MinMaxAngle.y){
//		hingeRight.localRotation = hingeRightRotationTMP;
//	}
	
	//test for collsion
	Ray testRay = new  Ray(hingeUp.position,-hingeRight.forward);
	RaycastHit testHit;
	int mask = 1<<8;
	Physics.Raycast(testRay,out testHit,1000.0f,mask);

	//translate camera based on vertical rotation
	Vector3 zoomNormalPosition = hingeUp.position + ((hingeRight.forward *  ControllerCamera.DistanceToCharacter.y) *factor);
//	Vector3 zoomClosePosition = hingeUp.position + ((hingeRight.forward *  ControllerCamera.DistanceToCharacter.z)*factor);
	Vector3 rayTestPosition = hingeUp.position + ((hingeRight.forward * (-testHit.distance + 0.5f))*factor); 

	
//	Vector3 assamledPosition = Vector3.Lerp(zoomNormalPosition, zoomClosePosition, hingeRightRotationFactor) + offset;
	Vector3 assamledPosition = zoomNormalPosition + offset;
	
	if(testHit.collider && Vector3.Distance(hingeRight.position, assamledPosition) > testHit.distance){
		assamledPosition = rayTestPosition;
	}
	//set camera to lerped targets
	c.transform.position = assamledPosition;
	return this;
	}
}
public class CameraLOOKAT: CameraState {
	public override CameraState ControllCamera(Transform c, Transform hingeUp, Transform hingeRight, Vector3 offset, float stateTime){
	//update and restore_hinge_position
	ControllerCamera.hingeUpPosition = Vector3.Lerp(ControllerCamera.hingeUpPosition, ControllerCamera.CameraTarget.transform.position, ControllerCamera.CameraFollowSpeed*Time.deltaTime);
	hingeUp.position = ControllerCamera.hingeUpPosition + hingeUp.rotation * ControllerCamera.CameraPositionOffset;
	
	//save hingeRight for min max check
	Quaternion hingeRightRotationTMP = hingeRight.localRotation;

	ControllerCamera._targetRotationUP =  Quaternion.LookRotation(Helper.floorVector3Normalized(ControllerCamera._alternativeTarget.transform.position,hingeUp.position),Vector3.up);
	ControllerCamera._targetRotationRIGHT =  Quaternion.LookRotation(Helper.PlaneVector3(ControllerCamera._alternativeTarget.transform.position,hingeUp.position),Vector3.up);
	
	hingeUp.rotation = Quaternion.Lerp(hingeUp.rotation,ControllerCamera._targetRotationUP ,ControllerCamera._lookAtSpeed*Time.deltaTime);
	hingeRight.localRotation = Quaternion.Lerp(hingeRight.localRotation,ControllerCamera._targetRotationRIGHT,ControllerCamera._lookAtSpeed*Time.deltaTime);

	
	float hingeRightRotation = Mathf.Sin(hingeRight.localRotation.eulerAngles.x*Mathf.Deg2Rad);
	float hingeRightRotationFactor = Mathf.Abs((hingeRightRotation - ControllerCamera.MinMaxAngle.y) /( ControllerCamera.MinMaxAngle.x -  ControllerCamera.MinMaxAngle.y));
	
	//restrict rotation to limits
	if(hingeRightRotation >  ControllerCamera.MinMaxAngle.x || hingeRightRotation <  ControllerCamera.MinMaxAngle.y){
		hingeRight.localRotation = hingeRightRotationTMP;
	}
	
	//test for collsion
	Ray testRay = new  Ray(hingeUp.position,-hingeRight.forward);
	RaycastHit testHit;
	int mask = 1<<8;
	Physics.Raycast(testRay,out testHit,1000.0f,mask);

	//translate camera based on vertical rotation
	Vector3 zoomNormalPosition = hingeUp.position + (hingeRight.forward *  ControllerCamera.DistanceToCharacter.y);
	Vector3 zoomClosePosition = hingeUp.position + (hingeRight.forward *  ControllerCamera.DistanceToCharacter.z);
	Vector3 rayTestPosition = hingeUp.position + (hingeRight.forward * (-testHit.distance + 0.5f)); 

	
	Vector3 assamledPosition = Vector3.Lerp(zoomNormalPosition, zoomClosePosition, hingeRightRotationFactor) + offset;
	if(testHit.collider && Vector3.Distance(hingeRight.position, assamledPosition) > testHit.distance){
		assamledPosition = rayTestPosition;
	}
	//set camera to lerped targets
	c.transform.position = assamledPosition;
	if(ControllerCamera.stateTime > ControllerCamera._lookAtDuration){
		return ControllerCamera._CameraOFFCENTERROTATION;
	}	
	return this;
	}
}
public class CameraLOOKATWITHPOSITION: CameraState {
	public override CameraState ControllCamera(Transform c, Transform hingeUp, Transform hingeRight, Vector3 offset, float stateTime){
	
	float factor = Mathf.Clamp01(1- stateTime * 0.5f);
	//update and restore_hinge_position
	ControllerCamera.hingeUpPosition = Vector3.Lerp(ControllerCamera.hingeUpPosition, ControllerCamera._alternativePosition.transform.position, ControllerCamera.CameraFollowSpeed*Time.deltaTime);
	hingeUp.position = ControllerCamera.hingeUpPosition + (hingeUp.rotation * ControllerCamera.CameraPositionOffset*factor);
	
	//save hingeRight for min max check
	Quaternion hingeRightRotationTMP = hingeRight.localRotation;

	ControllerCamera._targetRotationUP =  Quaternion.LookRotation(Helper.floorVector3Normalized(ControllerCamera._alternativeTarget.transform.position,hingeUp.position),Vector3.up);
	ControllerCamera._targetRotationRIGHT =  Quaternion.LookRotation(Helper.PlaneVector3(ControllerCamera._alternativeTarget.transform.position,hingeUp.position),Vector3.up);
	
	hingeUp.rotation = Quaternion.Lerp(hingeUp.rotation,ControllerCamera._targetRotationUP ,ControllerCamera._lookAtSpeed*Time.deltaTime);
	hingeRight.localRotation = Quaternion.Lerp(hingeRight.localRotation,ControllerCamera._targetRotationRIGHT,ControllerCamera._lookAtSpeed*Time.deltaTime);

	
	float hingeRightRotation = Mathf.Sin(hingeRight.localRotation.eulerAngles.x*Mathf.Deg2Rad);
	float hingeRightRotationFactor = Mathf.Abs((hingeRightRotation - ControllerCamera.MinMaxAngle.y) /( ControllerCamera.MinMaxAngle.x -  ControllerCamera.MinMaxAngle.y));
	
	//restrict rotation to limits
	if(hingeRightRotation >  ControllerCamera.MinMaxAngle.x || hingeRightRotation <  ControllerCamera.MinMaxAngle.y){
		hingeRight.localRotation = hingeRightRotationTMP;
	}
	
	//test for collsion
	Ray testRay = new  Ray(hingeUp.position,-hingeRight.forward);
	RaycastHit testHit;
	int mask = 1<<8;
	Physics.Raycast(testRay,out testHit,1000.0f,mask);

	//translate camera based on vertical rotation
	Vector3 zoomNormalPosition = hingeUp.position + ((hingeRight.forward *  ControllerCamera.DistanceToCharacter.y) * factor);
	Vector3 zoomClosePosition = hingeUp.position + ((hingeRight.forward *  ControllerCamera.DistanceToCharacter.z) * factor);
	
	Vector3 assamledPosition = Vector3.Lerp(zoomNormalPosition, zoomClosePosition, hingeRightRotationFactor);

	//set camera to lerped targets
	c.transform.position = assamledPosition;
	ControllerCamera._lookAtDuration -= Time.deltaTime;
	if(ControllerCamera._lookAtDuration <= 0){
		return ControllerCamera._CameraOFFCENTERROTATION;
	}	
	return this;
	}
}
