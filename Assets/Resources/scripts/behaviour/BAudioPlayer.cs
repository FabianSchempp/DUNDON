using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class BAudioPlayer : MonoBehaviour {
	
	public Dictionary<string,AudioClip> Clips = new Dictionary<string, AudioClip>();
	
	public AudioClip[] clips;
	private string currentClip;
	
	void Start () {
		foreach(AudioClip c in clips){
			Clips.Add(c.name,c);
		}
	}
	
	
	public void Play(string c){
		if(c != currentClip || !audio.isPlaying){
			audio.clip=Clips[c];
			audio.Play();
			currentClip = c;
			audio.loop = false;
		}
	}
	
	public void PlayBreak(string c){
			audio.clip=Clips[c];
			audio.Play();
			currentClip = c;
			audio.loop = false;
	}
	
	public void PlayRandom(string[] c){
		if(c[0] != currentClip || !audio.isPlaying){
			audio.clip=Clips[c[Random.Range(0,c.Length-1)]];
			audio.Play();
			currentClip = c[0];
			audio.loop = false;
		}
	}
	
	public void PlayRandomBreak(string[] c){
			audio.clip=Clips[c[Random.Range(0,c.Length-1)]];
			audio.Play();
			currentClip = c[0];
			audio.loop = false;
	}
	
	public void Loop(string c){
		if(c != currentClip || !audio.isPlaying){
			audio.clip=Clips[c];
			audio.Play();
			audio.loop = true;
			currentClip = c;
		}
	}
}
