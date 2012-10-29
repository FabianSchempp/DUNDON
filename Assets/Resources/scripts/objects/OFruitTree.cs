using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OFruitTree : MonoBehaviour {
	
	public static string createTreesURL = "http://www.uniworlds.de/caravan/createTree.php?name=";
	public static string getTreesURL = "http://uniworlds.de/caravan/getTrees.php";
	public static string getFruitsURL = "http://uniworlds.de/caravan/getFruits.php?name=";
	public static string consumeFruitsURL = "http://uniworlds.de/caravan/consumFruit.php?id=";
	
	public List<GameObject> fruit_holders;
	string name = "";
	public string tag = "mango";

	IEnumerator Start(){
		name = transform.position.x.ToString() + "," + transform.position.y.ToString() + "," + transform.position.z.ToString();
		return get_fruits();
	}
	
	public IEnumerator get_fruits () {
			WWW hs_post = new WWW(OFruitTree.getFruitsURL + name);
			yield return hs_post;
			string[] fruit_datas = hs_post.text.Split(' ');
			
			Debug.Log("fruit_string: " + hs_post.text);
				
			if(hs_post.text[0] != "<".ToCharArray()[0]){
				for(int i = 0; i < fruit_datas.Length; i++){
					string f = fruit_datas[i];
	
					if(f != "")
					{
						string[] components = f.Split(',');
						GameObject h = fruit_holders[i];
						string fruit_id = components[0];
						int fruit_age = int.Parse(components[1]);
						Debug.Log("fruit_age: " + fruit_age);
						if(fruit_age > 1000){
							GameObject new_fruit = (GameObject)Instantiate(Resources.Load("prefabs/items/fruit_a"), h.transform.position, Quaternion.identity);
							new_fruit.GetComponent<fruit>().id = fruit_id;
						}
					}
				}
			}
			//if tree does not exist create it!
			else{
				WWW create_tree = new WWW(createTreesURL+name+"&tag="+tag+"&y"+transform.position.y+"&x"+transform.position.x+"&z"+transform.position.z);
			}
	}
}