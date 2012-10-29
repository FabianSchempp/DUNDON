using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterInventory : MonoBehaviour {
	private List<Item> _items =  new List<Item>();
	public List<Item> items{
		get {return _items;}
		set {_items= value;}
	}
	
	private GameObject _inventoryRoot;
	private GameObject _inventoryInfo;
	private GameObject _inventoryChooser;
	private GameObject _rigidbodyItem;
	private Vector3 offsetRoot = new Vector3(-5,0.5f,0);
	private Vector3 offsetInfo = new Vector3(0,3.3f,0);
	private Vector3 offsetItems = new Vector3(0,2,0);
	
	private int _selectedItem;
	private List<GameObject> _inventoryItems = new List<GameObject>();
	private bool _visible = false;
	public bool visible{
		get {return _visible;}
	}
	private float _selection = 0;
	
	void Start(){
		_inventoryRoot = new GameObject("inventory");
		_inventoryInfo = (GameObject)GameObject.Instantiate((GameObject) Resources.Load("prefabs/GUI/InventoryInfo"));
		_inventoryInfo.transform.parent = _inventoryRoot.transform;
		_inventoryInfo.transform.localPosition = offsetInfo;
		_inventoryInfo.active = false;
		_inventoryChooser = (GameObject)GameObject.Instantiate((GameObject) Resources.Load("prefabs/GUI/InventoryChooser"));
		_inventoryChooser.transform.parent = _inventoryRoot.transform;
		_inventoryChooser.transform.localPosition = offsetItems;
		_inventoryChooser.active = false;
		_rigidbodyItem = (GameObject) Resources.Load("prefabs/items/IRawRigidbody");

	}
	void Update(){
		if(_visible && _inventoryItems.Count > 0 && _inventoryItems[0].active){
			_inventoryRoot.transform.position = transform.position + offsetRoot;
			for(int i=0;i < _inventoryItems.Count; i++){
				_inventoryItems[i].active = true;
				_inventoryItems[i].transform.localPosition = Vector3.Lerp(_inventoryItems[i].transform.localPosition, Quaternion.AngleAxis((360.0f/(float)_inventoryItems.Count)*((float)i - _selection),Vector3.right) * offsetItems,Time.deltaTime*20);
				_inventoryItems[i].transform.LookAt(Camera.main.transform.position);
				_inventoryItems[i].transform.localScale = Vector3.one * 3;
			}
			_selectedItem = Mathf.Clamp((int)_selection,0,_inventoryItems.Count-1);
//			_inventoryInfo.GetComponent<exSpriteFont>().text = _items[_selectedItem].name.ToUpper();
		}
		if(!_visible && _inventoryItems.Count > 0 && _inventoryItems[0].active){
			_inventoryRoot.transform.position = transform.position + offsetRoot;;
			for(int i=0;i < _inventoryItems.Count; i++){
				_inventoryItems[i].active = true;
				_inventoryItems[i].transform.localPosition = Vector3.Lerp(_inventoryItems[i].transform.localPosition, Quaternion.AngleAxis((360.0f/(float)_inventoryItems.Count)*((float)i - _selection),Vector3.right) * Vector3.up*20,Time.deltaTime*10);
				_inventoryItems[i].transform.LookAt(Camera.main.transform.position);
				_inventoryItems[i].transform.localScale = Vector3.one * 4;
				if(Time.timeSinceLevelLoad - counter > 0.5f){
					for(int y=0;y < _inventoryItems.Count; y++){
						_inventoryItems[y].active = false;
					}	
				}
			}
		}
	}
	
	public void AddItem (Item i) {
		//searches items for new item
		Item _existingItem = _items.Find(it => it.name == i.name);
		
		int existingItemIndex;
		existingItemIndex = _items.IndexOf(_existingItem);
		
		// if there some item of that kind already exist, only add value, else add the item
		if(_existingItem != null){
			_items[existingItemIndex].count += i.count;
		}
		else{
			GameObject t = (GameObject)GameObject.Instantiate(i.body);
			_inventoryItems.Add(t);
			t.transform.parent = _inventoryRoot.transform;
			_items.Add(i);
		}
	}
	public bool UseItem (Item i, int count) {
		//look for keys in items
		Item item = _items.Find(it => it.name == i.name);
		if (item != null && item.count >= count){
			_items[_items.IndexOf(i)].count -= count;
			if(_items[_items.IndexOf(i)].count <= 0){
				_items.RemoveAt(_items.IndexOf(i));
				Destroy(_inventoryItems[_items.IndexOf(i)]);
				_inventoryItems.RemoveAt(_items.IndexOf(i));
			}
			if(_items.Count <= 0){
				hideInventory();
			}
			return true;
		}
		return false;
	}
	public void UseItem () {
		_items[_selectedItem].count --;
		if(_items[_selectedItem].count <= 0){
			_items.RemoveAt(_selectedItem);
			Destroy(_inventoryItems[_selectedItem]);
			_inventoryItems.RemoveAt(_selectedItem);
		}
		if(_items.Count <= 0){
			hideInventory();
		}
	}
	public void dropItem(){
		GameObject itemIcon;
		ItemManager.itemList.TryGetValue(_items[_selectedItem].name,out itemIcon);
		GameObject rb = (GameObject) GameObject.Instantiate(_rigidbodyItem,transform.position + transform.forward * 2,Quaternion.identity);
		rb.GetComponent<OItem>().icon = itemIcon;
		rb.rigidbody.AddExplosionForce(100,transform.position,5);
		UseItem();
	}
	public bool showInventory(){
		if(_items.Count > 0){
			for(int i=0;i < _inventoryItems.Count; i++){
				_inventoryItems[i].active = true;
				_inventoryItems[i].transform.localPosition = Quaternion.AngleAxis((360.0f/(float)_inventoryItems.Count)*(float)i,Vector3.right) * Vector3.up*200;
				_inventoryItems[i].transform.LookAt(Camera.main.transform.position);
				_inventoryItems[i].transform.localScale = Vector3.one * 4;
			}
			
			_inventoryChooser.active = true;
			_inventoryInfo.active = true;
	//		Time.timeScale = 0.01f;
			_visible = true;
			return true;
		}
		return false;
	}
	public void hideInventory(){
		_inventoryInfo.active = false;
		_inventoryChooser.active = false;

		counter = Time.timeSinceLevelLoad;
		_visible = false;
	}
	public void toggleInventory(){
		if(_visible){
			hideInventory();
		}
		else{
			showInventory();
		}
	}
	
	float counter = 0;
	public void selectRight(){
		if(Time.timeSinceLevelLoad - counter > 0.3f){
			_selection = (_selection + 1);
			_selection = _selection > _inventoryItems.Count - 1 ? 0 : _selection;
			counter = Time.timeSinceLevelLoad;
		}
	}
	public void selectLeft(){
		if(Time.timeSinceLevelLoad - counter > 0.3f){
			_selection = (_selection - 1);
			_selection = _selection < 0 ? _inventoryItems.Count - 1 : _selection;

			counter = Time.timeSinceLevelLoad;
		}
	}
}

public class ItemManager{
	public static Dictionary<string,GameObject> itemList = new Dictionary<string, GameObject>();
}