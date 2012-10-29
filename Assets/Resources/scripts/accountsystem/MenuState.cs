///// <summary>
///// Fabian: Added pageOffset variable for animated transitions.
///// 		blame me if it sucks!
///// </summary>
//
//using UnityEngine;
//using System.Collections;
//
//public abstract class MenuState : MonoBehaviour
//{	
//	private GUISkin _guiStyle;
//	private GUISkin _guiSkin;
//	
//	//public Vector2 pageOffset;
//	public abstract MenuState renderGUI(Vector2 pageOffset, GUISkin skin);
//}
//
//public class MainMenuState : MenuState
//{
//	public override MenuState renderGUI(Vector2 pageOffset, GUISkin skin)
//	{
//		if(GUI.Button(new Rect(0 + pageOffset.x ,120 + pageOffset.y,100,30),"Register",skin))
//		{
//			Authenticator.getInstance().reset();
//			return MenuManager.register;
//		}
//		
//		if(GUI.Button(new Rect(0 + pageOffset.x ,160 + pageOffset.y,100,30),"Login",skin))
//		{
//			Authenticator.getInstance().reset();
//			return MenuManager.login;
//		}
//		
//		return this;
//	}
//}
//
//public class RegisterMenuState : MenuState
//{
//	private string _username = "";
//	private string _password = "";
//	private string _mail = "";
//	private string _error = "";
//	
//	
//	public override MenuState renderGUI(Vector2 pageOffset , GUISkin skin)
//	{
//			 GUI.Label(new Rect(0 + pageOffset.x, 0 + pageOffset.y, 100, 40), "Username: ",skin);
//        	_username = GUI.TextField(new Rect(70 + pageOffset.x, 0 + pageOffset.y, 100, 20), _username,skin);
//        	GUI.Label(new Rect(0 + pageOffset.x, 40+ pageOffset.y, 100 , 40), "Password: ",skin);
//        	_password = GUI.PasswordField(new Rect(70 + pageOffset.x, 40 + pageOffset.y, 100, 20), _password, '*',skin);
//			GUI.Label(new Rect(0 + pageOffset.x, 80 + pageOffset.y, 100, 40), "Mail(optional): ",skin);
//        	_mail = GUI.TextField(new Rect(90 + pageOffset.x, 80 + pageOffset.y, 100, 20), _mail,skin);
//			
//			if(GUI.Button(new Rect(120 + pageOffset.x,120 + pageOffset.y,100,30),"Register",skin))
//			{
//				AccountData data = new AccountData(_username,_password,_mail);
//				StartCoroutine(Authenticator.getInstance().register(data));
//			}
//			
//			if(GUI.Button(new Rect(0 + pageOffset.x,120 + pageOffset.y,100,30),"Back",skin))
//			{
//				_error = "";
//				Authenticator.getInstance().reset();
//				return MenuManager.main;
//			}
//			
//			GUI.Label(new Rect(0 + pageOffset.x, 160 + pageOffset.y, 200, 40), _error,skin);
//		
//			if(Authenticator.getInstance().hasFinished())
//			{
//				if(Authenticator.getInstance().hasSuccessed())
//				{
//					_error = "";
//					Authenticator.getInstance().reset();
//					return MenuManager.main;
//				}
//				else
//				{
//					_error = Authenticator.getInstance().getError();
//					Authenticator.getInstance().reset();
//				}
//			}
//		
//		return this;
//	}
//}
//
//public class LoginMenuState : MenuState
//{
//	private string _username = "";
//	private string _password = "";
//	private string _mail = "";
//	private string _error = "";
//	
//	public override MenuState renderGUI(Vector2 pageOffset , GUISkin skin)
//	{
//			GUI.Label(new Rect(0 + pageOffset.x, 0+ pageOffset.y, 100, 40), "Username: ",skin);
//        	_username = GUI.TextField(new Rect(70+ pageOffset.x, 0, 100+ pageOffset.y, 20), _username,skin);
//        	GUI.Label(new Rect(0 + pageOffset.x, 40+ pageOffset.y, 100, 40), "Password: ",skin);
//        	_password = GUI.PasswordField(new Rect(70+ pageOffset.x, 40+ pageOffset.y, 100, 20), _password, '*',skin);
//			
//			if(GUI.Button(new Rect(120 + pageOffset.x,160+ pageOffset.y,100,30),"Login",skin))
//			{
//				AccountData data = new AccountData(_username,_password,_mail);
//				StartCoroutine(Authenticator.getInstance().login(data));
//			}
//		
//			if(GUI.Button(new Rect(0 + pageOffset.x,160+ pageOffset.y,100,30),"Back",skin))
//			{
//				_error = "";
//				Authenticator.getInstance().reset();
//				return MenuManager.main;
//			}
//			
//			GUI.Label(new Rect(0 + pageOffset.x, 200+ pageOffset.y, 200, 40), _error,skin);
//		
//			if(Authenticator.getInstance().hasFinished())
//			{
//				if(Authenticator.getInstance().hasSuccessed())
//				{
//					_error = "";
//					Debug.Log(_error);
//					Authenticator.getInstance().reset();
//					return MenuManager.charSelection;
//				}
//				else
//				{
//					
//					_error = Authenticator.getInstance().getError();
//					Debug.Log(_error);
//					Authenticator.getInstance().reset();
//				}
//			}
//		
//		return this;
//	}
//}
//
//public class ConnectMenuState : MenuState
//{
//	private string _gameName = "My Game";
//	private int _port = 25002;
//	
//	public override MenuState renderGUI(Vector2 pageOffset , GUISkin skin)
//	{
//		_gameName = GUI.TextField(new Rect(110 + pageOffset.x, 0 + pageOffset.y, 100, 30), _gameName);
//		_port = int.Parse(GUI.TextField(new Rect(230 + pageOffset.x, 0 + pageOffset.y, 100, 30), _port.ToString()));
//		
//		if(GUI.Button(new Rect(0 + pageOffset.x,0 + pageOffset.y,100,30),"Create Server",skin))
//		{
//			Application.LoadLevel("mainscene");
//			Network.InitializeServer(20,_port,true);
//			MasterServer.RegisterHost("Caravan",_gameName);
//			
//			
//		}
//		if(GUI.Button(new Rect(0 + pageOffset.x,40 + pageOffset.y,100,30),"Pull Gamelist",skin))
//		{
//			MasterServer.RequestHostList("Caravan");
//		}
//		
//		HostData[] data = MasterServer.PollHostList();
//		for(int i=0;i<data.Length;i++)
//		{
//			GUI.Label(new Rect(110 + pageOffset.x, 80+40*i + pageOffset.y, 100, 30), data[i].gameName);
//			if(GUI.Button(new Rect(200 + pageOffset.x,80+40*i + pageOffset.y,100,20),"Connect",skin))
//			{
//				Application.LoadLevel("mainscene");
//				Network.Connect(data[i]);
//				
//			}
//		}
//		
//		
//		return this;
//	}
//}
//
//public class CharacterSelectionMenuState : MenuState
//{
//	
//	public override MenuState renderGUI(Vector2 pageOffset , GUISkin skin)
//	{
//		
//		if(GUI.Button(new Rect(120+ pageOffset.x,80+ pageOffset.y,150,30),"Get characters",skin))
//		{
//			StartCoroutine(Authenticator.getInstance().getCharacters());	
//		}
//		
//		int i = 0;
//		foreach(Authenticator.character c in Authenticator.getInstance()._character)
//		{
//			GUI.Label(new Rect(110+ pageOffset.x, 20*i+ pageOffset.y, 100, 20), c.name);
//			if(GUI.Button(new Rect(200+ pageOffset.x,20*i+ pageOffset.y,100,20),"Select",skin))
//			{
//				Authenticator.getInstance().selectedChar = c;
//				Debug.Log("world: " + Authenticator.getInstance().selectedChar.world);
//				
//			
//				return MenuManager.connect;
//			}
//			i++;
//		}
//		
//		
//		
//		
//		if(GUI.Button(new Rect(120+ pageOffset.x,120+ pageOffset.y,150,30),"Create new character",skin))
//		{
//			return MenuManager.charCreate;	
//		}	
//		
//		if(GUI.Button(new Rect(0 + pageOffset.x,120+ pageOffset.y,100,30),"Back",skin))
//		{
//
//			return MenuManager.login;
//		}
//		
//		return this;
//	}
//}
//
//public class CharacterCreationMenuState : MenuState
//{
//	private string _charName = "";
//	private string _error = "";
//	
//	public override MenuState renderGUI(Vector2 pageOffset , GUISkin skin)
//	{
//		
//		GUI.Label(new Rect(0 + pageOffset.x, 0 + pageOffset.y, 100, 40), "Name: ",skin);
//        _charName = GUI.TextField(new Rect(40 + pageOffset.x, 0 + pageOffset.y, 100, 20), _charName,skin);
//		
//		
//		if(GUI.Button(new Rect(120 + pageOffset.x,120 + pageOffset.y,150,30),"Create character",skin))
//		{
//			StartCoroutine(Authenticator.getInstance().createCharacter(_charName));
//		}
//		
//		if(GUI.Button(new Rect(0 + pageOffset.x,120 + pageOffset.y,100,30),"Back",skin))
//		{
//
//			return MenuManager.charSelection;
//		}
//		
//		GUI.Label(new Rect(0 + pageOffset.x, 200 + pageOffset.y, 200, 40), _error,skin);
//		
//		if(Authenticator.getInstance().hasFinished())
//		{
//			if(Authenticator.getInstance().hasSuccessed())
//			{
//				_error = "";
//				Authenticator.getInstance().reset();
//				return MenuManager.charSelection;
//			}
//			else
//			{
//				_error = Authenticator.getInstance().getError();
//				Authenticator.getInstance().reset();
//			}
//		}
//		
//		return this;
//	}
//}
//
//public class NoneState : MenuState
//{
//
//	public override MenuState renderGUI(Vector2 pageOffset , GUISkin skin)
//	{
//		return this;
//	}
//}

using UnityEngine;
using System.Collections;

public abstract class MenuState : MonoBehaviour
{	
	private GUISkin _guiStyle;
	private GUISkin _guiSkin;
	
	//public Vector2 pageOffset;
	public abstract MenuState renderGUI(Vector2 pageOffset, GUISkin skin);
}

public class MainMenuState : MenuState
{
	public override MenuState renderGUI(Vector2 pageOffset, GUISkin skin)
	{
//		if(GUI.Button(new Rect(0 + pageOffset.x ,120 + pageOffset.y,100,30),"Register",skin))
//		{
//			Authenticator.getInstance().reset();
//			return MenuManager.register;
//		}
//		
//		if(GUI.Button(new Rect(0 + pageOffset.x ,160 + pageOffset.y,100,30),"Login",skin))
//		{
//			Authenticator.getInstance().reset();
//			return MenuManager.login;
//		}
		GUIStyle style = new GUIStyle(skin.GetStyle("label"));
		style.fontSize = 90;
		GUI.DrawTexture(new Rect(MenuManager.field_offset.x + pageOffset.x, 100 + pageOffset.y, 600, 125), (Texture)Resources.Load("textures/GUI/caravan_title", typeof(Texture)));

		return MenuManager.none;
	}
}

public class RegisterMenuState : MenuState
{
	private string _username = "";
	private string _password = "";
	private string _mail = "";
	private string _error = "";
	
	
	public override MenuState renderGUI(Vector2 pageOffset , GUISkin skin)
	{
		
			GUIStyle style_text_field = new GUIStyle(skin.GetStyle("textfield"));
			GUIStyle style_lable = new GUIStyle(skin.GetStyle("label"));

			 GUI.Label(new Rect(MenuManager.field_offset.x+ pageOffset.x, 0 + pageOffset.y + MenuManager.field_offset_height, MenuManager.field_width, MenuManager.field_height), "Username: ",style_lable);
        	_username = GUI.TextField(new Rect(MenuManager.field_offset.x + MenuManager.field_width + pageOffset.x, 0 + pageOffset.y + MenuManager.field_offset_height, MenuManager.field_width, MenuManager.field_height), _username,style_text_field);
        	GUI.Label(new Rect(MenuManager.field_offset.x + pageOffset.x, MenuManager.field_offset.y + pageOffset.y + MenuManager.field_offset_height, MenuManager.field_width , MenuManager.field_height), "Password: ",style_lable);
        	_password = GUI.PasswordField(new Rect(MenuManager.field_offset.x +MenuManager.field_width + pageOffset.x, MenuManager.field_offset.y + pageOffset.y+ MenuManager.field_offset_height, MenuManager.field_width, MenuManager.field_height), _password, '*',style_text_field);
			GUI.Label(new Rect(MenuManager.field_offset.x + pageOffset.x, MenuManager.field_offset.y*2 + pageOffset.y+ MenuManager.field_offset_height, MenuManager.field_width, MenuManager.field_height), "Mail(optional): ",style_lable);
        	_mail = GUI.TextField(new Rect(MenuManager.field_offset.x + MenuManager.field_width + pageOffset.x, MenuManager.field_offset.y*2 + pageOffset.y+ MenuManager.field_offset_height, MenuManager.field_width, MenuManager.field_height), _mail,style_text_field);
			
			if(GUI.Button(new Rect(MenuManager.field_offset.x + pageOffset.x,MenuManager.field_offset.y*4 + pageOffset.y+ MenuManager.field_offset_height,MenuManager.field_width,MenuManager.field_height),"Register",style_lable))
			{
				AccountData data = new AccountData(_username,_password,_mail);
				StartCoroutine(Authenticator.getInstance().register(data));
			}
			
//			if(GUI.Button(new Rect(0 + pageOffset.x,120 + pageOffset.y,MenuManager.field_width,MenuManager.field_height),"Back",skin))
//			{
//				_error = "";
//				Authenticator.getInstance().reset();
//				return MenuManager.main;
//			}
			
			GUI.Label(new Rect(MenuManager.field_offset.x + MenuManager.field_width + pageOffset.x, MenuManager.field_offset.y*4 + pageOffset.y+ MenuManager.field_offset_height - MenuManager.field_height, MenuManager.field_width, MenuManager.field_height*2), _error,style_lable);
		
			if(Authenticator.getInstance().hasFinished())
			{
				if(Authenticator.getInstance().hasSuccessed())
				{
					_error = "";
					Authenticator.getInstance().reset();
					return MenuManager.login;
				}
				else
				{
					_error = Authenticator.getInstance().getError();
					Authenticator.getInstance().reset();
					return MenuManager.none;
				}
			}
		
		return MenuManager.none;
	}
}

public class LoginMenuState : MenuState
{
	private string _username = "";
	private string _password = "";
	private string _mail = "";
	private string _error = "";
	
	public override MenuState renderGUI(Vector2 pageOffset , GUISkin skin)
	{
		
			GUIStyle style_text_field = new GUIStyle(skin.GetStyle("textfield"));
			GUIStyle style_lable = new GUIStyle(skin.GetStyle("label"));
		
			GUI.Label(new Rect(MenuManager.field_offset.x + pageOffset.x, 0 + pageOffset.y+ MenuManager.field_offset_height, MenuManager.field_width, MenuManager.field_height), "Username: ",style_lable);
        	_username = GUI.TextField(new Rect(MenuManager.field_offset.x + MenuManager.field_width + pageOffset.x, 0 + MenuManager.field_offset_height, MenuManager.field_width+ pageOffset.y, MenuManager.field_height), _username,style_text_field);
        	GUI.Label(new Rect(MenuManager.field_offset.x + pageOffset.x, MenuManager.field_offset.y+ pageOffset.y+ MenuManager.field_offset_height, MenuManager.field_width, MenuManager.field_height), "Password: ",style_lable);
        	_password = GUI.PasswordField(new Rect(MenuManager.field_offset.x + MenuManager.field_width+ pageOffset.x, MenuManager.field_offset.y+ pageOffset.y + MenuManager.field_offset_height, MenuManager.field_width, MenuManager.field_height), _password, '*',style_text_field);
			
			if(GUI.Button(new Rect(MenuManager.field_offset.x + pageOffset.x,MenuManager.field_offset.y*4+ pageOffset.y+ MenuManager.field_offset_height,MenuManager.field_width,MenuManager.field_height),"Login",style_lable))
			{
				AccountData data = new AccountData(_username,_password,_mail);
				StartCoroutine(Authenticator.getInstance().login(data));
			}
		
//			if(GUI.Button(new Rect(0 + pageOffset.x,160+ pageOffset.y,MenuManager.field_width,MenuManager.field_height),"Back",skin))
//			{
//				_error = "";
//				Authenticator.getInstance().reset();
//				return MenuManager.main;
//			}
			
			GUI.Label(new Rect(MenuManager.field_offset.x + MenuManager.field_width + pageOffset.x, MenuManager.field_offset.y*4+ pageOffset.y+ MenuManager.field_offset_height - MenuManager.field_height, MenuManager.field_width, MenuManager.field_height*2), _error,style_lable);
		
			if(Authenticator.getInstance().hasFinished())
			{
				if(Authenticator.getInstance().hasSuccessed())
				{
					_error = "";
					Authenticator.getInstance().reset();
					Application.LoadLevel("main_construction_scene");
				}
				else
				{
					
					_error = Authenticator.getInstance().getError();
					Authenticator.getInstance().reset();
				}
			}
		
		return MenuManager.none;
	}
}


public class NoneState : MenuState
{

	public override MenuState renderGUI(Vector2 pageOffset , GUISkin skin)
	{
		return this;
	}
}
