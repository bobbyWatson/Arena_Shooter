using UnityEngine;
using System.Collections;

public class PersoSelect : MonoBehaviour {

	public int playerNumber;
	public Player.Colors color;
	public GamepadInput.GamePad.Index gamePadIndex;
	public Sprite selectedSprite;
	public Sprite unselectedSprite;
	public SpriteRenderer sprite;
	private bool selected = false;


	void Update (){
		if(!selected){
			if(GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.Start, gamePadIndex)){
				Select();
			}
		}else{
			if(GamepadInput.GamePad.GetButtonDown( GamepadInput.GamePad.Button.Start, gamePadIndex)){
				Unselect();
			}
		}
	}

	void Select (){
		selected = true;
		sprite.sprite = selectedSprite;
		Menu_Controller.instance.nextGameColors[playerNumber] = color;
		Menu_Controller.instance.nextGameInputs[playerNumber] = gamePadIndex;
		Menu_Controller.instance.selectedPlayersNumber ++;
	}

	void Unselect (){
		selected = false;
		sprite.sprite = unselectedSprite;
		Menu_Controller.instance.nextGameColors[playerNumber] = Player.Colors.NONE;
		Menu_Controller.instance.selectedPlayersNumber --;
	}
}
