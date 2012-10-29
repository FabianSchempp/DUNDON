using UnityEngine;
using System.Collections;

public class BShowHideExSpriteFont : MonoBehaviour {
	exSpriteFont eSF;
	void Start(){
		eSF = gameObject.GetComponent<exSpriteFont>();
	}
	public void hide(float factor){
		StartCoroutine(hideText(factor));
	}
	IEnumerator hideText(float factor){
		float f = factor;
		while (eSF.topColor.a >= 0) {
			Debug.Log("hide" + eSF.topColor.a);
       		eSF.topColor = new Color(1,1,1,eSF.topColor.a - Time.deltaTime * f);
			eSF.botColor = eSF.topColor;
        	yield return null;
    	}
	}
	
	public void show(float factor){
		StartCoroutine(showText(factor));
	}
	
	IEnumerator showText(float factor){
		float f = factor;
		while (eSF.topColor.a <= 1) {
			Debug.Log("show");
       		eSF.topColor = new Color(1,1,1,eSF.topColor.a + Time.deltaTime * f);
			eSF.botColor = eSF.topColor;
        	yield return null;
    	}
	}
}
