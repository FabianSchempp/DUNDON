using UnityEngine;
using System.Collections;

public class OAwareness : MonoBehaviour {
	
	private float _fade_speed = 1;
	public Color reveal_color = Color.yellow;
	private Color color = Color.black;
	private bool _activated = false;
	
	void Start () {
		AwarenessManager.addObject(this.UpdateAwarenessLevel);
	}

	void UpdateAwarenessLevel(Vector3 position, float radius)
	{
		if(Vector3.Distance(transform.position, position) < radius)
		{
			color = reveal_color;
		}
		else
		{
			color = Color.black;
			
		}
	}
	
	void Update(){
		//fade in or fadeout based on Awareness;
		foreach (Material m in renderer.materials){
			m.SetColor("_Color", Color.Lerp(renderer.material.GetColor("_Color"), color,_fade_speed*Time.deltaTime));
		}
	}
}
