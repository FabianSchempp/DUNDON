using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BCharacterAssambler : MonoBehaviour {	
	public Transform head;
	public Transform torso;
	public Transform stomage;
	public Transform thight_right;
	public Transform thight_left;
	public Transform shin_right;
	public Transform shin_left;
	public Transform shoulder_right;
	public Transform shoulder_left;
	public Transform arm_upper_right;
	public Transform arm_upper_left;
	public Transform arm_lower_right;
	public Transform arm_lower_left;
	public Transform shield;
	public Transform sword;
	
	GameObject[] pieces = new GameObject[16];
	
	public void assambleRandomCharacter(){
		foreach(GameObject g in pieces){
			DestroyImmediate(g);
		}
		pieces[0] = (GameObject) Instantiate(CharacterDatabase.getHead(),head.transform.position,head.transform.rotation);
		pieces[0].transform.parent = head;
		
		pieces[1] = (GameObject)Instantiate(CharacterDatabase.getHelmet(),head.transform.position,head.transform.rotation);
		pieces[1].transform.parent = head;
		
		pieces[2] = (GameObject)Instantiate(CharacterDatabase.getTorso(),torso.transform.position,torso.transform.rotation);
		pieces[2].transform.parent = torso;
		
		pieces[3] = (GameObject)Instantiate(CharacterDatabase.getStomage(),stomage.transform.position,stomage.transform.rotation);
		pieces[3].transform.parent = stomage;
		
		pieces[4] = (GameObject)Instantiate(CharacterDatabase.getThigtRight(),thight_right.transform.position,thight_right.transform.rotation);
		pieces[4].transform.parent = thight_right;
		
		pieces[5] = (GameObject)Instantiate(CharacterDatabase.getThigtLeft(),thight_left.transform.position,thight_left.transform.rotation);
		pieces[5].transform.parent = thight_left;
		
		pieces[6] = (GameObject)Instantiate(CharacterDatabase.getShinRight(),shin_right.transform.position,shin_right.transform.rotation);
		pieces[6].transform.parent = shin_right;
		
		pieces[7] = (GameObject)Instantiate(CharacterDatabase.getShinLeft(),shin_left.transform.position,shin_left.transform.rotation);
		pieces[7].transform.parent = shin_left;
		
		pieces[8] = (GameObject)Instantiate(CharacterDatabase.getShoulderRight(),shoulder_right.transform.position,shoulder_right.transform.rotation);
		pieces[8].transform.parent = shoulder_right;
		
		pieces[9] = (GameObject)Instantiate(CharacterDatabase.getShoulderLeft(),shoulder_left.transform.position,shoulder_left.transform.rotation);
		pieces[9].transform.parent = shoulder_left;
		
		pieces[10] = (GameObject)Instantiate(CharacterDatabase.getArmUpperRight(),arm_upper_right.transform.position,arm_upper_right.transform.rotation);
		pieces[10].transform.parent = arm_upper_right;
		
		pieces[11] = (GameObject)Instantiate(CharacterDatabase.getArmUpperLeft(),arm_upper_left.transform.position,arm_upper_left.transform.rotation);
		pieces[11].transform.parent = arm_upper_left;
		
		pieces[12] = (GameObject)Instantiate(CharacterDatabase.getArmLowerRight(),arm_lower_right.transform.position,arm_lower_right.transform.rotation);
		pieces[12].transform.parent = arm_lower_right;
		
		pieces[13] = (GameObject)Instantiate(CharacterDatabase.getArmLowerLeft(),arm_lower_left.transform.position,arm_lower_left.transform.rotation);
		pieces[13].transform.parent = arm_lower_left;
		
		pieces[14] = (GameObject)Instantiate(CharacterDatabase.getShield(),shield.transform.position,shield.transform.rotation);
		pieces[14].transform.parent = shield;
		
		pieces[15] = (GameObject)Instantiate(CharacterDatabase.getSword(),sword.transform.position,sword.transform.rotation);
		pieces[15].transform.parent = sword;
	}
}
