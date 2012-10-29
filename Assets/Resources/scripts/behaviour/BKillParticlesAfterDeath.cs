using UnityEngine;
using System.Collections;

public class BKillParticlesAfterDeath : MonoBehaviour {

	void Update () {
		if(!particleSystem.IsAlive()){
			Destroy(gameObject);
		}
	}
}
