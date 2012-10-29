using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum light_type{
	bounce_light,
	sun_light,
}
public class light_manager : MonoBehaviour {
	
	public static List<Light> bounce_lights = new List<Light>();
	public static List<Light> sun_lights = new List<Light>();
	
	private Skybox sky_box;
	
	public Color sun_light_color;
	public Color bounce_light_color;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Light l in light_manager.bounce_lights){
			l.color = bounce_light_color;
		}
		foreach(Light l in light_manager.sun_lights){
			l.color = sun_light_color;
		}
		sky_box.material.SetColor("_Tint", sun_light_color);
	}
}
