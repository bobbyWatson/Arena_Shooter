using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageToController : MonoBehaviour {

	//TODO trouver un moyen pplus propre (par custom editor par exemple)

	public enum Controllers{
		GAME_CONTROLLER,
		MENU_CONTROLLER,
		SCENE_CONTROLLER
	}
	private Controllers[] controllersKey = new Controllers[]{
		Controllers.GAME_CONTROLLER,
		Controllers.MENU_CONTROLLER,
		Controllers.SCENE_CONTROLLER
	};
	private Dictionary<Controllers, Controller> ControllersDic;

	public Controllers controller;
	public string enabledMessage;
	public string startMessage;
	public string buttonMessage;

	void Awake (){
		Controller[] controllersValue = new Controller[]{
			Game_Controller.instance,
			Menu_Controller.instance,
			Scene_Controller.instance
		};
		ControllersDic = Utils.CreateDictionnary(controllersKey, controllersValue);
	}

	void Start (){
		ControllersDic[controller].SendMessage(startMessage, SendMessageOptions.DontRequireReceiver);
	}

	void OnEnable (){
		ControllersDic[controller].SendMessage(enabledMessage, SendMessageOptions.DontRequireReceiver);
	}

	void OnPress (bool pressed){
		if(!pressed)
			ControllersDic[controller].SendMessage(buttonMessage, SendMessageOptions.DontRequireReceiver);
	}

}
