using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (BThirdPerson))]
[System.Serializable]
public class BThirdPersonMotionSystem : MonoBehaviour {
	
	public Animation root;
	public List<AnimationClip> Animations;
	public float stateTime = 0;

	string _groundTag = "";
	
	private BThirdPerson _controller;
	public BThirdPerson controller{
		get{return _controller;}
		set{_controller = value;}
	}
	
	void Start(){
		controller = gameObject.GetComponent<BThirdPerson>();
	}
	
	void LateUpdate(){
	}

	public void CrossFade(string clip){
		root.CrossFade(clip);
	}
	
	public void Play(string clip){
		root.Play(clip);
	}
	
	public void Stop(){
		root.Stop();
	}
	
	public void Loop(string clip){
		root[clip].wrapMode = WrapMode.Loop;
		root.CrossFade(clip);
	}
	
	public float getAnimationTime(string clip){
		return root[clip].length;
	}
	
	public void resetAnimation(){
		root[root.clip.name].time = 0;
	}
}