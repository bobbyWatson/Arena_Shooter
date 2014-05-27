using UnityEngine;
using System.Collections;
using SimpleJSON;

[RequireComponent(typeof(UIGrid))]
public class GetServers : MonoBehaviour {

	public GameObject listElementPrefab;
	public UIGrid grid;

	void Awake(){
		grid = GetComponent<UIGrid>();
	}

	void Start (){
		GetServersList();
		StartCoroutine(refresh());
	}

	public void GetServersList (){
		StartCoroutine(_GetServersList());
	}

	private IEnumerator _GetServersList (){
		WWW www = new WWW("localhost/RampageRooster/getServerList.php");
		while(!www.isDone)
			yield return null;
		JSONNode json = JSON.Parse(www.data);
		JSONArray array = json.AsArray;
		for(int i = transform.childCount-1; i >= 0; i--){
			GameObject.Destroy(transform.GetChild(i).gameObject);
		}
		for(int i = 0; i < array.Count; i++){
			GameObject o = GameObject.Instantiate(listElementPrefab) as GameObject;
			o.transform.parent = transform;
			o.GetComponent<ServerListElem>().host.text = array[i]["host"];
			o.transform.localPosition = Vector3.zero;
			o.transform.localScale = new Vector3(1,1,1);
			Debug.Log(o.transform.position);
		}
		grid.Reposition();
	}

	private IEnumerator refresh(){
		while(true){
			yield return new WaitForSeconds(30);
			GetServersList();
		}
	}
}
