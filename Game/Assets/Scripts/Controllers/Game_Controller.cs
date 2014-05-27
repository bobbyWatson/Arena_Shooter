using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_Controller : Controller {

	private static Game_Controller _instance;
	public static Game_Controller instance{
		get	{
			if(_instance == null){
				_instance = (Game_Controller)FindObjectOfType(typeof(Game_Controller));
				DontDestroyOnLoad(_instance.gameObject);
				if(_instance == null){
					if(Resources.FindObjectsOfTypeAll(typeof(Game_Controller)).Length > 0){
						Game_Controller prefab = (Game_Controller)Resources.FindObjectsOfTypeAll(typeof(Game_Controller))[0];
						_instance = (Game_Controller)Instantiate(prefab);
						DontDestroyOnLoad(_instance.gameObject);
					}else{
						_instance = (new GameObject()).AddComponent<Game_Controller>();
					}
					_instance.gameObject.name = "_Game_Controller";
					DontDestroyOnLoad(_instance.gameObject);
				}
			}
			return _instance;
		}
	}



	public BulletManager bulletManager;
	public GameObject testArena;
	public GameObject playerPrefab;
	public Player.Colors[] dicKeys;
	public GameObject scorePrefab;
	public RuntimeAnimatorController[] playerAnimatorsValue;
	public RuntimeAnimatorController[] canonAnimatorsValue;
	public Sprite[] targetValue;
	public Dictionary<Player.Colors,RuntimeAnimatorController> playerAnimatorDic;
	public Dictionary<Player.Colors,RuntimeAnimatorController> playerCanonAnimatorDic;
	public Dictionary<Player.Colors,Sprite> playerTargetDic;
	public List<Player> players;
	public Player.Colors winnerColor;
	public Vector2[] scorePositions;
	
	void Awake(){
		DontDestroyOnLoad(gameObject);
		playerAnimatorDic = Utils.CreateDictionnary(dicKeys,playerAnimatorsValue);
		playerCanonAnimatorDic = Utils.CreateDictionnary(dicKeys,canonAnimatorsValue);
		playerTargetDic = Utils.CreateDictionnary(dicKeys,targetValue);
	}

	public void StartGame (){
		Scene_Controller.instance.LoadLevel("Game", delegate (){
			GameObject arenaObject = GameObject.Instantiate(testArena) as GameObject; 
			Arena arena = arenaObject.GetComponent<Arena>();
			players = new List<Player>();
			for(int i  = 0; i < Menu_Controller.instance.nextGameColors.Length; i++){
				if(Menu_Controller.instance.nextGameColors[i] != Player.Colors.NONE && Menu_Controller.instance.nextGameColors[i] != null){
					GameObject playerObject = GameObject.Instantiate(playerPrefab) as GameObject;
					Player player = playerObject.GetComponent<Player>();
					player.Init(Menu_Controller.instance.nextGameColors[i], Menu_Controller.instance.nextGameInputs[i]);
					player.transform.position = arena.spawnPoints[i].position;
					players.Add(player);
					player.respawn = arena.spawnPoints[i];
					GameObject scoreObject = GameObject.Instantiate(scorePrefab) as GameObject;
					Score score = scoreObject.GetComponent<Score>();
					score.transform.position = new Vector3(scorePositions[i].x, scorePositions[i].y ,score.transform.position.z);
					player.score = score;
					score.Init(player.lifes, player.color);
				}
			}
		});
	}
		
	void Update (){
		if(Input.GetKeyDown(KeyCode.PageDown)){
			Application.Quit();
		}
	}

	public void CheckLastAlive(){
		if(players.Count == 1){
			Debug.Log("GAME OVER");
			ShowVictory(Player.Colors.YELLOW);
		}else{
			int playersLeft = 0;
			Player playerLeft = null;
			for(int i = 0; i < players.Count; i++){
				if(players[i].lifes > 0){
					playersLeft++;
					playerLeft = players[i];
				}
			}
			if(playersLeft == 1){
				Debug.Log("Last Survivor is player " + playerLeft.color.ToString());
				ShowVictory(playerLeft.color);
			}
		}
	}

	private void ShowVictory (Player.Colors color){
		winnerColor = color;
		Menu_Controller.instance.ChangePage(Menu_Controller.Pages.VICTORY);
	}
}
