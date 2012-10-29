using UnityEngine;
using System.Collections;

public class BWeapon : MonoBehaviour {
	
	public Transform right;
	public Transform left;
	
	public GameObject rightWeapon;
	public GameObject leftWeapon;
	
	void Start () {
		if(rightWeapon){
		rightWeapon.transform.parent = right;
		rightWeapon.transform.localPosition = Vector3.zero;
		rightWeapon.transform.localRotation = Quaternion.identity;
		}
		
		if(leftWeapon){
		leftWeapon.transform.parent = left;
		leftWeapon.transform.localPosition = Vector3.zero;
		leftWeapon.transform.localRotation = Quaternion.identity;
		}
	}
	
	void Update () {
	
	}
}
