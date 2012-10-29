using UnityEngine;
using System.Collections;

public abstract class CameraState{
	public abstract CameraState ControllCamera(Transform c, Transform hingeUp, Transform hingeRight, Vector3 offset,float stateTime);
}
