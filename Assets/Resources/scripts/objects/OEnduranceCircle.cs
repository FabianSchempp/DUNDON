using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OEnduranceCircle: MonoBehaviour {
	
	void Awake() {
		animation["Default Take"].speed = 0;
	}
	
	void Update () {
		animation["Default Take"].normalizedTime = ControllerKnight.KnightStats.enduranceFactor;
		
	}
}
