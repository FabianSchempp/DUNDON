using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterDatabase : MonoBehaviour {
	
	public static List<GameObject> head_database = new List<GameObject>();
	public static List<GameObject> helm_database = new List<GameObject>();
	public static List<GameObject> torso_database = new List<GameObject>();
	public static List<GameObject> stomage_database = new List<GameObject>();
	public static List<GameObject> thight_right_database = new List<GameObject>();
	public static List<GameObject> thight_left_database = new List<GameObject>();
	public static List<GameObject> shoulder_right_database = new List<GameObject>();
	public static List<GameObject> shoulder_left_database = new List<GameObject>();
	public static List<GameObject> shin_right_database = new List<GameObject>();
	public static List<GameObject> shin_left_database = new List<GameObject>();
	public static List<GameObject> arm_upper_right_database = new List<GameObject>();
	public static List<GameObject> arm_upper_left_database = new List<GameObject>();
	public static List<GameObject> arm_lower_right_database = new List<GameObject>();
	public static List<GameObject> arm_lower_left_database = new List<GameObject>();
	public static List<GameObject> shield_database = new List<GameObject>();
	public static List<GameObject> sword_database = new List<GameObject>();
	
	public GameObject head_database_root;
	public GameObject helm_database_root;
	public GameObject torso_database_root;
	public GameObject stomage_database_root;
	public GameObject shoulder_right_database_root;
	public GameObject shoulder_left_database_root;
	public GameObject thight_right_database_root;
	public GameObject thight_left_database_root;
	public GameObject shin_right_database_root;
	public GameObject shin_left_database_root;
	public GameObject arm_upper_right_database_root;
	public GameObject arm_upper_left_database_root;
	public GameObject arm_lower_right_database_root;
	public GameObject arm_lower_left_database_root;
	public GameObject shield_database_root;
	public GameObject sword_database_root;
	
	public void initialize(){
		int i = 0;
		
		foreach (Transform child in head_database_root.transform) {
			Debug.Log("loaded heads" +i);
			head_database.Add(child.gameObject);
			i++;
		}
		i = 0;
		foreach (Transform child in helm_database_root.transform) {
			Debug.Log("loaded helmets" +i);
			helm_database.Add(child.gameObject);
			i++;
		}
		i = 0;
		foreach (Transform child in torso_database_root.transform) {
			Debug.Log("loaded torso" +i);
			torso_database.Add(child.gameObject);
			i++;
		}
		i = 0;
		foreach (Transform child in stomage_database_root.transform) {
			Debug.Log("loaded stomage" +i);
			 stomage_database.Add(child.gameObject);
			i++;
		}
		
		i = 0;
			foreach (Transform child in shoulder_right_database_root.transform) {
			Debug.Log("shoulder_right" +i);
			shoulder_right_database.Add(child.gameObject);
			i++;
		}
		i = 0;
		foreach (Transform child in shoulder_left_database_root.transform) {
			Debug.Log("shoulder_left" + i);
			 shoulder_left_database.Add(child.gameObject);
			i++;
		}
		i = 0;
		
		foreach (Transform child in thight_right_database_root.transform) {
			Debug.Log("thight_right" +i);
			thight_right_database.Add(child.gameObject);
			i++;
		}
		i = 0;
		foreach (Transform child in  thight_left_database_root.transform) {
			Debug.Log("thight_left" + i);
			 thight_left_database.Add(child.gameObject);
			i++;
		}
		i = 0;
		foreach (Transform child in shin_right_database_root.transform) {
			Debug.Log("shin_right");
			shin_right_database.Add(child.gameObject);
			i++;
		}
		i = 0;
		foreach (Transform child in  shin_left_database_root.transform) {
			Debug.Log("shin_left" +i);
			 shin_left_database.Add(child.gameObject);
			i++;
		}
		i = 0;
		foreach (Transform child in arm_upper_right_database_root.transform) {
			Debug.Log("loaded arm_upper_right" +i);
			arm_upper_right_database.Add(child.gameObject);
			i++;
		}
		i = 0;
		foreach (Transform child in  arm_upper_left_database_root.transform) {
			Debug.Log("loaded arm_upper_left" +i);
			 arm_upper_left_database.Add(child.gameObject);
			i++;
		}
		i = 0;
		foreach (Transform child in arm_lower_right_database_root.transform) {
			Debug.Log("loaded arm_lower_right" +i);
			arm_lower_right_database.Add(child.gameObject);
			i++;
		}
		foreach (Transform child in arm_lower_left_database_root.transform) {
			Debug.Log("loaded arm_lower_left" +i);
			 arm_lower_left_database.Add(child.gameObject);
			i++;
		}
		i = 0;
		foreach (Transform child in shield_database_root.transform) {
			Debug.Log("loaded shields" +i);
			shield_database.Add(child.gameObject);
			i++;
		}
		i = 0;
		foreach (Transform child in  sword_database_root.transform) {
			Debug.Log("loaded swords" +i);
			sword_database.Add(child.gameObject);
			i++;
		}
	}
	
		
	public static GameObject getArmLowerRight(){
		return arm_lower_right_database[Random.Range(0,arm_lower_right_database.Count-1)];
	}
	public static GameObject getArmLowerLeft(){
		return arm_lower_left_database[Random.Range(0,arm_lower_left_database.Count-1)];
	}
	public static GameObject getArmUpperRight(){
		return arm_upper_right_database[Random.Range(0,arm_upper_right_database.Count-1)];
	}
	public static GameObject getArmUpperLeft(){
		return arm_upper_left_database[Random.Range(0,arm_upper_left_database.Count-1)];
	}
	
	public static GameObject getThigtRight(){
		return thight_right_database[Random.Range(0,thight_right_database.Count-1)];
	}
	public static GameObject getThigtLeft(){
		return thight_left_database[Random.Range(0,thight_left_database.Count-1)];
	}
	public static GameObject getShinRight(){
		return shin_right_database[Random.Range(0,shin_right_database.Count-1)];
	}
	public static GameObject getShinLeft(){
		return shin_left_database[Random.Range(0,shin_left_database.Count-1)];
	}
	
	public static GameObject getShoulderRight(){
		return shoulder_right_database[Random.Range(0,shoulder_right_database.Count-1)];
	}
	public static GameObject getShoulderLeft(){
		return shoulder_left_database[Random.Range(0,shoulder_left_database.Count-1)];
	}
	
	public static GameObject getHelmet(){
		return helm_database[Random.Range(0,helm_database.Count-1)];
	}
	public static GameObject getSword(){
		return sword_database[Random.Range(0,sword_database.Count-1)];
	}
	public static GameObject getShield(){
		return shield_database[Random.Range(0,shield_database.Count-1)];
	}
	public static GameObject getHead(){
		Debug.Log(head_database.Count);
		return head_database[Random.Range(0,head_database.Count-1)];
	}
	public static GameObject getTorso(){
		return torso_database[Random.Range(0,torso_database.Count-1)];
	}
	public static GameObject getStomage(){
		return stomage_database[Random.Range(0,torso_database.Count-1)];
	}
}
