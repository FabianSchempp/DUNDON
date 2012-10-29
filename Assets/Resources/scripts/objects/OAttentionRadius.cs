using UnityEngine;
using System.Collections;

public class OAttentionRadius : MonoBehaviour {
	private string testFor = "Player";
	private GameObject _attentionObject;
	private float radius = 0;
	public GameObject attentionObject{
		get{return _attentionObject;}
	}
	
	public void Start(){
		SphereCollider c = (SphereCollider) collider;
		radius = c.radius;
	}
	
	void OnTriggerStay(Collider collider){
		if(collider.tag == testFor){
			_attentionObject = collider.gameObject;
		}	
	}
	
	void OnTriggerExit(Collider collider){
		if(collider.gameObject == _attentionObject){
			_attentionObject = null;
		}	
	}
	
	public void sizeTrigger(float r){
		SphereCollider c = (SphereCollider) collider;
		c.radius = r;
	}
	
	public void resetTrigger(){
		SphereCollider c = (SphereCollider) collider;
		c.radius = radius;
	}
}
