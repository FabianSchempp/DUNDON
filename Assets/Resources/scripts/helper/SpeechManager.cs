using System.Collections;
using UnityEngine;
using System.Xml;
using System.Xml.XPath;
using System.Collections.Generic;

public class SpeechManager : MonoBehaviour {

//	public static Dictionary<string, Dialog> dialogs = new Dictionary<string, Dialog>();
//	public static Dialog _currentDialog;
//	public static float _lineTime = 0;
//	public static int _currentLine = 0;
//	public static lineType _currentLineType;
//	public static string _currentPose;
//	public static string _currentSpeaker;
//	public static GameObject _currentTextObject;
//	public static int currentLine{
//		get{return _currentLine;}
//		set{
//			if(_currentLine != value){
//				if(value != 0 && _currentDialog.lines[value] != null){
//					changeLine(value);
//				}
//			}
//			_lineTime = 0;
//			_currentLine = value;
//		}
//	}
//	public static float currentLineLength = 10;
//	public static Controller currentController;
//	private ControllState nextState;
//	private float durationPerWord = 0.5f;
//	
//	public static void loadTexts(){
//		//parse XML
//		XmlDocument xml = new XmlDocument();
//		xml.Load("Assets\\Resources\\GUI\\speeches\\speeches.xml");
//		
//		foreach(XmlNode xml_element in xml.SelectSingleNode("dialogs").ChildNodes){
//			
//			string key = xml_element.SelectSingleNode("key").InnerText;
//			dialogType _dialogType;
//			string dt = xml_element.SelectSingleNode("type").InnerText;
//			
//			if(dt == "dialogFreedanStayn"){
//				 _dialogType = dialogType.dialogFreedanStayn;
//			}
//			else{
//				_dialogType = dialogType.monolog;
//			}
//			
//			Dialog dialog = new Dialog();
//			dialog.type = _dialogType;
//			dialog.lines = new List<Line>();
//			dialog.Answers = new List<Answer>();
//			
//			foreach(XmlNode sub_element in xml_element.SelectSingleNode("lines")){
//				Line _line = new Line();
//				
//				lineType type = lineType.Answer;
//				if(sub_element.Attributes[2].Value == "question"){
//					type = lineType.Question;
//				}
//				else if(sub_element.Attributes[2].Value == "auto"){
//					type = lineType.Auto;
//				}
//				int answers = 0;
//				if(type == lineType.Question){
//					answers = (int)decimal.Parse(sub_element.Attributes[4].Value);
//				}
//				
//				_line.text = sub_element.InnerText;
//				_line.speaker = sub_element.Attributes[0].Value;
//				_line.pose = sub_element.Attributes[1].Value;
//				_line.type = type;
//				_line.jumpToLine = (int)decimal.Parse(sub_element.Attributes[3].Value);
//				_line.answers = answers;
//				
//				dialog.lines.Add(_line);
//			}
//			
//			foreach(XmlNode sub_element in xml_element.SelectSingleNode("answers")){
//				Answer answer = new Answer();
//				answer.good = sub_element.SelectSingleNode("good").InnerText;
//				answer.neutral = sub_element.SelectSingleNode("neutral").InnerText;
//				answer.bad = sub_element.SelectSingleNode("bad").InnerText;
//				answer.goodJumpToLine = (int)decimal.Parse(sub_element.SelectSingleNode("good").Attributes[1].Value);
//				answer.badJumpToLine = (int)decimal.Parse(sub_element.SelectSingleNode("bad").Attributes[1].Value);
//				answer.neutralJumpToLine = (int)decimal.Parse(sub_element.SelectSingleNode("neutral").Attributes[1].Value);
//				answer.speaker = sub_element.Attributes[0].Value;
//				answer.pose = sub_element.Attributes[1].Value;
//				dialog.Answers.Add(answer);
////				Debug.Log("added answer Nr." + dialog.Answers.Count + " " + answer.good + " " + answer.bad);
//			}
//			
//			dialogs.Add(key,dialog);
//		}
//	}
//	void Awake(){
//		loadTexts();
//	}
//	
//	void Update(){
//		if (_currentDialog != null){
//			_lineTime += Time.deltaTime;			
//			lookAtSpeaker();
////			Debug.Log(_currentLineType + " " + _lineTime + " " +  currentLineLength * durationPerWord);
//			if(_currentLineType == lineType.Answer && _lineTime > currentLineLength * durationPerWord){
//				if(_currentDialog.lines[_currentLine].jumpToLine == -1){
//					GUIManager.destroyQuickInfo(_currentTextObject);
//					endSpeech();
//				}
//				else{
//					GUIManager.destroyQuickInfo(_currentTextObject);
//					currentLine = _currentDialog.lines[_currentLine].jumpToLine;
//				}
//			}
//		}
//	}
//	
//	public static void changeLine(int nl){
//		currentLineLength = calculateSpeechLength(_currentDialog.lines[nl].text);
//		_currentSpeaker = _currentDialog.lines[nl].speaker;
//		_currentTextObject = GUIManager.screenCastSpeech(_currentDialog.lines[nl].text, 1000f);
//		_currentLineType = _currentDialog.lines[nl].type;
//		_currentPose = _currentDialog.lines[nl].pose;
//		if(_currentLineType == lineType.Question){
//				string asker = _currentDialog.Answers[_currentDialog.lines[nl].answers].speaker;
//				Debug.Log("asker: " + asker + " " + nl);
//				if(asker == "freedan"){
//					GUIManager.showAnswersFreedan();
//					setAnswersFreedan(nl);
//					ControllFreedan.FreedanObject.GetComponent<ControllFreedan>().state = ControllFreedan._FreedanANSWER;
//				}
//				else if(asker == "stayn" || _currentSpeaker == "stayn_spirit"){
//					GUIManager.showAnswersStayn();
//					setAnswersStayn(nl);
//					ControllStayn.StaynObject.GetComponent<ControllStayn>().state = ControllStayn._StaynANSWER;
//				}
//		}
//	}
//	public void endSpeech(){
//		_currentDialog = null;
//		currentLine = 0;
//		GUIManager.stopTalking();
//		if(_currentSpeaker == "custom"){
//			currentController.state = nextState;
//		}
//		else{
//			ControllStayn.StaynObject.GetComponent<ControllStayn>().state = ControllStayn._StaynIDLE;
//			ControllFreedan.FreedanObject.GetComponent<ControllFreedan>().state = ControllFreedan._FreedanIDLE;
//		}
//	}
//	private void lookAtSpeaker(){
//		if(_currentSpeaker == "freedan"){
//			if(_currentPose == "total"){
//				ControllCamera.focusOnObject(ControllFreedan.FreedanObject,(Vector3.up * 3.6f)+(Vector3.right * 12));
//			}
//			else if(_currentPose == "half"){
//				ControllCamera.focusOnObject(ControllFreedan.FreedanObject,(Vector3.up * 2.6f)+(Vector3.right * 24));
//			}
//			else if(_currentPose == "face"){
//				ControllCamera.focusOnObject(ControllFreedan.FreedanObject,(Vector3.up * 1.7f)+(Vector3.right * 48));
//			}
//		}
//		else if(_currentSpeaker == "stayn" ){
//			if(_currentPose == "total"){
//				ControllCamera.focusOnObject(ControllStayn.StaynObject,(Vector3.up * 3.6f)+(Vector3.right * 12));
//			}
//			else if(_currentPose == "half"){
//				ControllCamera.focusOnObject(ControllStayn.StaynObject,(Vector3.up * 2.6f)+(Vector3.right * 24));
//			}
//			else if(_currentPose == "face"){
//				ControllCamera.focusOnObject(ControllStayn.StaynObject,(Vector3.up * 1.7f)+(Vector3.right * 48));
//			}
//		}
//		else if(_currentSpeaker == "custom"){
//			
//		}
//		else if(_currentSpeaker == "stayn_spirit"){
//			if(_currentPose == "total"){
//				ControllCamera.focusOnObject(ControllStayn.StaynObject,(Vector3.up * 3.6f)+(Vector3.right * 12));
//			}
//			else if(_currentPose == "half"){
//				ControllCamera.focusOnObject(ControllStayn.StaynObject,(Vector3.up * 2.6f)+(Vector3.right * 24));
//			}
//			else if(_currentPose == "face"){
//				ControllCamera.focusOnObject(ControllStayn.StaynObject,(Vector3.up * 1.7f)+(Vector3.right * 48));
//			}
//			
//		}
//		
//	}
//	public static float calculateSpeechLength(string text){
//		return text.Split(' ').Length; 
//	}
//}
//
//public class Dialog{
//	public  string key;
//	public dialogType type;
//	public List<Line> lines;
//	public List<Answer> Answers;
//}
//public class Line{
//	public string text;
//	public  string speaker;
//	public  string pose;
//	public  lineType type;
//	public int jumpToLine;
//	public  int answers;
//}
//public class Answer{
//	public  string good;
//	public  string neutral;
//	public  string bad;
//	public  string speaker;
//	public  string pose;
//	public int goodJumpToLine;
//	public int neutralJumpToLine;
//	public int badJumpToLine;
//}
//public enum lineType{
//	Answer,
//	Question,
//	Auto
//}
//public enum dialogType{
//	dialogFreedanStayn,
//	monolog
}