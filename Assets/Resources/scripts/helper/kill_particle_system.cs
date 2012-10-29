using UnityEngine;
using System.Collections;

public class kill_particle_system : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(!particleSystem.IsAlive()){
			Destroy(gameObject);
		}
	}
}
