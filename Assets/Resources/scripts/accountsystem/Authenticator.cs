using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Authenticator  {
	
	private static Authenticator _instance = new Authenticator();
	
	private bool _success = false;
	private bool _finished = false;
	private string _errorMessage = "";
	
	private string _response = "";
	private string _username = "";
	
	public character selectedChar;
	
	
	public string quest_state = "";
	
	public struct character
	{
		public string name;
		public int maxWater;
		public int id;
		public int world;
	}
	
	public LinkedList<character> _character = new LinkedList<character>();
	
	public static Authenticator getInstance() 
	{
		return _instance;	
	}
	
	
	private string _registerAddress = "http://www.uniworlds.de/caravan_test/registerUser.php";
	private string _loginAddress = "http://www.uniworlds.de/caravan_test/login.php";
	private string _checkLoginAddress = "http://www.uniworlds.de/caravan_test/checkLogin.php";
	private string _createCharacterAddress = "http://www.uniworlds.de/caravan_test/registerCharacter.php";
	private string _getCharactersAddress = "http://www.uniworlds.de/caravan_test/getCharacters.php";
	private string _updateCharactersAddress = "http://www.uniworlds.de/caravan_test/updateCharacter.php";
	private string _getQuestAdress = "http://www.uniworlds.de/caravan_test/getOrCreateCommunityQuest.php";
	private string _updateQuestAdress = "http://www.uniworlds.de/caravan_test/updateCommunityQuest.php";
	
	
	public string getResponse()
	{
		return _response;	
	}
	
	public string getUsername()
	{
		return _username;
	}
	
	
	public IEnumerator register(AccountData data)
	{
		_success = false;
		_finished = false;
		
		string url = _registerAddress+ "?user=" + data.username + "&mail=" + data.mailAdress + "&pw=" + data.pwhash;
		
		
		WWW hs_post = new WWW(url);
		yield return hs_post;
		
		if(hs_post.text == "")
		{
			_success = true;
			_finished = true;
		}
		else
		{
			_success = false;
			_finished = true;
			_errorMessage = hs_post.text;
		}
	}
	
	public IEnumerator login(AccountData data)
	{
		_finished = false;
		
		string url = _loginAddress+ "?user=" + data.username;
		
		
		WWW hs_post = new WWW(url);
		yield return hs_post;
		
		
		if(hs_post.text == "Username not registered")
		{
			_success = false;
			_finished = true;
			_errorMessage = "Username not registered";
		}
		else 
		{
			
			
			//We got the challenge, lets solve it
			System.Security.Cryptography.SHA512 hasher = new System.Security.Cryptography.SHA512Managed();
			
			string toBeHashed = hs_post.text + data.pwhash;
			byte[] hash = hasher.ComputeHash(System.Text.Encoding.ASCII.GetBytes(toBeHashed));
			string response = AccountData.toPHPHash(hash);
			
			
			//Check if the login is correct
			url = _checkLoginAddress+ "?user=" + data.username + "&response=" +  response;
			hs_post = new WWW(url);
			
			
			yield return hs_post;
			
			
			if(hs_post.text == "Login successful")
			{
				//We have a correct response lets save it
				
				_response = response;
				_username = data.username;
				
				
				_success = true;
				_finished = true;
			}
			else
			{
				_success = false;
				_finished = true;
				_errorMessage = hs_post.text;	
			}
			
			
		}
	}
	
	public IEnumerator createCharacter(string name)
	{
		
		_success = false;
		_finished = false;
		
		string url = _createCharacterAddress + "?username=" + _username +  "&response=" + _response + "&name=" + name;
		
		
		WWW hs_post = new WWW(url);
		yield return hs_post;
		
		if(hs_post.text == "Cant get character list because you are not logged in!")
		{
			_success = false;
			_finished = true;
			_errorMessage = hs_post.text;
		}
		else
		{
			_success = true;
			_finished = true;
			
		}
	}
	
	public IEnumerator getCharacters()
	{
		
		_success = false;
		_finished = false;
		
		string url = _getCharactersAddress + "?username=" + _username +  "&response=" + _response;
		
		
		WWW hs_post = new WWW(url);
		yield return hs_post;
		
		
		if(hs_post.text == "Cant get character list because you are not logged in!")
		{
			_success = false;
			_finished = true;
			_errorMessage = hs_post.text;
		}
		else
		{
			string[] chars = hs_post.text.Split(' ');
			for(int i=0;i<chars.Length-1;i++)
			{
				string c = chars[i].Substring(1,chars[i].Length -2 );	
				
				string[] stats   = c.Split(',');
				
				character c1;
				c1.id = int.Parse(stats[0]);
				c1.name = stats[1];
				c1.maxWater = int.Parse(stats[2]);
				c1.world = int.Parse(stats[3]);
				
				
				
			
				
				_character.AddLast(c1);
				
			}
		}
	}
	
	public IEnumerator updateCharacter()
	{
		
		
		
		_success = false;
		_finished = false;
		
		string url = _updateCharactersAddress + "?username=" + _username +  "&response=" + _response + "&char_id=" + selectedChar.id + "&water=" + selectedChar.maxWater;
		
		
		WWW hs_post = new WWW(url);
		yield return hs_post;
		
		
		if(hs_post.text == "Cant update character because you are not logged in!")
		{
			_success = false;
			_finished = true;
			_errorMessage = hs_post.text;
		}
		else
		{
			_success = true;
			_finished = true;
		}
	}
	
	public IEnumerator getQuestState(string questname)
	{
		
		_success = false;
		_finished = false;
		
		string url = _getQuestAdress + "?username=" + _username +  "&response=" + _response + "&world_id=1&quest_name=" + questname;
		
		
		WWW hs_post = new WWW(url);
		yield return hs_post;
		
		
		if(hs_post.text == "Cant get community quest because you are not logged in!")
		{
			_success = false;
			_finished = true;
			_errorMessage = hs_post.text;
		}
		else
		{
			quest_state = hs_post.text;
			_success = true;
			_finished = true;
		}
		yield return true;
	}
	
	public IEnumerator updateQuestState(string questname,string state)
	{
		_success = false;
		_finished = false;
		
		WWWForm data = new WWWForm();
		data.AddField("username",_username);
		data.AddField("response",_response);
		data.AddField("world_id","1");
		data.AddField("quest_state",state);
		data.AddField("quest_name",questname);
	
		
		
		WWW hs_post = new WWW(_updateQuestAdress,data);
		yield return hs_post;
		
	
		
		if(hs_post.text == "Cant update community quest because you are not logged in!")
		{
			_success = false;
			_finished = true;
			_errorMessage = hs_post.text;
		}
	}
	
	public bool hasFinished()
	{
		return _finished;	
	}
	
	public bool hasSuccessed()
	{
		return _success;	
	}
	
	public string getError()
	{
		return _errorMessage;
	}
	
	public void reset()
	{
		_success = false;
		_finished = false;
	}
	
	
}
