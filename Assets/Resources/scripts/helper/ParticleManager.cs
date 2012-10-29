using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour {

	public static Dictionary<string,GameObject> Particles = new Dictionary<string, GameObject>();
	public GameObject[] particles;
	
	
	void Awake() {
		foreach(GameObject g in particles){
			Particles.Add(g.name, g);
		}
	}
	
	public static void spawnParticles(string key, Vector3 position){
		GameObject.Instantiate(ParticleManager.Particles[key],position,Quaternion.identity);
	}
}
