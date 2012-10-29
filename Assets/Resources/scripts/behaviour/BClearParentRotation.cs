using UnityEngine;
using System.Collections;

public class BClearParentRotation : MonoBehaviour {
	
	void Update () {
		transform.rotation = Quaternion.LookRotation(Vector3.up);
	}
}
