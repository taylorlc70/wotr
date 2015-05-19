using UnityEngine;
using System.Collections;

public class PopupButtonClick : MonoBehaviour {


	public PlayerClass myPlayer;

	void Awake(){
		// Popup menus should only appear for the user player
		myPlayer = GameObject.Find("UserPlayer").GetComponent<PlayerClass>();
	}

	public void DoMyClickAction(){
		//The function called in the PlayerClass is dependent on the name of this button
		myPlayer.GetComponent<PlayerClass>().PopupButtonClicked(gameObject.name);
	}
}
