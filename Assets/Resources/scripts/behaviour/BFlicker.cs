using UnityEngine;
using System.Collections;

public class BFlicker : MonoBehaviour {

	public AnimationCurve flickerCurve;
	public float speed = 1;
	public float min = 0.5f;
	public float max = 0.7f;
	
	public Renderer[] renderers;
	
	void Start () {
		flickerCurve = new AnimationCurve(new Keyframe[20]);
		
		Random.seed = (int)gameObject.GetInstanceID();
		int keys = 20;
		for (int i = 0; i < keys; i++){
			flickerCurve.AddKey(new Keyframe(1/(float)keys*(float)i,Random.Range(min,max)));
		}
		flickerCurve.preWrapMode = WrapMode.Loop;
		flickerCurve.postWrapMode = WrapMode.Loop;
	}
	
	// Update is called once per frame
	void Update () {
		if(renderers.Length > 0){
			foreach (Renderer r in renderers){
				foreach (Material m in r.materials){
					m.SetFloat("_Emit",flickerCurve.Evaluate(Time.time*speed));
				}
			}
		}
		else{
			foreach (Material m in renderer.materials){
					m.SetFloat("_Emit",flickerCurve.Evaluate(Time.time*speed));
			}
		}
	}
}
