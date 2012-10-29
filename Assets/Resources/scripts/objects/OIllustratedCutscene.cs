using UnityEngine;
using System.Collections;

public class OIllustratedCutscene : MonoBehaviour {
	public Texture2D image;
	public float livetime = 2;
	private float counter = 0;
	public bool kill_at_click = true;
	public Material fade_out;
	private bool alive = true;
	
	// Use this for initialization
	void Start () {
		fade_out.SetTexture("_MainTex",image);
	}
	
	// Update is called once per frame
	void Update () {
		if(kill_at_click){
			if(Input.GetMouseButtonDown(0) && alive){
				//Destroy(gameObject);
				kill();
			}
			if(Input.GetButtonDown("action") && alive){
				//Destroy(gameObject);
				kill();
			}
		}
		else
		{
			if (counter > livetime) Destroy(gameObject);
		}
		
		//set fading
		if(alive){
			counter += Time.deltaTime * 2.2f;
		}
		else{
			counter -= Time.deltaTime * 2.2f;
			if (counter < 0){
				Destroy(gameObject);
			}
		}
		fade_out.SetFloat("_Fade",counter);
	}	
	
	public void kill(){
		alive = false;
		counter = 1;
	}
	void OnGUI(){
		//GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),image);	
		Graphics.DrawTexture(new Rect(0,0,Screen.width,Screen.height),image,fade_out);
	}
}
