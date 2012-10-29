using UnityEngine;
using System.Collections;

public class BFollow : MonoBehaviour {
	
	public Transform parent;
	public Vector3 offset;
	public bool deparent;
	
	void Start(){
		if(deparent){
			transform.parent = null;
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 target = Vector3.zero;
		Vector3 parentPosition = parent.transform.position;
		Vector3 selfPosition = transform.position;
		
		target.x = parent.position.x;
		target.y = parent.position.y;
		target.z = parent.position.z ;
		
		transform.position = target + offset;
	}
}
