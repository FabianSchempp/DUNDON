using UnityEngine;
using System.Collections;

public class BShake : MonoBehaviour {
	private AnimationCurve shakeCurveSin = new AnimationCurve(new Keyframe[3]{
															new Keyframe(0,1),
															new Keyframe(0.5f, -1),
															new Keyframe(1,1)
															});
	private AnimationCurve shakeCurveCos = new AnimationCurve(new Keyframe[3]{
															new Keyframe(0,-1),
															new Keyframe(0.5f, 1),
															new Keyframe(1,-1)
															});
	private float shakeStrength = 0;
	private float shakeFrequency = 10f;
	private Vector3 originalPosition;
	
	void Start () {
		shakeCurveSin.preWrapMode = WrapMode.Loop;
		shakeCurveSin.postWrapMode = WrapMode.Loop;
		shakeCurveCos.preWrapMode = WrapMode.Loop;
		shakeCurveCos.postWrapMode = WrapMode.Loop;
	}
	
	void FixedUpdate () {
			//shake
			if(shakeStrength > 0){
				Vector3 shakeVector = Vector3.zero;
				shakeStrength -= Time.deltaTime*3;
				shakeVector.z += shakeCurveCos.Evaluate(Time.timeSinceLevelLoad * shakeFrequency)*shakeStrength;
				shakeVector.y += shakeCurveSin.Evaluate(Time.timeSinceLevelLoad * shakeFrequency)*shakeStrength;
				transform.position = originalPosition + shakeVector;
			}
	}
	
	public void shake(float frequency, float strength){
		originalPosition = transform.position;
		shakeStrength = strength;
		shakeFrequency = frequency;
	}
}

public class Shake{
	public static  AnimationCurve shakeCurveSin = new AnimationCurve(new Keyframe[3]{
															new Keyframe(0,1),
															new Keyframe(0.5f, -1),
															new Keyframe(1,1)
															});
	public static AnimationCurve shakeCurveCos = new AnimationCurve(new Keyframe[3]{
															new Keyframe(0,-1),
															new Keyframe(0.5f, 1),
															new Keyframe(1,-1)
															});
	void Start () {
		shakeCurveSin.preWrapMode = WrapMode.Loop;
		shakeCurveSin.postWrapMode = WrapMode.Loop;
		shakeCurveCos.preWrapMode = WrapMode.Loop;
		shakeCurveCos.postWrapMode = WrapMode.Loop;
	}
	
	public static Vector3 shakePosition(Vector3 originalPosition, float strenght, float frequency){
				Vector3 shakeVector = Vector3.zero;
				shakeVector.z += shakeCurveCos.Evaluate(Time.timeSinceLevelLoad * frequency)*strenght;
				shakeVector.y += shakeCurveSin.Evaluate(Time.timeSinceLevelLoad * frequency)*strenght;
				return originalPosition + shakeVector;
	}
}