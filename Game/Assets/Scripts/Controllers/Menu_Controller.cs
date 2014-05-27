using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu_Controller : Controller {

	private static Menu_Controller _instance;
	public static Menu_Controller instance{
		get	{
			if(_instance == null){
				_instance = (Menu_Controller)FindObjectOfType(typeof(Menu_Controller));
				DontDestroyOnLoad(_instance.gameObject);
				if(_instance == null){
					if(Resources.FindObjectsOfTypeAll(typeof(Menu_Controller)).Length > 0){
						Menu_Controller prefab = (Menu_Controller)Resources.FindObjectsOfTypeAll(typeof(Menu_Controller))[0];
						_instance = (Menu_Controller)Instantiate(prefab);
						DontDestroyOnLoad(_instance.gameObject);
					}else{
						_instance = (new GameObject()).AddComponent<Menu_Controller>();
					}
					_instance.gameObject.name = "_Menu_Controller";
					DontDestroyOnLoad(_instance.gameObject);
				}
			}
			return _instance;
		}
	}

	public enum Pages{
		SELECT_MODE,
		SELECT_CHARACTER,
		CREDITS,
		PAUSE,
		VICTORY,
		GAME,
		ONLINE_MENU
	}

	//public MenuPage 
	public UIRefs UI;
	public Pages currentPage;
	public Player.Colors[] nextGameColors;
	public GamepadInput.GamePad.Index[] nextGameInputs;
	public int selectedPlayersNumber = 0;
	public string myIp;

	void Awake(){
		DontDestroyOnLoad(gameObject);
	}

	public void ChangePage(Pages pageName){
		if(currentPage != null){
			UI.menuPages[currentPage].SetActive(false);
		}
		currentPage = pageName;
		UI.menuPages[currentPage].SetActive(true);
	}

	public void EnterCharacterSelection (){
		selectedPlayersNumber = 0;
		nextGameColors = new Player.Colors[4]{
			Player.Colors.NONE,
			Player.Colors.NONE,
			Player.Colors.NONE,
			Player.Colors.NONE
		};
		nextGameInputs = new GamepadInput.GamePad.Index[4];
	}

	public void StartGame (){
		ChangePage(Pages.GAME);
		Game_Controller.instance.StartGame();
	}

	public void BackToMenu (){
		Scene_Controller.instance.LoadLevel("Menu");
	}

	public void EnterOnlineMenu (){
		if(myIp == null){
			StartCoroutine(GetMyIP());
		}else{
			UI.WaitForOnline.SetActive(false);
		}
	}

	IEnumerator GetMyIP (){
		UI.WaitForOnline.SetActive(true);
		WWW myExtIPWWW = new WWW("http://checkip.dyndns.org");
		yield  return myExtIPWWW;
		myIp = myExtIPWWW.data;
		myIp = myIp.Substring(myIp.IndexOf(":")+1);
		myIp = myIp.Substring(0,myIp.IndexOf("<"));
		UI.WaitForOnline.SetActive(false);
		yield break;
	}
}
