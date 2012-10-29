using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class character_audio_player : MonoBehaviour {
	
	public AudioClip[] step_sound;
	
	public void on_step (int i) {
		int sound_number = (Time.frameCount + i) % step_sound.Length;
		audio.PlayOneShot(step_sound[sound_number]);
	}

}
