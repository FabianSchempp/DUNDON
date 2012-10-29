using UnityEngine;
using System.Collections;

public class glowONE : MonoBehaviour {
	
	Material m;
	public float cycleSpeed = 1;
	public float cycleOffset = 0;
	public float cycleLength = 1;
	public float alpha = 0.3f;
	void Start () {
		m = renderer.material;
	}
	
	void Update () {
		
		float cycleTimeA = Mathf.Repeat(Time.timeSinceLevelLoad*cycleSpeed ,cycleLength) + cycleOffset;
		
		float alphaA = (cycleLength - cycleTimeA) * alpha;
		
		m.SetFloat("_CycleA",cycleTimeA);
		m.SetFloat("_AlphaA",alphaA);
	}
}
