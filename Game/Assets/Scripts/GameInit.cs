using UnityEngine;
using System.Collections;

public class GameInit : MonoBehaviour {

	public GameObject go;
	// Use this for initialization
	void Start () {
		Scene_Controller.instance.LoadLevel("Menu");
	}
}
