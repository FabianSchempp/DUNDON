using UnityEngine;
using System.Collections;
public class OItem : MonoBehaviour {
	public int points = 10;
	public int count;
	public GameObject icon;
	private Item _item;
	public ItemTypes type;
	public GameObject[] collectParticle;
	
	void Start () {
		switch (type){
		case ItemTypes.HeartShape:
			_item = new HeartShape();
			_item.name = "Ruby of Live";
		break;
		case ItemTypes.Gold:
			_item = new Item();
			_item.name = count.ToString() + " Gold";
			break;
		default:
			_item = new Item();
			_item.name = icon.name;
			break;
		}
		_item.count = count;
		_item.body = icon;
		if(!ItemManager.itemList.ContainsKey(_item.name)){
			ItemManager.itemList.Add(_item.name,icon);
		}
	}
	
	public Item collectItem(Controller controller) {
		GameObject.Instantiate(collectParticle[Random.Range(0,collectParticle.Length-1)],transform.position,transform.rotation);
		GUIManager.screenCastQuickInfo("found: " + _item.name );
		Destroy(gameObject);
		OScoreBar.AddScore(points);
		_item.OnCollect(controller);
		
		Destroy(this);
		return _item;
	}
}

public class Item{
	private string _name;
	private int _count;
	private GameObject _body;	
	
	public string name{
		get {return _name;}
		set {_name = value;}
	}
	public int count{
		get {return _count;}
		set {_count = value;}
	}
	public GameObject body{
		get {return _body;}
		set {_body = value;}
	}
	
	public virtual void OnCollect(Controller controller){
		
	}
	
	public virtual void OnUpdate(Controller controller){
		
	}
}

public class HeartShape : Item{
	public override void OnCollect(Controller controller){
		controller.stats.healthMax += 1;
	}
	public override void OnUpdate(Controller controller){
		
	}
}

public enum ItemTypes{
	Gold,
	HeartShape
}