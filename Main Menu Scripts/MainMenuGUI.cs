using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuGUI : MonoBehaviour {

	public Button singlePlayerButton;
	public Button FPsideButton;
	public Button SHsideButton;
	public Button goBack;
	//public bool singlePlayerPressed;
	//public bool multiplayerPressed;
	//public bool optionsPressed;

	// Use this for initialization
	void Start () {
		GameManager.multiplayerGame = false;
		GameManager.singlePlayerGame = false;
	}
	
	void SinglePlayerClicked(){
		GameManager.singlePlayerGame = true;

		Destroy(singlePlayerButton);
		Instantiate(FPsideButton);
		Instantiate(SHsideButton);
		Instantiate(goBack);
	}


	/*void OnGUI(){
		if(!singlePlayerPressed && !multiplayerPressed){
			if(GUI.Button(new Rect(100,100,100,100),"Single Player")){
				singlePlayerPressed = true;
			}
			if(GUI.Button(new Rect(200,100,100,100),"Multi-player")){
				//Disabled for now
				//multiplayerPressed = true;
			}
		}
		if(singlePlayerPressed == true){
			GUI.Box (new Rect(200,300,400,400),"Choose your side");
			if(GUI.Button(new Rect(100,100,100,100),"Free Peoples")){
				GameManager.singlePlayerGame = true;
				GameManager.userIsFP = true;
				Application.LoadLevel(1);
			}
			if(GUI.Button(new Rect(200,100,100,100),"Shadow")){
				GameManager.singlePlayerGame = true;
				GameManager.userIsFP = false;
				Application.LoadLevel(1);
			}
			if(GUI.Button(new Rect(550,300,60,50),"Go back")){
				singlePlayerPressed = false;
				multiplayerPressed = false;
			}
		}

		if(multiplayerPressed == true){
			//Multiplayer is disabled for now
			//GameManager.multiplayerGame = true;
			//Application.LoadLevel(1);
			}
	}*/
}
