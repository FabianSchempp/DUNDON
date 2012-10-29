using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OSkySign : MonoBehaviour {
	public static List<GameObject> Stars = new List<GameObject>();
	public static GameObject SkyCursor;
	public static float skyHight = 432;
	
	void Awake(){
		OSkySign.SkyCursor = (GameObject)Resources.Load("prefabs/GUI/sky_sign_a");
		OSkySign.placeStar(transform.position);
	}
	
	public static GameObject placeStar(Vector3 pos){
		GameObject sky_cursor = (GameObject) Instantiate(SkyCursor);
		sky_cursor.name = "sky_cursor";
		sky_cursor.layer = 23;
		sky_cursor.transform.localScale = Vector3.one * 20;
		sky_cursor.transform.position = Helper.floorVector3(pos) + Vector3.up * skyHight;
		Stars.Add(sky_cursor);
		return sky_cursor;
	}
}
