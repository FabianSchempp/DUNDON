using UnityEditor;
using UnityEngine;
using System.Collections;
using System.IO;


public class FindUnsedMaterials : EditorWindow {

	[MenuItem("UniworldTools/FindUnsedMaterials")]
    static void ShowWindow () {
		
		Searcher searcher = new Searcher();
    	searcher.search(Application.dataPath);
		//searcher.search("D:/uniworlds/svn/Caravan/project/caravan/Assets/Resources/models/enviroment/");
		searcher.printResult();
		
	
    }
	
	class Searcher {
		
		int lenght = Application.dataPath.Length - 6; 
		
		string[] modelExtensions = {"blend","fbx"};
		string[] materialExtensions = {"mat"};
		
		
		System.Collections.Generic.LinkedList<string> usedMaterials;
		System.Collections.Generic.LinkedList<string> materials;
		System.Collections.Generic.LinkedList<string> modelsWithoutMaterial;
		
		public Searcher() {
			usedMaterials = new System.Collections.Generic.LinkedList<string>();
			materials = new System.Collections.Generic.LinkedList<string>();
			modelsWithoutMaterial = new System.Collections.Generic.LinkedList<string>();	
		}
		
		
		public void search(string path) {
			
		
			
			
			string[] dirs = Directory.GetDirectories(path);
			foreach(string s in dirs) {
				checkDirForModels(s);
				
				//Serach subdirectories
				search(s);	
			}
			
	
		}
		
		public void checkDirForModels(string dirpath) {
			string[] files = Directory.GetFiles(dirpath);
			foreach(string f in files) {
				foreach(string ext in modelExtensions) {
					if(f.EndsWith(ext)) {
						
						GameObject obj = Resources.LoadAssetAtPath(f.Substring(lenght),typeof(GameObject)) as GameObject;
						MeshRenderer r = obj.GetComponent<MeshRenderer>();
						if(r != null) {
							foreach(Material m in r.sharedMaterials) {
								if(m != null) {
									usedMaterials.AddLast(m.name);
								}
							}
						}
						else {
							modelsWithoutMaterial.AddLast(f);	
						}
					}
				}
				foreach(string ext in materialExtensions) {
					if(f.EndsWith(ext)) {
						Material m = Resources.LoadAssetAtPath(f.Substring(lenght),typeof(Material)) as Material;
						materials.AddLast(m.name);
					}
				}
			}
			
		}
		
		public void printResult() {

			
			//Remove used materials
			foreach(string s in usedMaterials) {
				materials.Remove(s);
			}
			
			Debug.Log("Found " + materials.Count + " unused materials");
			foreach(string s in materials) {
				Debug.Log(s);	
			}
			
			Debug.Log("Found " + modelsWithoutMaterial.Count + " models without material");
			foreach(string s in modelsWithoutMaterial) {
				Debug.Log(s);	
			}
		}
	}
	
	
	

   
}

