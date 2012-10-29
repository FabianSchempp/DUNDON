using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SphereCollider))]
public class OAttentionObject : MonoBehaviour {
	
	private Material _material;
	public Color activeColor = Color.blue;
	private float _selected = 1000;
	private float _cutoff = 0.0f;
	private bool _activated = false;
	private float _factorMultiplyer = 0.02f;
	private float _factorOffset = 0.4f;
	
	public bool activated{
		get {return _activated;}
	}
	
	void Start () {
		_material = gameObject.renderer.material;
		gameObject.tag = "attention object";
		gameObject.collider.isTrigger = true;
		gameObject.layer = 25;
		_material.SetColor("_select", Color.black);
	}
	
	void Update (){
		markSelected();
	}
	
	private void markSelected(){
		
		if(_selected < 0.1f){
			_activated = true;
		}
		else if(_activated){
			_material.SetColor("_select",activeColor);
		}
		else{
			_material.SetColor("_select",Color.Lerp(_material.GetColor("_select"), Color.Lerp(activeColor, Color.black,_selected),1*Time.deltaTime));
		}
	}
	
	public void attention(float distanceToTester,float distanceToCamera) {
    	_selected = Mathf.Clamp01(((distanceToTester + distanceToCamera) * _factorMultiplyer) - _factorOffset);	
	}
}
