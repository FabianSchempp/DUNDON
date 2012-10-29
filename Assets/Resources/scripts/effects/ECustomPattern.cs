using UnityEngine;
using System.Collections;

public class ECustomPattern : MonoBehaviour {
	public Vector2 pattern_size = new Vector2(4,4);
	Texture2D pattern;
	public Texture2D pixel_white;
	public Texture2D pixel_black;
	private GUIStyle style = new GUIStyle();
	public static int pattern_UID = 0;
	// Use this for initialization
	void Start () {
		//look for unique ID if not found generate one
		if (PlayerPrefs.HasKey("caravan_pattern_UID") && PlayerPrefs.GetInt("caravan_pattern_UID") != 0){
			pattern_UID = PlayerPrefs.GetInt("caravan_pattern_UID");
			Debug.Log("found your own uniqe pattern: " + pattern_UID);

		}
		else{
			BitArray bit_pattern = new BitArray(16);
			for (int i = 0;  i < 16; i++ ) {
				bit_pattern.Set(i,Random.value > 0.5f);
			}
  			PlayerPrefs.SetInt("caravan_pattern_UID",getIntFromBitArray(bit_pattern));
			PlayerPrefs.Save();
			pattern_UID = PlayerPrefs.GetInt("caravan_pattern_UID");
			Debug.Log("generated your own uniqe pattern");

		}
		
		//create texture with pattern form playerprefs
		pattern = new Texture2D((int)pattern_size.x * pixel_white.width,(int)pattern_size.y * pixel_white.height);
		pattern.filterMode = FilterMode.Point;
		//pattern.wrapMode = TextureWrapMode.Clamp;
		
		BitArray pattern_array = new BitArray(System.BitConverter.GetBytes(pattern_UID));
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
		renderer.material.SetTexture("_sign", pattern);
		sign_material = (Material)Resources.Load("shader/sign_gui");
	}
	
//	void Start () {
//		//look for unique ID if not found generate one
//		if (PlayerPrefs.HasKey("caravan_pattern_UID") && PlayerPrefs.GetInt("caravan_pattern_UID") != 0){
//			pattern_UID = PlayerPrefs.GetInt("caravan_pattern_UID");
//			Debug.Log("found your own uniqe pattern: " + pattern_UID);
//
//		}
//		else{
//			BitArray bit_pattern = new BitArray(16);
//			for (int i = 0;  i < 16; i++ ) {
//				bit_pattern.Set(i,Random.value > 0.5f);
//			}
//  			PlayerPrefs.SetInt("caravan_pattern_UID",getIntFromBitArray(bit_pattern));
//			PlayerPrefs.Save();
//			pattern_UID = PlayerPrefs.GetInt("caravan_pattern_UID");
//			Debug.Log("generated your own uniqe pattern");
//
//		}
//		
//		//create texture with pattern form playerprefs
//		pattern = new Texture2D((int)pattern_size.x,(int)pattern_size.y);
//		pattern.filterMode = FilterMode.Point;
//		//pattern.wrapMode = TextureWrapMode.Clamp;
//		
//		BitArray pattern_array = new BitArray(System.BitConverter.GetBytes(pattern_UID));
//		
//		for(int x = 0; x < pattern.width; x++){
//			for(int y = 0; y < pattern.height; y++){
//					if((pattern_array[x + (y*pattern.width)])){
//						pattern.SetPixel(x,y,Color.black);
//					}
//					else{
//						pattern.SetPixel(x,y,Color.white);
//					}
//			}		
//		}	
//		pattern.Apply();
//		renderer.material.SetTexture("_sign", pattern);

//	}
	
	// Update is called once per frame
	void Update () {
	}
	
	private Material sign_material;
	void OnGUI(){
		Graphics.DrawTexture(new Rect(128,128,128,128),pattern,sign_material);
	}
				
	private int getIntFromBitArray(BitArray bitArray)
	{
	    int[] array = new int[1];
	    bitArray.CopyTo(array, 0);
	    return array[0];
	}
			
}
				
