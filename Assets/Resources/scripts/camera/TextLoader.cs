using System.Collections;
//using UnityEditor;
using UnityEngine;
using System.Xml;
using System.Xml.XPath;
using System.Collections.Generic;

public class TextLoader : MonoBehaviour {

	public static Dictionary<string,string> texts;
	
	public static void loadTexts(){
		texts = new Dictionary<string, string>();
		//parse XML
		XmlDocument xml = new XmlDocument();
		xml.Load("Assets\\Resources\\GUI\\texts\\texts.xml");
		
		foreach(XmlNode xml_element in xml.SelectSingleNode("text").ChildNodes){
			string key = xml_element.SelectSingleNode("key").InnerText;
			string _string = xml_element.SelectSingleNode("string").InnerText;
			texts.Add(key,_string);
			Debug.Log("loaded text: " + key + " " + _string);
		}
	}
}

