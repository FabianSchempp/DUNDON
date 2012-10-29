using UnityEngine;
using System.Collections;

public class OOasis : MonoBehaviour {

	public GameObject spawn_point;
	public GameObject spawn_camera;
	public AudioClip refresh_sound;
	
	public void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Player"){
		}
		else if (collider.gameObject.tag == "Enemy"){
//			collider.gameObject.GetComponent<monster_behaviour>().collected_water += 100; 
		}
	}
	
//	public void OnTriggerExit(Collider collider){
//		if (collider.gameObject.tag == "Player"){
//			refresh(collider.gameObject);
//		}
//		if (collider.gameObject.tag == "Enemy"){
//			collider.gameObject.GetComponent<monster_behaviour>().collected_water += 100; 
//		}
//	}
//	
//
//	
//	public void refresh(GameObject player){
//			audio.PlayOneShot(refresh_sound);
//			//set spawnpoint and spawn camera
//			GameObject sp = GameObject.FindGameObjectWithTag("spawn_point");
//			Camera.main.SendMessage("set_spawn_camera", spawn_camera.transform);
//		
//			sp.transform.position = spawn_point.transform.position;
//			player.GetComponent<input_player>().current_water = player.GetComponent<input_player>().max_water;
//			player.GetComponent<input_player>().current_oasis = gameObject;
//	}
}
