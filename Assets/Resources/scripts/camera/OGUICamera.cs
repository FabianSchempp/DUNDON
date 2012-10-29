using UnityEngine;
using System.Collections;

//public class OGUICamera : MonoBehaviour {
//	
//	public exSprite backgroundWhite;
//	public exSprite backgroundWhiteHalf;
//	public exSpriteFont overlayText;
//	public exSpriteFont controllText;
//	public exSprite mouseButtonRight;
//	public exSprite mouseButtonMiddle;
//	public exSprite mouseButtonLeft;
//	
//	public static exSprite BackgroundWhite;
//	public static exSprite BackgroundWhiteHalf;
//	public static exSpriteFont OverlayText;
//	public static exSpriteFont ControllText;
//	public static exSprite MouseButtonRight;
//	public static exSprite MouseButtonMiddle;
//	public static exSprite MouseButtonLeft;
//	
//	public static Color whiteOpaque = new Color(1,1,1,1); 
//	public static Color whiteTransparent = new Color(1,1,1,0);
//	
//	// Use this for initialization
//	void Start () {
//		BackgroundWhite = backgroundWhite;
//		BackgroundWhite.color = whiteOpaque;
//		ControllText = controllText;
//		MouseButtonLeft = mouseButtonLeft;
//		MouseButtonMiddle = mouseButtonMiddle;
//		MouseButtonRight = mouseButtonRight;
//		
////		Backgroundwhitehalf = backgroundWhiteHalf;
//		
//		OverlayText = overlayText;
//		OverlayText.text = "";
//		hideText();
//		mouseButtonLeft.enabled = false;
//		mouseButtonMiddle.enabled = false;
//		mouseButtonRight.enabled = false;
//		controllText.text = "";
//		TextLoader.loadTexts();
//	}
//	
//	public static void whiteOut(float speed) {
//		BackgroundWhite.color = Color.Lerp(BackgroundWhite.color,whiteTransparent, speed *Time.deltaTime);
//	}
//	public static void whiteIn(float speed) {
//		BackgroundWhite.color = Color.Lerp(BackgroundWhite.color,whiteOpaque, speed *Time.deltaTime);
//	}
//	public static void showText(string key) {
//		string text = "...";
//		exViewportPosition p = OverlayText.gameObject.GetComponent<exViewportPosition>();
//		p.x = 0.5f;
//		p.y = 0.5f;
//		TextLoader.texts.TryGetValue(key,out text);
//		OverlayText.text = text;
//		OverlayText.GetComponent<BShowHideExSpriteFont>().show(2);
//	}
//	public static void showTextUpperThird(string key) {
//		string text = "...";
//		exViewportPosition p = OverlayText.gameObject.GetComponent<exViewportPosition>();
//		p.x = 0.5f;
//		p.y = 0.7f;
//		TextLoader.texts.TryGetValue(key,out text);
//		OverlayText.text = text;
//		OverlayText.GetComponent<BShowHideExSpriteFont>().show(2);
//	}
//	public static void hideText() {
////		OverlayText.text = "";
//		OverlayText.GetComponent<BShowHideExSpriteFont>().hide(5);
//	}
//	
//	public static void showControlls(mouseButtons b, string text){
//		if(b == mouseButtons.left){
//			MouseButtonLeft.enabled = true;
//			MouseButtonLeft.animation.Play("gui_buttons_middle_in");
//		}
//		if(b == mouseButtons.middle){
//			MouseButtonMiddle.enabled = true;
//			MouseButtonMiddle.animation.Play("gui_buttons_middle_in");
//		}
//		if(b == mouseButtons.right){
//			MouseButtonRight.enabled = true;
//			MouseButtonRight.animation.Play("gui_buttons_middle_in");
//		}
//
//		if(ControllText.text != text){
//			ControllText.text = text;
//		}
//	}
//	
//	public static void hideControlls(){
//		MouseButtonLeft.enabled = false;
//		MouseButtonMiddle.enabled = false;
//		MouseButtonRight.enabled = false;
//		ControllText.text = "";
//		MouseButtonLeft.animation.Stop();
//		MouseButtonMiddle.animation.Stop();
//		MouseButtonRight.animation.Stop();
//	}
//	
//	
//}
//
//public enum mouseButtons{
//	left,
//	right,
//	middle
//}