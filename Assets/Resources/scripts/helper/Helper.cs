using UnityEngine;
using System.Collections;

public class Helper : MonoBehaviour {
	public static float flooredMagnitude(Vector3 original, Vector3 target){
		return Vector3.Magnitude(new Vector3(original.x, 0, original.z) - new Vector3(target.x, 0, target.z));
	}
	public static Vector3 floorVector3(Vector3 original ){
		return new Vector3(original.x, 0, original.z);
	}
	public static Vector3 floorVector3(Vector3 original, Vector3 reference){
		return new Vector3(original.x, reference.y, original.z);
	}
	public static Vector3 floorVector3Normalized(Vector3 original, Vector3 reference){
		return new Vector3(original.x - reference.x,0, original.z - reference.z);
	}
	public static Vector3 PlaneVector3(Vector3 original, Vector3 reference){
		return new Vector3(0, original.y - reference.y, Vector2.Distance(new Vector2(original.x, original.z),new Vector2(reference.x,reference.z)));
	}
	public static string raycastTestWall(Vector3 position, ref Vector3 normal, float length, Vector3 direction){
		RaycastHit hit;
		//offset raytest position by max_step_height and raytest down
		Ray ray = new Ray(position, direction);
		Debug.DrawLine(position,position+direction*length,Color.red);
		//layer_mask for ground
		int layer_mask = 1 << 8;
		
		if (Physics.Raycast(ray,out hit,Mathf.Max(0.01f,length), layer_mask)){
			if(Vector3.Distance(hit.point, position) < length){ 
				normal = hit.normal;
				return hit.collider.tag;
			}
			else{
				return "";
			}
		}
		else{
				return "";
			}
	}
	public static string raycastTestGround(ref Vector3 position, float height){
		RaycastHit hit;
		//offset raytest position by max_step_height and raytest down
		Ray ray = new Ray(position, Vector3.down);
		//layer_mask for ground
		int layer_mask = 1 << 8;
		
		if (Physics.Raycast(ray,out hit,Mathf.Max(0.01f,height), layer_mask)){
			if(Vector3.Distance(hit.point, position) < height){ 
				position = hit.point;
				return hit.collider.tag;
			}
		else{
				return "";
			}
		}
		else{
				return "";
			}
	}
	public static bool raycastTestOverHead(Vector3 position, float height){
		RaycastHit hit;
		//offset raytest position by max_step_height and raytest down
		Ray ray = new Ray(position, Vector3.up);
		
		//layer_mask for ground
		int layer_mask = 1 << 8;
		
		if (Physics.Raycast(ray,out hit,Mathf.Max(0.01f,height), layer_mask)){
				return true;
		}
		else{
				return false;
		}
	}
	public static string raycastPlaceGround(ref Vector3 position, out Vector3 normal){
		RaycastHit hit;
		float height = 10;
		//offset raytest position by max_step_height and raytest down
		Ray ray = new Ray(position + (Vector3.up * height), Vector3.down);
		
		//layer_mask for ground
		int layer_mask = 1 << 8;
		
		normal = Vector3.up;
		
		if (Physics.Raycast(ray,out hit,Mathf.Max(0.01f,height*2), layer_mask)){
			if(Vector3.Distance(hit.point, position) < height){ 
				position = hit.point;
				normal = hit.normal;
				return hit.collider.tag;
			}
			else{
				return "";
			}
		}
		else{
				return "";
			}
	}
	
	public static Vector3 lastMousePoint;
	public static string raycastTestMousePoint(){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		//layer_mask for ground
		int layer_mask = 1 << 8;
		
		if (Physics.Raycast(ray,out hit,Mathf.Max(0.01f,1000), layer_mask) && hit.point != Vector3.zero){
			lastMousePoint = hit.point;
			return hit.collider.tag;
		}
		return "";
	}	
	public static string raycastTestMousePoint(out Vector3 position){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		position = lastMousePoint;
		
		//layer_mask for ground
		int layer_mask = 1 << 8;
		
		if (Physics.Raycast(ray,out hit,Mathf.Max(0.01f,1000), layer_mask) && hit.point != Vector3.zero){
			position = hit.point;
			lastMousePoint = hit.point;
			return hit.collider.tag;
		}
		return "";
	}	
	public static bool raycastTestMousePoint(out Vector3 position, out Vector3 normal){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		position = lastMousePoint;
		normal = Vector3.zero;
		
		//layer_mask for ground
		int layer_mask = 1 << 8;
		
		if (Physics.Raycast(ray,out hit,Mathf.Max(0.01f,1000), layer_mask) && hit.point != Vector3.zero){
			position = hit.point;
			normal = hit.normal;
			lastMousePoint = hit.point;
			return true;
		}
		return false;
	}	
	
	public static bool isWithinRange(float v, float min,float max){
		if(v > min && v < max) return true;
		else return false;
	}
	public static bool isPointInFrontOfObject(GameObject reference, Vector3 point){
		Vector3 relativePoint = reference.transform.InverseTransformPoint(point);
		if (relativePoint.z > 0){
		    return true;
		}
		else{
		   return false;
		}
	}
	public static bool isPointRightOfObject(GameObject reference, Vector3 point){
		Vector3 relativePoint = reference.transform.InverseTransformPoint(point);
		if (relativePoint.x > 0){
		    return true;
		}
		else{
		   return false;
		}
	}
	public static string getRelativQuadPosition(GameObject reference, Vector3 point){
		Vector3 relativePoint = reference.transform.InverseTransformPoint(point);
		if (Mathf.Abs(relativePoint.x) > Mathf.Abs(relativePoint.z)){
		   	if (relativePoint.x > 0){
				return "right";
			}
			else{
				return "left";
			}
		}
		else{
		     if (relativePoint.z > 0){
				return "forward";
			}
			else{
				return "backward";
			}
		}
	}
	public static Vector3 getEdgePosition(Collider collider, Vector3 position){
		float length = collider.transform.localScale.x/2;
		Vector3 pos = collider.transform.position;
		Vector3 right = pos + collider.transform.right* length;
		Vector3 left = pos - collider.transform.right * length;
		float distance = Vector3.Distance(right,left);
		float distancePos = Vector3.Distance(right, position);
		return Vector3.Lerp(right,left,distancePos/distance);	
	}
	
	public static float getRelativAngleOnZ(GameObject reference, Vector3 point){
		Vector3 relativePoint = reference.transform.InverseTransformPoint(point);
		return Mathf.Atan2(relativePoint.y, relativePoint.x)*Mathf.Rad2Deg;
	}
	public static float getRelativAngleOnX(GameObject reference, Vector3 point){
		Vector3 relativePoint = reference.transform.InverseTransformPoint(point);
		return Mathf.Atan2(relativePoint.y, relativePoint.z)*Mathf.Rad2Deg;
	}
}
