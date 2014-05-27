using UnityEngine;
using System.Collections;

public class ButtonChangePage : MonoBehaviour {

	public Menu_Controller.Pages nextPage;

	void OnPress(bool pressed){
		if(!pressed){
			Menu_Controller.instance.ChangePage(nextPage);
		}
	}
}
