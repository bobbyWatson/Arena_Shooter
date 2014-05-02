using UnityEngine;
using System.Collections;

public class GameInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Scene_Controller.instance.LoadLevel("Menu");
	}
}
