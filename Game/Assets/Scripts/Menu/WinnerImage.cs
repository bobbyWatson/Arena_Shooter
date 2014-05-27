using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinnerImage : MonoBehaviour {

	public Player.Colors[] colorImageKeys;
	public Sprite[] colorImageValues;
	public Dictionary<Player.Colors, Sprite> colorImagesDic;
	public UISprite sprite;

	void Awake (){
		colorImagesDic = Utils.CreateDictionnary(colorImageKeys, colorImageValues);
	}

	void OnEnable (){
		sprite.spriteName = colorImagesDic[Game_Controller.instance.winnerColor].name;
	}
}
