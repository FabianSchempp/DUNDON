using UnityEngine;
using System.Collections;

//public enum cursor_state{
//	normal,
//	loading,
//}

public class ECustomMeshCursor : MonoBehaviour {
//	public GameObject cursor_mesh_ground;
//	public GameObject cursor_mesh_sky;
//	public Vector3 ground_offset = Vector3.zero;
	public static GameObject ground_cursor;
	public static GameObject sky_cursor;
	public static Vector3 cursor_ground_offset;
//	public Vector3 sky_level;
	//public bool trail = true;
		
	void Awake()
	{
		Screen.showCursor = false;
		ground_cursor = (GameObject) Instantiate(Resources.Load("prefabs/GUI/fadeing_cursor"));
		ground_cursor.name = "ground_cursor";
		
		sky_cursor = (GameObject) Instantiate(Resources.Load("prefabs/GUI/fadeing_cursor_sky"));
		sky_cursor.name = "sky_cursor";
		sky_cursor.layer = 23;
		sky_cursor.transform.localScale = Vector3.one * 20;
//		cursor_ground_offset = ground_offset;
	}
	
	public static void update_mesh_cursor(Vector3 position,Quaternion ground_rotation,Vector3 player_position){
		if(ground_cursor != null && ground_cursor.active == true && !float.IsNaN(position.x) && position != Vector3.zero){
			//ground_cursor.transform.position = position + cursor_ground_offset;
			sky_cursor.transform.position = position + Vector3.up * 432;
			ground_cursor.transform.position = position;
			ground_cursor.transform.rotation = ground_rotation;
		}
		if(ground_cursor != null && !float.IsNaN(position.x) && position != Vector3.zero){
			sky_cursor.active = true;
			ground_cursor.active = true;
		}
		else if(position == Vector3.zero){
			sky_cursor.active = false;
			ground_cursor.active = false;
		}
	}
	
}
