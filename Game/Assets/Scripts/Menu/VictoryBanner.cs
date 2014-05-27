using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VictoryBanner : MonoBehaviour {

	public Player.Colors[] bannerColorsKeys;
	public Sprite[] bannerColorsValue;
	public Dictionary<Player.Colors, Sprite> bannerColorsDic;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		bannerColorsDic = Utils.CreateDictionnary(bannerColorsKeys, bannerColorsValue);
	}

	public void ChangeBanner (Player.Colors color){
		spriteRenderer.sprite = bannerColorsDic[color];
	}
}
