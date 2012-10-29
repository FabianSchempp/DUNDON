using UnityEngine;
using System.Collections;

public class OSpeechBar : MonoBehaviour {
	
	public void show(){
		animation.Play("speech_shadow_fade_in");
	}
	public void hide(){
		animation.Play("speech_shadow_fade_out");
	}
}
