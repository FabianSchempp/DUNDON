using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[ExecuteInEditMode]
public class OShaderHealthbar : MonoBehaviour {
	
	public GameObject highest_fill;
	public GameObject lowest_fill;
	public float smooth_value = 1f;
	public float alarm_factor = 0.8f;
	public Controller player;
	
//	public float test_factor;
	
	private Material _material;
	private float _fillDistance;	
	
	void Start () {
		_material = renderer.material;
		_fillDistance = Vector3.Distance(lowest_fill.transform.position ,highest_fill.transform.position);
	}
	
	private void update_healthbar() { 
		float _fill = player.stats.healthFactor;
//		float _fill = test_factor;
		_material.SetVector("_reference_vector",Vector3.Lerp(lowest_fill.transform.position, highest_fill.transform.position,_fill));
	}
	
	void Update () {
		update_healthbar();
	}
	
}