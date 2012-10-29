using UnityEngine;
using System.Collections;

public class EWhiteIn : MonoBehaviour {

	void Update () {
		if(Time.timeSinceLevelLoad < 1){
		}
		else if(Time.timeSinceLevelLoad < 10){
		}
		else{
			Destroy(this);
		}
		
	}
}
