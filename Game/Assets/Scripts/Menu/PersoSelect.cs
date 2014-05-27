using UnityEngine;
using System.Collections;

public class PersoSelect : MonoBehaviour {

	public bool forcedSelected;
	public int playerNumber;
	public Player.Colors color;
	public GamepadInput.GamePad.Index gamePadIndex;
	public Sprite selectedSprite;
	public Sprite unselectedSprite;
	public UISprite sprite;
	private bool selected = false;
	private AudioSource audioSource;

	void Awake (){
		audioSource = GetComponent<AudioSource>();
	}

	void Start(){
		if(forcedSelected){
			Select();
		}
	}

	void Update (){
		if(!selected){
			if(GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.Start, gamePadIndex)){
				Select();
			}
		}else{
			if(GamepadInput.GamePad.GetButtonDown( GamepadInput.GamePad.Button.Start, gamePadIndex) && !forcedSelected){
				Unselect();
			}
		}
	}

	void Select (){
		selected = true;
		audioSource.Play();
		sprite.spriteName = selectedSprite.name;
		Menu_Controller.instance.nextGameColors[playerNumber] = color;
		Menu_Controller.instance.nextGameInputs[playerNumber] = gamePadIndex;
		Menu_Controller.instance.selectedPlayersNumber ++;
	}

	void Unselect (){
		selected = false;
		sprite.spriteName = unselectedSprite.name;
		Menu_Controller.instance.nextGameColors[playerNumber] = Player.Colors.NONE;
		Menu_Controller.instance.selectedPlayersNumber --;
	}
}
