using UnityEngine;
using System.Collections;

public class glowFIVE : MonoBehaviour {
	
	public AnimationCurve Curve = new AnimationCurve(new Keyframe[3]{
															new Keyframe(0,1),
															new Keyframe(0.5f, 0.25f),
															new Keyframe(1,1)
															});
	
	Material m;
	
	public float cycleOffset = 0;
	public float cycleSpeed = 1;
	public float cycleLength = 1;
	public float alpha = 0.3f;
	void Start () {
		m = renderer.material;
		Curve.preWrapMode = WrapMode.Loop;
		Curve.postWrapMode = WrapMode.Loop;
	}
	
	void Update () {
		
		float offset = cycleLength/5;
		
		float cycleTimeA = Curve.Evaluate(Time.timeSinceLevelLoad*cycleSpeed)*cycleLength;
		float cycleTimeB = Curve.Evaluate(Time.timeSinceLevelLoad*cycleSpeed + offset)*cycleLength;
		float cycleTimeC = Curve.Evaluate(Time.timeSinceLevelLoad*cycleSpeed + offset*2)*cycleLength;
		float cycleTimeD = Curve.Evaluate(Time.timeSinceLevelLoad*cycleSpeed + offset*3)*cycleLength;
		float cycleTimeE = Curve.Evaluate(Time.timeSinceLevelLoad*cycleSpeed + offset*4)*cycleLength;
		
		float alphaA = (cycleLength - cycleTimeA) * alpha;
		float alphaB = (cycleLength - cycleTimeB) * alpha;
		float alphaC = (cycleLength - cycleTimeC) * alpha;
		float alphaD = (cycleLength - cycleTimeD) * alpha;
		float alphaE = (cycleLength - cycleTimeE) * alpha;
		
		m.SetFloat("_CycleA",cycleTimeA);
		m.SetFloat("_AlphaA",alphaA);
		
		m.SetFloat("_CycleB",cycleTimeB);
		m.SetFloat("_AlphaB",alphaB);
		
		m.SetFloat("_CycleC",cycleTimeC);
		m.SetFloat("_AlphaC",alphaC);
		
		m.SetFloat("_CycleD",cycleTimeC);
		m.SetFloat("_AlphaD",alphaD);
		
		m.SetFloat("_CycleE",cycleTimeE);
		m.SetFloat("_AlphaE",alphaE);
		
//		Debug.Log(alphaA + " " + alphaB + " " + alphaC + " " + alphaD + " " + alphaE);
	}
}
