using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button : MonoBehaviour {

	public Sprite normalSprite;
	public Sprite selectedSprite;
	public SelectionDirections[] linkSelectionKeys;
	public Button[] linkSelectionValues;
	public Dictionary<SelectionDirections,Button> linkSelection;
	public bool defaultSelected;
	public bool changePageOnValidate;
	public Menu_Controller.Pages validateNewPage;
	public bool changePageOnCancel;
	public Menu_Controller.Pages CancelNewPage;
	public string onValidateCallBack;
	public string onCancelCallBack;

	private bool selected;
	private SpriteRenderer spriteRenderer;


	public enum SelectionDirections{
		UP,
		DOWN, 
		LEFT,
		RIGHT,
		VALIDATE,
		CANCEL
	}

	void Awake(){
		linkSelection = Utils.CreateDictionnary(linkSelectionKeys, linkSelectionValues);
		spriteRenderer = GetComponent<SpriteRenderer>();
		if(defaultSelected)
			Select();
	}

	public void Select (){
		//TODO add a tween plug-in and transforms like scale, positino and color
		spriteRenderer.sprite = selectedSprite;
		selected = true;
		Menu_Controller.instance.selectedButton = this;
	}

	public void Unselect(){
		spriteRenderer.sprite = normalSprite;
		selected = false;
	}

	public void Validate(){
		if(changePageOnValidate)
			Menu_Controller.instance.ChangePage(validateNewPage);
		if(onValidateCallBack != null && onValidateCallBack != ""){
			Debug.Log(onValidateCallBack);
			Menu_Controller.instance.SendMessage(onValidateCallBack, SendMessageOptions.DontRequireReceiver);
		}
	}

	public void Cancel(){		
		if(changePageOnCancel)
			Menu_Controller.instance.ChangePage(CancelNewPage);
		if(onCancelCallBack != null && onCancelCallBack != "")
			Menu_Controller.instance.SendMessage(onCancelCallBack, SendMessageOptions.DontRequireReceiver);
	}
}
