using UnityEngine;
using System.Collections;

public class OWeapon : MonoBehaviour {

	public GameObject stroke;
	private BAudioPlayer _BAudioPlayer;
	private string[] strokeSounds = new string[4]{"stroke_a","stroke_b","stroke_c","stroke_d"};
	public void Start(){
		_BAudioPlayer = gameObject.GetComponent<BAudioPlayer>();
	}
	
	public void enable(){
		_BAudioPlayer.PlayRandomBreak(strokeSounds);
		stroke.active = true;
	}
	
	public void disable(){
		stroke.active = false;
	}
}
