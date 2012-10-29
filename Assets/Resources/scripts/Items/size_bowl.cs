using UnityEngine;
using System.Collections;

public class size_bowl : MonoBehaviour {

	public void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Player"){
			Destroy(gameObject);
//			ControllerMari.player_script.collect_size_bowl();
		}
	}
}
