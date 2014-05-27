using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {

	public Sprite[] blueScore;
	public Sprite[] redScore;
	public Sprite[] greenScore;
	public Sprite[] yellowScore;
	public Player.Colors[] framesColorKey;
	public Sprite[] framesColorValue;
	public SpriteRenderer frame;
	public SpriteRenderer number;
	private Dictionary<Player.Colors, Sprite> framesColorDic;
	private Dictionary<Player.Colors,Sprite[]> colorScores;

	void Awake (){
		colorScores = new Dictionary<Player.Colors, Sprite[]>(){
			{Player.Colors.BLUE, blueScore},
			{Player.Colors.RED, redScore},
			{Player.Colors.GREEN, greenScore},
			{Player.Colors.YELLOW, yellowScore}
		};
		framesColorDic = Utils.CreateDictionnary(framesColorKey, framesColorValue);
	}

	public void Init(int score, Player.Colors color){
		frame.sprite = framesColorDic[color];
		SetScore(score, color);
	}

	public void SetScore (int score, Player.Colors color){
		Debug.Log(score);
		if(score > 0)
			number.sprite = colorScores[color][score-1];
		else
			gameObject.SetActive(false);
	}
}
