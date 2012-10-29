using UnityEngine;
using System.Collections;

public class BKillRigidbodyAtSleep : MonoBehaviour {

	void Update () {
		if(rigidbody.IsSleeping()){
			Destroy(gameObject.rigidbody);
			Destroy(gameObject.GetComponent<BoxCollider>());
			Destroy(gameObject.GetComponent<BKillRigidbodyAtSleep>());
		}
	}
}
