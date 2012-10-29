using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EGravePattern : MonoBehaviour {
	public static System.Collections.Generic.Dictionary<int,Texture2D> grave_pattern_textures = new System.Collections.Generic.Dictionary<int, Texture2D>();
	Vector2 pattern_size = new Vector2(4,4);
	Texture2D pattern;
	public float visibilityRange = 20f;
	public Vector2 offset = new Vector2(-5f,-15f);
	public GameObject billboard;
	
	void Update(){
		if (billboard != null) {
			billboard.transform.LookAt(Camera.main.transform,Vector3.up);	
			billboard.transform.transform.position = gameObject.transform.position + Vector3.up * 1.4f;
		}
	}
	
	public void initzialize(int UID) {
		if(!EGravePattern.grave_pattern_textures.TryGetValue(UID, out pattern)){	
			//create texture with pattern form playerprefs
			Texture2D pixel_white = (Texture2D)Resources.Load("textures/GUI/white_cross");
			Texture2D pixel_black = (Texture2D)Resources.Load("textures/GUI/white_circle");
			
			//create texture with pattern form playerprefs
			pattern = new Texture2D((int)pattern_size.x * pixel_white.width,(int)pattern_size.y * pixel_white.height);
			pattern.filterMode = FilterMode.Point;
			//pattern.wrapMode = TextureWrapMode.Clamp;
			
			BitArray pattern_array = new BitArray(System.BitConverter.GetBytes(UID));
			Color[] pixels_white = pixel_white.GetPixels(0,0,pixel_white.width,pixel_white.height);
			Color[] pixels_black = pixel_black.GetPixels(0,0,pixel_black.width,pixel_black.height);
			int pixels_width = pixel_white.width;
			int pixels_height = pixel_white.height;
			
			for(int x = 0; x < pattern_size.x; x++){
				for(int y = 0; y < pattern_size.y; y++){
						if((pattern_array[(int)(x + (y*pattern_size.x))])){
							pattern.SetPixels(x*pixels_width,y*pixels_height,pixels_width,pixels_height,pixels_white);
						}
						else{
							pattern.SetPixels(x*pixels_width,y*pixels_height,pixels_width,pixels_height,pixels_black);
						}
				}		
			}	
			pattern.Apply();
			
			EGravePattern.grave_pattern_textures.Add(UID,pattern);
		}
		
		billboard = (GameObject)Instantiate(Resources.Load("GUI/billboard_sign"),transform.position + Vector3.up*5f,Quaternion.identity);
		billboard.transform.LookAt(Camera.main.transform,Vector3.up);	
		billboard.renderer.material.SetTexture("_MainTex",pattern);
	}

//	public void initzialize(int UID) {
//		//create texture with pattern form playerprefs
//		pattern = new Texture2D((int)pattern_size.x,(int)pattern_size.y);
//		pattern.filterMode = FilterMode.Point;
//		BitArray pattern_array = new BitArray(System.BitConverter.GetBytes(UID));
//		
//		for(int x = 0; x < pattern.width; x++){
//			for(int y = 0; y < pattern.height; y++){
//					if((pattern_array[x + (y*pattern.width)])){
//						pattern.SetPixel(x,y,new Color32(0,0,0,0));
//					}
//					else{
//						pattern.SetPixel(x,y,Color.white);
//					}
//			}		
//		}	
//		pattern.Apply();
//		billboard = (GameObject)Instantiate(Resources.Load("GUI/billboard_sign"),transform.position + Vector3.up*5f,Quaternion.identity);
//		billboard.transform.LookAt(Camera.main.transform,Vector3.up);	
//		billboard.renderer.material.SetTexture("_MainTex",pattern);
//	}
	
//	void OnGUI(){
//		if(pattern != null){
//			Vector3 GOSP = Camera.main.WorldToScreenPoint(gameObject.transform.position);
//			GOSP = new Vector3(GOSP.x,Screen.height - GOSP.y,GOSP.z); //converting screenspace go "lol" GUIspace
//			
//			int buttonWidth = (int)(32 * Mathf.Clamp(visibilityRange - GOSP.z,0.1f,1));
//			int buttonHeight = (int)(32 * Mathf.Clamp(visibilityRange - GOSP.z,0.1f,1));
//			
//			if(GOSP.z < visibilityRange && GOSP.z > 0){ //if point is within frustrum and range
//				Graphics.DrawTexture(new Rect(GOSP.x - buttonWidth/2 + offset.x ,GOSP.y - buttonHeight/2 + offset.y, buttonWidth, buttonHeight),pattern);
//			}
//		}
//	}
				
	private int getIntFromBitArray(BitArray bitArray)
	{
	    int[] array = new int[1];
	    bitArray.CopyTo(array, 0);
	    return array[0];
	}
	
	void OnDisable(){
		Destroy(billboard);
	}
}
				
