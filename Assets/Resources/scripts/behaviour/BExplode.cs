using UnityEngine;
using System.Collections;

public class BExplode : MonoBehaviour {
	
	public Transform root;
	
	public void explode () {
		foreach(Transform t in root.GetComponentsInChildren<Transform>()){
			t.parent = null;
			t.gameObject.AddComponent<CapsuleCollider>();
			t.gameObject.AddComponent<Rigidbody>();
			t.rigidbody.AddExplosionForce(3, root.transform.position,5);
			Destroy(t.gameObject,3);
		}
		Destroy(GetComponent<BThirdPersonMotionSystem>().root.animation);
		Destroy(GetComponent<BThirdPersonMotionSystem>());
		Destroy(GetComponent<Controller>());
		Destroy(this);
	}
}
