using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIRefs : MonoBehaviour {

	public Camera camera;
	public Menu_Controller.Pages[] menuPagesKeys;
	public GameObject[] menuPagesValues;
	public Dictionary<Menu_Controller.Pages, GameObject> menuPages;
	public Menu_Controller.Pages StartPage;
	public GameObject WaitForOnline;


	// Use this for initialization
	void Awake () {
		menuPages = Utils.CreateDictionnary<Menu_Controller.Pages, GameObject>(menuPagesKeys, menuPagesValues);
		if(Menu_Controller.instance.UI != null){
			Debug.Log("toto");
			GameObject.Destroy(Menu_Controller.instance.UI.gameObject);
			Debug.Log(Menu_Controller.instance.UI);
		}
		Menu_Controller.instance.UI = this;
		Menu_Controller.instance.currentPage = this.StartPage;
		menuPages[StartPage].SetActive(true);
	}
}
