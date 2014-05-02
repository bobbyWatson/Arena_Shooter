using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menu_Controller : MonoBehaviour {

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
		VICTORY
	}

	//public MenuPage 
	public GameObject currentPage;
	public Pages[] pagesDocKeys;
	public GameObject[] pagesDicValue;
	public bool inMenu = true;
	public float stickTreshold = 0.8f;
	public float timeBetweenTwoDirections = 0.1f;
	public Button selectedButton;
	public Player.Colors[] nextGameColors;
	public GamepadInput.GamePad.Index[] nextGameInputs;
	public int selectedPlayersNumber = 0;
	
	private bool canDirectionInput;
	private Dictionary<Pages,GameObject> pagesDic;

	void Awake(){
		pagesDic = Utils.CreateDictionnary(pagesDocKeys, pagesDicValue);
		DontDestroyOnLoad(gameObject);
		canDirectionInput = true;
	}


	void Update(){
		if(inMenu){
			if(canDirectionInput){
				if(GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, GamepadInput.GamePad.Index.One, false).y <= -stickTreshold || Input.GetAxis("Vertical") <= -stickTreshold){
					if(selectedButton.linkSelection.ContainsKey(Button.SelectionDirections.DOWN)){
						selectedButton.Unselect();
						selectedButton = selectedButton.linkSelection[Button.SelectionDirections.DOWN];
						selectedButton.Select();
						StartCoroutine(newDirectionAvalaible());
					}
				}
				else if(GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, GamepadInput.GamePad.Index.One, false).y >= stickTreshold || Input.GetAxis("Vertical") >= stickTreshold){
					if(selectedButton.linkSelection.ContainsKey(Button.SelectionDirections.UP)){
						selectedButton.Unselect();
						selectedButton = selectedButton.linkSelection[Button.SelectionDirections.UP];
						selectedButton.Select();
						StartCoroutine(newDirectionAvalaible());
					}
				}
			}
			if(GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.A, GamepadInput.GamePad.Index.One) || Input.GetKeyDown(KeyCode.Return)){
				if(selectedButton != null){
					selectedButton.Validate();
				}
			}
			if(GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.B, GamepadInput.GamePad.Index.One) || Input.GetKeyDown(KeyCode.Escape)){
				if(selectedButton != null){
					selectedButton.Cancel();
				}
			}
		}
	}
	
	IEnumerator newDirectionAvalaible (){
		canDirectionInput = false;
		yield return new WaitForSeconds(timeBetweenTwoDirections);
		canDirectionInput = true;
	}

	public void ChangePage(Pages pageName){
		GameObject.Destroy(currentPage);
		GameObject.Instantiate(pagesDic[pageName]);
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
		Game_Controller.instance.StartGame();
	}
}
