using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class AwarenessManager{
	
	public delegate void UpdateAwarenessLevel(Vector3 position, float radius);
	
	private static List<UpdateAwarenessLevel> delegates = new List<UpdateAwarenessLevel>();
	
	
	public static void addObject(UpdateAwarenessLevel function)
	{
		delegates.Add(function);	
	}
	
	public static void removeObject(UpdateAwarenessLevel function)
	{
		delegates.Remove(function);	
	}
	
	public static void updateAwareness(Vector3 position, float radius)
	{
		foreach(UpdateAwarenessLevel d in delegates)
		{
			d(position, radius);	
		}
	}
	
}
