using UnityEngine;
using System.Collections;

public class MenuPage : MonoBehaviour {

	public Menu_Controller.Pages page;

	void Awake(){
		Menu_Controller.instance.currentPage = this.gameObject;
		if(page == Menu_Controller.Pages.SELECT_CHARACTER){
			Menu_Controller.instance.EnterCharacterSelection();
		}
	}
}
