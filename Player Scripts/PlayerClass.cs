using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerClass: MonoBehaviour {

	//Not currently used...
	public bool isMyTurn;

	public bool AIControlled = false;

	public int handCount;

	public int elvenRings;

	public GameObject[] mySettlementSpaces;
	public GameObject[] myPoliticalMarkers;

	public TurnManager theTurnManager;
	//************************Monobehavior Functions *************************************
	void Awake(){
		//The player tag (FP or SH) is set in the Awake function of Game
	}

	void Start(){
		theTurnManager = TurnManager.Instance;
		//Find which settlement spaces belong to me
		if (gameObject.tag == "FreePlayer") {
			mySettlementSpaces = GameObject.FindGameObjectsWithTag("FP_Settlement");
			myPoliticalMarkers = GameObject.FindGameObjectsWithTag("FP_PoliticalMarker");
		} else if(gameObject.tag == "ShadowPlayer") {
			mySettlementSpaces = GameObject.FindGameObjectsWithTag("SH_Settlement");
			myPoliticalMarkers = GameObject.FindGameObjectsWithTag("SH_PoliticalMarker");
		}
		//If Game has determined this is AI player, turn on AI script
		if (AIControlled) {
			gameObject.GetComponent<AIPlayer>().enabled = true;
		}
	}

	//************************* Action Functions **************************************
	//*********************************************************************************
	private void PlayCard(){
		if (handCount < 1) {
			Debug.Log("No cards to play");
			return;
		}
		if (handCount >= 1) {
			handCount -= 1;
		}
	}
	//************************* Popup Button Function *****************************************
	//*****************************************************************************************
	//All ui popup buttons should have PopupButtonClicked that passes in their name as a string parameter to this function
	public void PopupButtonClicked(string buttonName){
		//The name of the button is passed in. It should match the name of the function it calls.
		Invoke(buttonName, 0); //Invoke(string methodName, timeInSeconds)
	}

	//************************* Button Functions****************************************

	private void RecruitUnits(){
		Debug.Log("Recruitment started for " + gameObject.name);
		//Finds all settlement spaces that are AtWar for Muster Dice recruitment
		foreach(GameObject spot in mySettlementSpaces){
			if(spot.GetComponent<SettlementSpace>().atWar == true)
				spot.GetComponent<SettlementSpace>().HighlightSpace(/*SpaceClass.HighlightState.Recruit*/);
		}
	}


	// Move armies button is pressed
	private void MoveArmies(){
		Debug.Log("Move armies has been clicked. Possible choices are now highlighted for " + gameObject.name);
		//buttonPressed = ButtonPressed.Move;
		GameObject[] myArmies = null;
		if (gameObject.tag == "FreePlayer") {
			myArmies = GameObject.FindGameObjectsWithTag("FreeArmy");
		} else if (gameObject.tag == "ShadowPlayer") {
			myArmies = GameObject.FindGameObjectsWithTag("ShadowArmy");
		}
		foreach (GameObject army in myArmies) {
			army.transform.parent.GetComponent<SpaceClass>().HighlightSpace(SpaceClass.HighlightState.MoveStart);
		}
	}

	private void HighlightPoliticalMarkers(){
		foreach(GameObject marker in myPoliticalMarkers){
			if(marker.GetComponent<Outline>() == null){
				marker.AddComponent<Outline>();
			}
			marker.GetComponent<Button>().enabled = true;
		}
	}
		/*List<string> allSpaces = new List<string>(GameManager.adjacencyMap.Keys);
		foreach(string space in allSpaces){

		}*/

	public void EndMyAction(){
		Debug.Log("Action Ended");
		theTurnManager.NextAction();
	}



}
