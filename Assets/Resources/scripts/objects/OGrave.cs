using UnityEngine;
using System.Collections;

public class OGrave : MonoBehaviour {
	
	public int waterToReturn = 5;
	public int age = 1;
	
	public int id = -1;
	
	public string collectURL = "http://www.uniworlds.de/caravan/postCollect.php";
	public GameObject grave_light;
	public GameObject destroy_stuff;
	public GameObject particles;
	
	bool disabled = false;
	
	private void give_treasures(GameObject player){ 
		StartCoroutine(Authenticator.getInstance().updateCharacter());
	}
	
	public bool collect(){
		if(!disabled){
			particles.SetActiveRecursively(true);
			animation.Play("grave_disintegrate");
			disabled = true;
			return true;
		}
		return false;
	}
	
	public void destroy(){
		disabled = true;
	}
}
