using UnityEngine;
using System.Collections;

public class starItem : MonoBehaviour {

	public void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Player"){
			Destroy(gameObject);
//			ControllerCamera.lookAt(ControllerMari.player_script.collect_star(new Vector3(Random.Range(-100,100),0,Random.Range(-100,100))),3);
		}
	}
}
