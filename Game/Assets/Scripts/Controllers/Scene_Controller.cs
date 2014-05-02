using UnityEngine;
using System.Collections;

public class Scene_Controller : MonoBehaviour {

	private static Scene_Controller _instance;
	public static Scene_Controller instance{
		get	{
			if(_instance == null){
				_instance = (Scene_Controller)FindObjectOfType(typeof(Scene_Controller));
				DontDestroyOnLoad(_instance.gameObject);
				if(_instance == null){
					if(Resources.FindObjectsOfTypeAll(typeof(Scene_Controller)).Length > 0){
						Scene_Controller prefab = (Scene_Controller)Resources.FindObjectsOfTypeAll(typeof(Scene_Controller))[0];
						_instance = (Scene_Controller)Instantiate(prefab);
						DontDestroyOnLoad(_instance.gameObject);
					}else{
						_instance = (new GameObject()).AddComponent<Scene_Controller>();
					}
					_instance.gameObject.name = "_Scene_Controller";
					DontDestroyOnLoad(_instance.gameObject);
				}
			}
			return _instance;
		}
	}
	
	public int loadingSceneIndex;
	public delegate void SceneLoadedCallBack ();
	private SceneLoadedCallBack callBack;
	
	void Awake(){
		DontDestroyOnLoad(gameObject);
	}


	public void LoadLevel(string levelName){
		StartCoroutine(LoadLevelCoroutine(levelName, null));
	}

	public void LoadLevel(string levelName, SceneLoadedCallBack callBack){
		StartCoroutine(LoadLevelCoroutine(levelName, callBack));
	}

	IEnumerator LoadLevelCoroutine (string levelName, SceneLoadedCallBack callBack){
		yield return Application.LoadLevelAsync(levelName);
		if(callBack != null)
			callBack();
	}

}
