using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OSocketHealthbar: MonoBehaviour {
	
	private AnimationCurve bumpCurve = new AnimationCurve(new Keyframe[3]{
															new Keyframe(0,0.04242746f),
															new Keyframe(0.5f, 0.08f),
															new Keyframe(1,0.04242746f)
															});
	
	List<GameObject> sockets = new List<GameObject>();
	List<GameObject> lives = new List<GameObject>();
	List<Vector3> positions = new List<Vector3>();
	
	void Awake() {
		foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()){
			if(t.name.Contains("socket")){
				sockets.Add(t.gameObject);
				positions.Add(t.transform.position);
			}
			if(t.name.Contains("live")){
				lives.Add(t.gameObject);
			}
		}
		
		bumpCurve.postWrapMode = WrapMode.Loop;
		bumpCurve.preWrapMode = WrapMode.Loop;
	}
	
	float lastMaxHealth = 0;
	float lastHealth = 0;
	
	void Update () {
		if(ControllerKnight.KnightStats.healthMax != lastMaxHealth){
			for (int i = 0; i < 32; i++){
				if(i <= ControllerKnight.KnightStats.healthMax){
					sockets[i].active = true;
				}
				else{
					sockets[i].active = false;
				}
			}
		}
		if(ControllerKnight.KnightStats.healthCurrent != lastHealth){
			for (int i = 0; i < 32; i++){
				if(i <= ControllerKnight.KnightStats.healthCurrent){
					lives[i].active = true;
				}
				else{
					lives[i].active = false;
				}
				lives[i].transform.localScale = Vector3.one * 0.04242746f;
			}
		}
		lastMaxHealth = ControllerKnight.KnightStats.healthMax;
		lastHealth = ControllerKnight.KnightStats.healthCurrent;
		
		lives[(int)ControllerKnight.KnightStats.healthCurrent].transform.localScale = Vector3.one * bumpCurve.Evaluate(Time.timeSinceLevelLoad);
	}
}
