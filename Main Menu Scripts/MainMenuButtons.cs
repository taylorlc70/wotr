using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour {


	// Use this for initialization
	void Start () {
		GameManager.multiplayerGame = false;
		GameManager.singlePlayerGame = false;
	}
	
	public void SinglePlayerClicked(){
		Debug.Log("SinglePlayerClicked running");
		/*Destroy(singlePlayerButton);
		Instantiate(FPsideButton);
		Instantiate(FPsideButton);
		Instantiate(goBack);*/
	}

	public void PickFPClicked(){
		Debug.Log("PickFPClicked running");
		GameManager.singlePlayerGame = true;
		GameManager.userIsFP = true;
		Application.LoadLevel(1);
	}

	public void PickSHClicked(){
		Debug.Log("PickSHClicked running");
		GameManager.singlePlayerGame = true;
		GameManager.userIsFP = false;
		Application.LoadLevel(1);
	}

	public void GoBackClicked(){
		Debug.Log("GoBackClicked running");
		/*Destroy(FPsideButton);
		Destroy(SHsideButton);
		Destroy(goBack);
		Instantiate(singlePlayerButton);*/
	}

}
