using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

// This script is attached to each player object
// It manages the dice UI panels and is primarily in charge of 
// adding / removing OWNED dice objects and activating, deactivating and/or
// destroying the interactable button components.

public class PlayerDiceScript : MonoBehaviour {

	TurnManager theTurnManager;
	GameManager theGameManager;
	public List<GameObject> myDice_UIbuttons; //This is only equal to the dice this player owns
	public GameObject myDicePanel;
	public GameObject myDicePrefab;

	public int startTurnDiceCount;

	public List<DiceFaceBase> availableActions;

	//************************Mono Behavior Functions *******************************
	void Start(){
		theTurnManager = TurnManager.Instance;
		//Set starting dice amts
		if (gameObject.tag == "FreePlayer") {
			startTurnDiceCount = 4;
			myDicePrefab = Resources.Load("UI_Prefabs/ScreenSpaceUI/FPActionDice") as GameObject;
		} else {
			startTurnDiceCount = 7;
			myDicePrefab = Resources.Load("UI_Prefabs/ScreenSpaceUI/SHActionDice") as GameObject;
		}
		//Setup the dice panel that belongs to this player
		if (gameObject.name == "UserPlayer") { //This is user player
			if (gameObject.tag == "FreePlayer") { //This is user FP
				myDicePanel = Instantiate(Resources.Load("UI_Prefabs/ScreenSpaceUI/FP_ActionDicePanel")) as GameObject;
			} else { //This is user shadow player
				myDicePanel = Instantiate(Resources.Load("UI_Prefabs/ScreenSpaceUI/SH_ActionDicePanel")) as GameObject;
			}
		} else { //This is opponent player
			if (gameObject.tag == "FreePlayer") { //this is opponent FP
				myDicePanel = Instantiate(Resources.Load("UI_Prefabs/ScreenSpaceUI/FP_OpponentDicePanel")) as GameObject;
			} else { //This is opponent SH
				myDicePanel = Instantiate(Resources.Load("UI_Prefabs/ScreenSpaceUI/SH_OpponentDicePanel")) as GameObject;
			}
		}
		myDicePanel.transform.SetParent(GameObject.Find("Main UI").transform, false);

		//Experimental!!!!!
		myDicePanel = myDicePanel.transform.Find ("DicePanel").gameObject;

		//Subscribe to event. Recover dice needs to happen first.
		EventManager.RecAndDraw += RecoverDice;
		//The buttons on all dice are inactive until ActionPhase
		EventManager.ActionPhase += ActivateMyDice;
		EventManager.ActionPhase += GetAvailableActions;
	}

	//***********************************Custom Functions ****************************************

	public void RecoverDice(){
		//Clear out current myDice
		myDice_UIbuttons.Clear();
		//Delete all dice from panel
		Debug.Log("Recovering dice");
		foreach (Transform child in myDicePanel.transform) {
			Destroy(child.gameObject);
		}
		//Add all available blank dice to my panel
		for(int i = 0; i < startTurnDiceCount; i++){
			GameObject go = Instantiate(myDicePrefab) as GameObject;
			go.transform.SetParent(myDicePanel.transform, false);
			//Add each new dice to myDice
			myDice_UIbuttons.Add(go);
		}
	}

	/*public void DeactivateMyDice(){
		Debug.Log("Running the DeactivateMyDice for " + gameObject.name);
		foreach (GameObject go in myDice_UIbuttons) {
			if(go.GetComponent<Button>() != null)
				go.GetComponent<Button>().enabled = false;
		}
	}*/
	
	public void ActivateMyDice(){
		RecalculateDice();
		Debug.Log("Running the ActivateMyDice for " + gameObject.name);
		foreach (GameObject go in myDice_UIbuttons) {
			if(go.GetComponent<Button>() != null){ //The eye dice have their button components removed
				go.GetComponent<Button>().enabled = true;
				if(gameObject.name == "UserPlayer"){
					go.GetComponent<Button>().interactable = true;
				} else{    //None of the opponent dice should actually be buttons
					Destroy(go.GetComponent<Button>());
				}
			}
		}
	}
	
	public void GetAvailableActions(){
		availableActions.Clear();
		foreach (GameObject go in myDice_UIbuttons) {
			availableActions.Add(go.GetComponent<DiceFaceBase>());
		}
	}

	public void RecalculateDice(){
		Debug.Log("Recalculating dice");
		myDice_UIbuttons.Clear();
		foreach (Transform die in myDicePanel.transform) { //Don't know why this doesn't work anymore... //TODO: Fix this!!!
			myDice_UIbuttons.Add(die.gameObject);
		}
	}

	private void OnDisable(){
		EventManager.RecAndDraw -= RecoverDice;
		EventManager.ActionPhase -= ActivateMyDice;
		EventManager.ActionPhase -= GetAvailableActions;
	}
}
