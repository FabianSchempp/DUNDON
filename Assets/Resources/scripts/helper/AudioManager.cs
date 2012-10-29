using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public static Dictionary<string,AudioClip> audioClips = new  Dictionary<string, AudioClip>();
	public AudioClip runeActivation;
	
	public static Dictionary<string,GameObject> audioSources = new Dictionary<string, GameObject>();
	public static HashSet<string> audioSourcesUsed = new HashSet<string>();
	
	void Awake(){
		audioClips.Add("runeActivation", runeActivation);	
	}
	
	public static void spawnAudioSource(string key){
		if(audioSourcesUsed.Add(key)){
			AudioClip clip; 
			if(audioClips.TryGetValue(key, out clip)){
				GameObject spawnedObject = new GameObject("audioSource " + key);
				spawnedObject.AddComponent<AudioSource>();
				spawnedObject.audio.clip = clip;
				spawnedObject.audio.Play();
				Destroy(spawnedObject,clip.length);
			}
		}
	}
		
	public static void spawnAudioSourceOneShot(string key){
		AudioClip clip; 
		if(audioClips.TryGetValue(key, out clip)){
			GameObject spawnedObject = new GameObject("audioSourceOneShot " + key);
			spawnedObject.AddComponent<AudioSource>();
			spawnedObject.audio.clip = clip;
			spawnedObject.audio.Play();
			Destroy(spawnedObject,clip.length);
		}
	}
	
	public static void deleteAudioSource(string key){
		GameObject g;
		if(audioSources.TryGetValue(key, out g)){
			Destroy(g);
			audioSourcesUsed.Remove(key);
		}
	}
	
	public static void modifyAudioSource(string key,float pitch){
		GameObject g;
		if(audioSources.TryGetValue(key, out g)){
			g.audio.pitch = pitch;
		}
	}
		
	public static void deleteAllAudioSource(){
		foreach(string s in audioSourcesUsed){
			GameObject g;
			if(audioSources.TryGetValue(s, out g)){
				Destroy(g);
			}
		}
		audioSourcesUsed.Clear();
	}
}
