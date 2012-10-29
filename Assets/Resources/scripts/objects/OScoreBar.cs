using UnityEngine;
using System.Collections;

public class OScoreBar : MonoBehaviour {
	
	public static int Score = 0;
	
	private float _score;
	private float _scoreTarget;
	private exSpriteFont _t;
	void Start () {
		_t = GetComponent<exSpriteFont>();
	}
	
	void Update () {
		_scoreTarget = OScoreBar.Score;
		_score = Mathf.Lerp(_score,_scoreTarget, 5 * Time.deltaTime);
		_t.text = Mathf.RoundToInt(_score).ToString();
	}
	
	public static void AddScore(int s){
		Score += s;
	}
}
