using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game_Controller : MonoBehaviour {

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
	public RuntimeAnimatorController[] playerAnimatorsValue;
	public RuntimeAnimatorController[] canonAnimatorsValue;
	public Sprite[] targetValue;
	public Dictionary<Player.Colors,RuntimeAnimatorController> playerAnimatorDic;
	public Dictionary<Player.Colors,RuntimeAnimatorController> playerCanonAnimatorDic;
	public Dictionary<Player.Colors,Sprite> playerTargetDic;
	public List<Player> players;
	
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
			for(int i  = 0; i < Menu_Controller.instance.nextGameColors.Length; i++){
				if(Menu_Controller.instance.nextGameColors[i] != Player.Colors.NONE && Menu_Controller.instance.nextGameColors[i] != null){
					GameObject playerObject = GameObject.Instantiate(playerPrefab) as GameObject;
					Player player = playerObject.GetComponent<Player>();
					player.Init(Menu_Controller.instance.nextGameColors[i], Menu_Controller.instance.nextGameInputs[i]);
					player.transform.position = arena.spawnPoints[i].position;
					players.Add(player);
					player.respawn = arena.spawnPoints[i];
				}
			}
		});
	}
}
