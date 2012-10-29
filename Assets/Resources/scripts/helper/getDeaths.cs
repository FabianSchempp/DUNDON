using UnityEngine;
using System.Collections;

/*
 * Usage: Add this script to an terrain where you wish to draw green spots
 * Layer is the index of the terrain texture which should be used to draw the graves
 * radius is the radius of the circle which is drawn around a grave
 * 
*/
public class getDeaths : MonoBehaviour {
	
	public static string deathURL = "http://www.uniworlds.de/caravan/getDeaths.php";
	public float radius = 2.0f;
	public int layer = 1;
	public static System.Collections.Generic.HashSet <Vector3> pointsAlreadyAdded;
	
	IEnumerator Start()
	{
		getDeaths.pointsAlreadyAdded = new System.Collections.Generic.HashSet<Vector3>();
		return paintGraves();
		
		//position death star
		if (PlayerPrefs.HasKey("last_death_x") && PlayerPrefs.HasKey("last_death_y") && PlayerPrefs.HasKey("last_death_z")){
			Instantiate(Resources.Load("prefabs/GUI/death_star"),new Vector3(PlayerPrefs.GetFloat("last_death_x"),200,PlayerPrefs.GetFloat("last_death_z")),Quaternion.identity);
		}
	}
	
	public static IEnumerator refreshGraves()
	{		
		//position death star
		GameObject ds = GameObject.FindGameObjectWithTag("death_star");
		if (PlayerPrefs.HasKey("last_death_x") && PlayerPrefs.HasKey("last_death_y") && PlayerPrefs.HasKey("last_death_z") && ds != null){
			ds.transform.position = new Vector3(PlayerPrefs.GetFloat("last_death_x"),200,PlayerPrefs.GetFloat("last_death_z"));
		}
		
		return paintGraves();
	}
	
	public static IEnumerator paintGraves () {
			WWW hs_post = new WWW(getDeaths.deathURL);
			yield return hs_post;
			string[] positions = hs_post.text.Split(' ');
			
			foreach(string s in positions)
			{
				if(s != "")
				{
					string[] components = s.Split(',');
					Vector3 pos = new Vector3(float.Parse(components[1]),float.Parse(components[2]),float.Parse(components[3]));
					int id = int.Parse(components[0]);
					int pattern = 0;
				    if (components[4] != "") {
						pattern = int.Parse(components[4]);
					}
					//Debug.Log("grave_pattern: " + components[4]);
					//Only draw grave if the point wasnt already added
					if(pos.y < 51.46423 && pointsAlreadyAdded.Add(pos) )
					{
						Vector3 normal = Vector3.one;
						Vector3 position = pos;
						getDeaths.ray_cast_test(ref position,ref normal);
					
						GameObject gb = (GameObject)Instantiate(Resources.Load("prefabs/enviroment/grave_bag"),position,Quaternion.LookRotation(normal));
//						gb.GetComponent<EGravePattern>().initzialize(pattern);
//						gb.GetComponent<OGrave>().id = id;

					}
					
				}
			}
	}
	
	public static void ray_cast_test(ref Vector3 position, ref Vector3 normal){
		RaycastHit hit;
		//offset raytest position by max_step_height and raytest down
		Ray ray = new Ray(position + (Vector3.up * 20), Vector3.down);
		
		//layer_mask for ground
		int layer_mask = 1 << 11;
		
		if (Physics.Raycast(ray,out hit,100, layer_mask)){
			normal = hit.normal;
			position = hit.point;
		}
	}
	
//	//Flower planting
//	public Transform[] flowers;
//	public int minFlowers = 5;
//	public int maxFlowers = 15;
//	
//
//	public void plant(Vector3 position, float radius)
//	{
//	
//		int flower = Random.Range(minFlowers,maxFlowers);
//		
//		for(int i=0;i<flower;i++)
//		{
//			Vector2 rand_pos = Random.insideUnitCircle * radius;
//			Vector3 pos = new Vector3(rand_pos.x,0,rand_pos.y);
//			Instantiate(flowers[Random.Range(0,flowers.Length)],position + pos,Quaternion.identity);
//		}
//	}
	
	
	
	
}
