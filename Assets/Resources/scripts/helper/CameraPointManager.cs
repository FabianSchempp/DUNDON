using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraPointManager : MonoBehaviour {
	
	public static Dictionary<string,GameObject> Positions = new Dictionary<string, GameObject>();
	public static Dictionary<string,GameObject> Targets = new Dictionary<string, GameObject>();
	
	public GameObject[] positions;
	public GameObject[] targets;
	
	void Start () {
		foreach(GameObject p in positions){
			Positions.Add(p.name,p);
		}
		foreach(GameObject t in targets){
			Targets.Add(t.name,t);
		}
	}
}
