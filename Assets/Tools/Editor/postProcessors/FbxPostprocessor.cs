using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class FbxPostprocessor : AssetPostprocessor {
  	Dictionary<string,GameObject> objects = new Dictionary<string, GameObject>();
	
	void OnPreprocessModel() {
        ModelImporter importer = assetImporter as ModelImporter;
      	importer.splitAnimations = false;
		importer.importMaterials = false;
		importer.materialName = ModelImporterMaterialName.BasedOnMaterialName;
    }

	protected void OnPostprocessModel (GameObject processedGameObject){
	if(processedGameObject.name.ToLower().Contains("level")){
		HashSet <Transform>objectsToDelete = new HashSet<Transform>();
		
		GameObject snow = (GameObject) Resources.Load("prefabs/tiles/snow");
		GameObject cliff_high_corner_concave = (GameObject) Resources.Load("prefabs/tiles/cliff_high_corner_concave");
		GameObject cliff_high_corner_convex = (GameObject) Resources.Load("prefabs/tiles/cliff_high_corner_convex");
		GameObject cliff_high_straight = (GameObject) Resources.Load("prefabs/tiles/cliff_high_straigth");
		GameObject cliff_corner_concave = (GameObject) Resources.Load("prefabs/tiles/cliff_corner_concave");
		GameObject cliff_corner_convex = (GameObject) Resources.Load("prefabs/tiles/cliff_corner_convex");
		GameObject cliff_corner_round_left = (GameObject) Resources.Load("prefabs/tiles/cliff_corner_round_left");
		GameObject cliff_corner_round_right = (GameObject) Resources.Load("prefabs/tiles/cliff_corner_round_right");
		GameObject cliff_corner_round_ramp = (GameObject) Resources.Load("prefabs/tiles/cliff_corner_round_ramp");
		GameObject cliff_end_left = (GameObject) Resources.Load("prefabs/tiles/cliff_end_left");
		GameObject cliff_end_right = (GameObject) Resources.Load("prefabs/tiles/cliff_end_right");
		GameObject cliff_round_straigt = (GameObject) Resources.Load("prefabs/tiles/cliff_round_straight");
		GameObject cliff_straigt = (GameObject) Resources.Load("prefabs/tiles/cliff_straigth");
		GameObject pile = (GameObject) Resources.Load("prefabs/tiles/pile");
		GameObject tree = (GameObject) Resources.Load("prefabs/tiles/tree");
		GameObject tiles = (GameObject) Resources.Load("prefabs/tiles/tiles");
		GameObject tiles_edge = (GameObject) Resources.Load("prefabs/tiles/tiles_edge");
		GameObject tiles_corner = (GameObject) Resources.Load("prefabs/tiles/corner");
		GameObject skeleton_a = (GameObject) Resources.Load("prefabs/doodads/skeleton_a");
		GameObject skeleton_b = (GameObject) Resources.Load("prefabs/doodads/skeleton_b");
		GameObject black_tower = (GameObject) Resources.Load("prefabs/doodads/black_tower");
		GameObject floor = (GameObject) Resources.Load("prefabs/tiles/floor");
		GameObject bruecke = (GameObject) Resources.Load("prefabs/doodads/bruecke");

		objects.Add("snow",snow);
		objects.Add("cliff_straigt",cliff_straigt);
		objects.Add("cliff_corner_concave",cliff_corner_concave);
		objects.Add("cliff_corner_convex",cliff_corner_convex);
		objects.Add("cliff_high_corner_concave",cliff_high_corner_concave);	
		objects.Add("cliff_high_corner_convex",cliff_high_corner_convex);
		objects.Add("cliff_heigh_straigt",cliff_high_straight);
		objects.Add("cliff_corner_round_left", cliff_corner_round_left);
		objects.Add("cliff_corner_round_right",cliff_corner_round_right);
		objects.Add("cliff_corner_round_ramp",cliff_corner_round_ramp);
		objects.Add("cliff_end_left",cliff_end_left);
		objects.Add("cliff_end_right",cliff_end_right);
		objects.Add("cliff_round_strigth",cliff_round_straigt);
		objects.Add("pile",pile);
		objects.Add("tree",tree);
		objects.Add("tiles",tiles);
		objects.Add("floor",floor);
		objects.Add("skeleton_a",skeleton_a);
		objects.Add("skeleton_b",skeleton_b);
		objects.Add("bruecke",bruecke);
		objects.Add("black_tower",black_tower);
			
			
		Object prefab = Resources.LoadAssetAtPath(assetPath.Replace(".blend",".prefab"),typeof(GameObject));
		if(prefab == null){
			prefab = PrefabUtility.CreateEmptyPrefab(assetPath.Replace(".blend",".prefab"));	
		}
			
		GameObject newAsset = new GameObject(processedGameObject.name);
		GameObject root = new GameObject("root");
			
		Transform[] allChildren = processedGameObject.GetComponentsInChildren<Transform>();		
			foreach (Transform child in allChildren) {
				foreach(string s in objects.Keys){
					Debug.Log(s);
					if (child.name.ToLower().Contains(s.ToLower())) {
						objectsToDelete.Add(child);
						GameObject g = (GameObject) PrefabUtility.InstantiatePrefab(objects[s]);
						g.transform.position = child.position;
						g.transform.localScale = child.localScale;
						g.transform.rotation = child.rotation;
						g.transform.parent = root.transform;
						break;
					}
				}
				
			}
			
			while (objectsToDelete.Count != 0) {
				List <Transform> objects_to_delete_for_loop = new List <Transform> (objectsToDelete);
				foreach (Transform object_to_delete in objects_to_delete_for_loop) {
						objectsToDelete.Remove (object_to_delete);
						Object.DestroyImmediate(object_to_delete.gameObject, true);
				}
			}
			root.transform.parent = newAsset.transform;
			processedGameObject.tag = "Solid";
			processedGameObject.layer = 8;
			processedGameObject.transform.parent = newAsset.transform;
			PrefabUtility.ReplacePrefab(newAsset,prefab,ReplacePrefabOptions.ReplaceNameBased);
			
			GameObject.DestroyImmediate(newAsset);
		}
	}
}
