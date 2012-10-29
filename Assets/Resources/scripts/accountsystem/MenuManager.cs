using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	
	
	public static MenuState main;
	public static MenuState register;
	public static MenuState login;
//	public static MenuState connect;
//	public static MenuState charSelection;
//	public static MenuState charCreate;
	
	public static int field_width = 300;
	public static int field_height = 60;
	public static int field_offset_height = 150;
	public static Vector2 field_offset = new Vector2(60,60);
	public static MenuState none;
	
	private int pageShift = 1;		
	private float page_shift_smoothness = 7f;
	private int offset = 0;
	private GUISkin _guiSkin;
	
	MenuState state;
	MenuState oldState;
	MenuState newState;

	// Use this for initialization
	void Start () {
		main = gameObject.AddComponent<MainMenuState>();
		register = gameObject.AddComponent<RegisterMenuState>();
		login = gameObject.AddComponent<LoginMenuState>();
//		connect = gameObject.AddComponent<ConnectMenuState>();
//		charSelection = gameObject.AddComponent<CharacterSelectionMenuState>();
//		charCreate = gameObject.AddComponent<CharacterCreationMenuState>();
		
		field_offset.x = Screen.width/6;
		
		none = gameObject.AddComponent<NoneState>();
		
		state = MenuManager.main;
		oldState = none;
		newState = none;
		pageShift = 1;
		offset = 0;
		
		_guiSkin = (GUISkin) Resources.Load("GUI/GUIstyle/custom_skin", typeof(GUISkin));
	}
	
	// Update is called once per frame
	void Update () {
		 calculate_page_shift();
	}
	
	void OnGUI()
	{
//		state = state.renderGUI(new Vector2(pageShift,0),_guiStyle);
		MenuState tmp_state;
		//renders shifted menu
		if (state != oldState) offset++;
		oldState = state;
//		oldState = oldState.renderGUI(new Vector2(pageShift - Screen.width,0),_guiStyle);
		tmp_state = oldState.renderGUI(new Vector2(pageShift - Screen.width,0),_guiSkin);
		if (tmp_state != MenuManager.none) state = tmp_state;
	}
	
	//calculate shift
	private void calculate_page_shift(){
		if (pageShift  <= (Screen.width - 10)){
			pageShift = (int)Mathf.Lerp(pageShift, offset * Screen.width, page_shift_smoothness*Time.deltaTime);	
		}
		else{
//			pageShift = 1;
//			offset = 0;
		}
	}
	
	public void action(){
		state = MenuManager.login;
		pageShift = 1;
		offset = 0;
	}
	
	public void awareness(){
		state = MenuManager.register;
		pageShift = 1;
		offset = 0;
	}
	
	public void boost(){
		Application.Quit();
	}
}
