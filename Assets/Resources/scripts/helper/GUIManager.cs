using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	
public class GUIManager : MonoBehaviour {
	public static Camera GUICamera;
	public static Transform GUIPlane;
	public static GameObject QuickInfo;
	public static GameObject Speech;
	public GameObject top;
	public GameObject bottom;
	public GameObject right;
	public GameObject left;
	public static Vector3 _top;
	public static Vector3 _bottom;
	public static Vector3 _right;
	public static Vector3 _left;
	public static Vector3 _gridHeight;
	public static Vector3 _gridWidth;
	private static List<GameObject> QuickInfos = new List<GameObject>();
	private static List<float> QuickInfoBirthTime = new List<float>();
	private static List<float> QuickInfoDuration = new List<float>();
	public static Animation guiAnimation;
		
	private bool _isMirrored = false;
	
	void Start(){
		GUIManager._top = top.transform.localPosition;
		GUIManager._bottom = bottom.transform.localPosition;
		GUIManager._right = right.transform.localPosition;
		GUIManager._left = left.transform.localPosition;
		GUIManager._gridHeight = (_top - _bottom)/20;
		GUIManager._gridWidth = (_right - _left)/20;
		GUIManager.QuickInfo = (GameObject) Resources.Load("GUI/QuickInfo");
		GUIManager.guiAnimation = animation;
		GUIManager.GUICamera = camera;
		GUIPlane = transform;
	}
	
	void Update () {
		List<GameObject> giToDestroy = new List<GameObject>();
		for (int i = 0; i < GUIManager.QuickInfos.Count; i++){
			GUIManager.QuickInfos[i].transform.localPosition = Vector3.Lerp(GUIManager.QuickInfos[i].transform.localPosition,_top - ((float)i * GUIManager._gridHeight),Time.deltaTime * 20);
			if (Time.timeSinceLevelLoad - GUIManager.QuickInfoBirthTime[i] >= GUIManager.QuickInfoDuration[i]){
				giToDestroy.Add(GUIManager.QuickInfos[i]);
			}
		}
		foreach (GameObject g in giToDestroy){
			GUIManager.QuickInfoBirthTime.RemoveAt(GUIManager.QuickInfos.IndexOf(g));
			GUIManager.QuickInfoDuration.RemoveAt(GUIManager.QuickInfos.IndexOf(g));
			GUIManager.QuickInfos.Remove(g);
			Destroy(g);
		}
	}
	
	public static void destroyQuickInfo(GameObject g){
		if(GUIManager.QuickInfos.Contains(g)){
			GUIManager.QuickInfoBirthTime.RemoveAt(GUIManager.QuickInfos.IndexOf(g));
			GUIManager.QuickInfoDuration.RemoveAt(GUIManager.QuickInfos.IndexOf(g));
			GUIManager.QuickInfos.Remove(g);
			Destroy(g);
		}
	}
	public static void screenCastQuickInfo(string qi){
		GameObject g = (GameObject)GameObject.Instantiate(QuickInfo);
		g.layer = 31;
		exSpriteFont gt = g.GetComponent<exSpriteFont>();
		gt.text = qi;
		g.transform.parent = GUIPlane;
		gt.renderCamera = GUIManager.GUICamera;
		GUIManager.QuickInfos.Add(g);
		GUIManager.QuickInfoBirthTime.Add(Time.timeSinceLevelLoad);
		GUIManager.QuickInfoDuration.Add(3);
	}

	public static void hideGUI(){
		if(GUIManager.guiAnimation.clip.name != "gui_out"){
			GUIManager.guiAnimation.Play("gui_out");
		}	
	}

	public static void showGUI(){
		if(GUIManager.guiAnimation.clip.name != "gui_in"){
			GUIManager.guiAnimation.Play("gui_in");	
		}
	}
	
	#region controllSets
//	public static Dictionary<string,string> cSFreedanIdle = new Dictionary<string, string>();
//	public void setControllSetsFreedan(){
//		cSFreedanEmpty.Add("up", "");
//		cSFreedanEmpty.Add("down", "");
//		cSFreedanEmpty.Add("left", "");
//		cSFreedanEmpty.Add("right", "");
//		cSFreedanEmpty.Add("action", "");
//		cSFreedanEmpty.Add("jump", "");
//	}
	#endregion
	
	
}
