using UnityEngine;
using System.Collections;

//Class for storing the data needed to register a new account
public class AccountData
{
	public string username;
	public string mailAdress;
	public string pwhash;
	
	
	public static string toPHPHash(byte[] hash)
	{
		string s = System.BitConverter.ToString(hash);
		s = s.Replace("-","");
		s = s.ToLower();
		return s;
	}
	
	
	public AccountData(string username, string password, string mail_adress)
	{
		this.username = username;
		this.mailAdress = mail_adress;
		
		
		System.Security.Cryptography.SHA512 hasher = new System.Security.Cryptography.SHA512Managed();
		
		//Hash password using username as salt, do 5000 rounds;
		string toBeHashed = password + username;
		byte[] hash = new byte[64];	
		for(int i = 0; i< 5000; i++)
		{
			hash = hasher.ComputeHash(System.Text.Encoding.ASCII.GetBytes(toBeHashed));
			toBeHashed = System.Text.Encoding.ASCII.GetString(hash) + username;
		}
		pwhash = toPHPHash(hash);
	}
	
}
