using UnityEngine;
using System.Collections;

public class OFireUrn : MonoBehaviour {
	public GameObject burning;
	public GameObject cold;
	public GameObject player;
	public bool isBurning = false;
	
	void Start () {
		burning.SetActiveRecursively(false);
		cold.SetActiveRecursively(true);
	}
	
	public void ignite() {
		burning.SetActiveRecursively(true);
		cold.SetActiveRecursively(false);
		isBurning = true;
	}
	
	void Update(){
		if(player){
			ControllerCamera.lookAt(gameObject,1f,5f);
		}
	}
	
	void OnTriggerEnter(Collider collider){
		if(collider.tag == "character"){
			player = collider.gameObject;
		}
	}
	
	void OnTriggerExit(Collider collider){
		if(collider.tag == "character"){
			player = null;
		}
	}
}
