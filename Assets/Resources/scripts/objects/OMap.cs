using UnityEngine;
using System.Collections;

public class OMap : MonoBehaviour {
	
	public Renderer mapRenderer;
	
	void Start () {
		animation.Stop();
		mapRenderer.enabled = false;
	}
	
	public void unfold() {
		mapRenderer.enabled = true;
		animation.Play("Default Take");
	}
	
	public void fold() {
		animation.Stop();
		mapRenderer.enabled = false;
	}
}
