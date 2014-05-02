using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {

	public bool canUse = true;
	public Player.Colors[] colorSpriteKeys;
	public Sprite[] colorSpriteValues;
	public Dictionary<Player.Colors,Sprite> colorSprite;
	public Player.Colors currentColor;
	public Player.Colors originColor;
	public float lifeTime;
	private bool inofensive;
	private SpriteRenderer spriteRenderer;

	void Awake (){
		colorSprite = Utils.CreateDictionnary(colorSpriteKeys, colorSpriteValues);
		currentColor = Player.Colors.WHITE;
		inofensive = true;
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnCollisionEnter2D (Collision2D coll){
		if(coll.gameObject.tag == "Player"){
			if(this.currentColor == Player.Colors.WHITE){
				coll.gameObject.GetComponent<Player>().Die();
			}else if(coll.gameObject.GetComponent<Player>().color == currentColor){
				coll.gameObject.GetComponent<Player>().Die();
			}else{
				Player.Colors newColor = coll.gameObject.GetComponent<Player>().color;
				currentColor = newColor;
				spriteRenderer.sprite = colorSprite[newColor];
			}
		}else{
			currentColor = originColor;
			spriteRenderer.sprite = colorSprite[originColor];
		}
	}

	public void OnPool (){
		inofensive = true;
		gameObject.layer = LayerMask.NameToLayer("OwnBullet");
		gameObject.SetActive(false);
		canUse = true;
		StopCoroutine("timedLife");
	}

	public void OnUnpool (){
		gameObject.SetActive(true);
		canUse = false;
		StartCoroutine("TimedLife");
	}
	
	IEnumerator TimedLife (){
		yield return new WaitForSeconds(this.lifeTime);
		yield return null;
		Game_Controller.instance.bulletManager.Pool(this);
	}
}
