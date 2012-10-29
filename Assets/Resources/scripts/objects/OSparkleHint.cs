using UnityEngine;
using System.Collections;

public class OSparkleHint: MonoBehaviour {
	
	public GameObject sparkle_plane;
	private GameObject _sparkle;
	private GameObject _camera;
	private GameObject _player;
	
	public float offset_y = 0.0f;
	private float time = 0;
	#region inizialise
	// Use this for initialization
	void initialize () {
		_camera = GameObject.FindGameObjectWithTag("MainCamera");
		_player = GameObject.FindGameObjectWithTag("Player");
		set_sparkle();	
	}

	//initializes sparkle_plane
	void set_sparkle () {
		_sparkle = (GameObject)Instantiate(sparkle_plane,transform.position,transform.rotation);
	}
	
	
	void Start () {
		initialize();
	}
	#endregion
	
	#region update
	//updates transform and rotation
	void update_transform() {
		_sparkle.transform.position = new Vector3(transform.position.x, transform.position.y + offset_y, transform.position.z);
		
		//billboard to Camera
		_sparkle.transform.LookAt(new Vector3(_camera.transform.position.x,_sparkle.transform.position.y,_camera.transform.position.z));
		_sparkle.transform.Rotate(Vector3.right*270);
		_sparkle.transform.Rotate(Vector3.up*180);
		
		//rotate sparkle
		_sparkle.transform.Rotate(new Vector3(0,0,1)*200*time);
	
		//change size
		_sparkle.transform.localScale = Vector3.one*(Mathf.Max(0, 50- (50*Vector3.Distance(transform.position, _player.transform.position))));
		
		time += Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		update_transform();
	}
	
	void OnDisable () {
		Destroy(_sparkle);
	}
	#endregion
}
