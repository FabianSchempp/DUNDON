using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class die{
	
	public static HashSet<Vector3> graves = new HashSet<Vector3>();
	
	public static void sendDeath(Vector3 position, int id)
	{
		graves.Add(position);
		Vector3 pos = position;
		PlayerPrefs.SetFloat("lastDeathX", pos.x);
		PlayerPrefs.SetFloat("lastDeathY", pos.y);
		PlayerPrefs.SetFloat("lastDeathZ", pos.z);
		
		int i = 0;
		foreach(Vector3 g in graves){
			PlayerPrefs.SetFloat("graveX"+i,g.x);
			PlayerPrefs.SetFloat("graveY"+i,g.y);
			PlayerPrefs.SetFloat("graveZ"+i,g.z);
			i++;
		}
		PlayerPrefs.Save();

	}
	
	public static void removeDeath(Vector3 position){
		graves.Remove(position);
	}
	
	public static void getDeath(){
		int graveCount = PlayerPrefs.GetInt("graveCount",0);
		for(int i = 0; i < graveCount; i++){
			Vector3 grave = Vector3.zero;
			grave.x = PlayerPrefs.GetFloat("graveX"+i,0);
			grave.y = PlayerPrefs.GetFloat("graveY"+i,0);
			grave.z = PlayerPrefs.GetFloat("graveZ"+i,0);
			graves.Add(grave);
		}
		paintGraves();
	}
	
	public static void paintGraves(){
		GameObject zombie = (GameObject) Resources.Load("models/character/knight_evil");
		foreach(Vector3 p in graves){
			Vector3 px = p;
			Vector3 nx;
			Helper.raycastPlaceGround(ref px,out nx);
			GameObject.Instantiate(zombie,px,Quaternion.AngleAxis(Random.Range(0,360),Vector3.up));
		}
	}
}