using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Game : MonoBehaviour {

	public TurnManager theTurnManager;

	public GameObject userPlayer;
	public GameObject opponentPlayer;

	public List<GameObject> allSpaces;


	//Amounts of Reinforcement units
	public static int gondorRegs = 15, gondorElites = 5, gondorLeaders = 4, rohanRegs = 10, rohanElites = 5, rohanLeaders = 4, elfRegs = 5, elfElites = 10, elfLeaders = 4, dwarfRegs = 5, dwarfElites = 5, dwarfLeaders = 4, northRegs = 10, northElites = 5, northLeaders = 4, isengardRegs = 12, isengardElites = 6, eastRegs = 24, eastElites = 6, sauronRegs = 36, sauronElites = 6, nazgul = 8;

	//MonoBehaviour Functions
	//****************************************************************************
	void Awake(){
		if (GameManager.userIsFP) {
			userPlayer.tag = "FreePlayer";
			opponentPlayer.tag = "ShadowPlayer";
		} else {
			userPlayer.tag = "ShadowPlayer";
			opponentPlayer.tag = "FreePlayer";
		}
	}
	
	void Start () {
		theTurnManager = TurnManager.Instance;
		//Set the tags for the players

		//AI controlled settings will have to change for multiplayer
		opponentPlayer.GetComponent<PlayerClass>().AIControlled = true;
	}

	//*******************************************************************************
	//************* My Functions ****************************************************

	public void StartGame(){
		//This function is run from the Start button
		//Setup the board
		GetAllBoardSpaces();
		StartingGameBoardSetup();
		//TurnManager starting
		theTurnManager.StartTurn();
		GameManager.gameRunning = true;
		Debug.Log(GameManager.regionNamesList.Count);
	}

	public List<GameObject> GetAllBoardSpaces(){
		if (allSpaces.Count <= 1) {
			Debug.Log(GameManager.regionNamesList.Count);
			for (int i=0; i < GameManager.regionNamesList.Count; i++) {
				allSpaces.Add(GameObject.Find(GameManager.regionNamesList [i]));
			}
		}
		return allSpaces;
	}

	public void StartingGameBoardSetup(){
		//Add all of the starting armies to spaces. Use the allSpaces list for this.
		Debug.Log("Starting boardgame setup...");
		foreach(GameObject space in allSpaces){
			New_SpaceClass nsc = space.GetComponent<New_SpaceClass>();
			GameManager.Nation n = nsc.nation;
			if(space.name == "Erebor"){
				nsc.RecruitUnit(1, n, "Regular");
				nsc.RecruitUnit(2, n, "Elite");
				nsc.RecruitUnit(1, n, "Leader");
			}
			if(space.name == "Ered Luin"){
				nsc.RecruitUnit(1, n, "Regular");
			}
			if(space.name == "Iron Hills"){
				nsc.RecruitUnit(1, n, "Regular");
			}
			if(space.name == "Grey Havens"){
				nsc.RecruitUnit(1, n, "Regular");
				nsc.RecruitUnit(1, n, "Elite");
				nsc.RecruitUnit(1, n, "Leader");
			}
			if(space.name == "Rivendell"){
				nsc.RecruitUnit(2, n, "Elite");
				nsc.RecruitUnit(1, n, "Leader");
			}
			if(space.name == "Woodland Realm"){
				nsc.RecruitUnit(1, n, "Regular");
				nsc.RecruitUnit(1, n, "Elite");
				nsc.RecruitUnit(1, n, "Leader");
			}
			if(space.name == "Lorien"){
				nsc.RecruitUnit(1, n, "Regular");
				nsc.RecruitUnit(2, n, "Elite");
				nsc.RecruitUnit(1, n, "Leader");
			}
			if(space.name == "Minas Tirith"){
				nsc.RecruitUnit(3, n, "Regular");
				nsc.RecruitUnit(1, n, "Elite");
				nsc.RecruitUnit(1, n, "Leader");
			}
			if(space.name == "Dol Amroth"){
				nsc.RecruitUnit(3, n, "Regular");
			}
			if(space.name == "Osgiliath"){
				nsc.RecruitUnit(2, GameManager.Nation.Gondor, "Regular"); //Osgiliath is technically a neutral territory
			}
			if(space.name == "Pelargir"){
				nsc.RecruitUnit(1, n, "Regular");
			}
			if(space.name == "Bree"){
				nsc.RecruitUnit(1, n, "Regular");
			}
			if(space.name == "North Downs"){
				nsc.RecruitUnit(1, n, "Elite");
			}
			if(space.name == "Carrock"){
				nsc.RecruitUnit(1, n, "Regular");
			}
			if(space.name == "The Shire"){
				nsc.RecruitUnit(1, n, "Regular");
			}
			if(space.name == "Dale"){
				nsc.RecruitUnit(1, n, "Regular");
				nsc.RecruitUnit(1, n, "Leader");
			}
			if(space.name == "Edoras"){
				nsc.RecruitUnit(1, n, "Regular");
				nsc.RecruitUnit(1, n, "Elite");
			}
			if(space.name == "Fords of Isen"){
				nsc.RecruitUnit(2, n, "Regular");
				nsc.RecruitUnit(1, n, "Regular");
			}
			if(space.name == "Helm's Deep"){
				nsc.RecruitUnit(1, n, "Regular");
			}
			if(space.name == "Orthanc"){
				nsc.RecruitUnit(4, n, "Regular");
				nsc.RecruitUnit(1, n, "Elite");
			}
			if(space.name == "North Dunland"){
				nsc.RecruitUnit(1, n, "Regular");
			}
			if(space.name == "South Dunland"){
				nsc.RecruitUnit(1, n, "Regular");
			}
			if(space.name == "Barad-Dur"){
				nsc.RecruitUnit(4, n, "Regular");
				nsc.RecruitUnit(1, n, "Elite");
				nsc.RecruitUnit(1, n, "Leader");
			}
			if(space.name == "Dol Guldur"){
				nsc.RecruitUnit(5, n, "Regular");
				nsc.RecruitUnit(1, n, "Elite");
				nsc.RecruitUnit(1, n, "Leader");
			}
			if(space.name == "Gorgoroth"){
				nsc.RecruitUnit(3, n, "Regular");
			}
			if(space.name == "Minas Morgul"){
				nsc.RecruitUnit(5, n, "Regular");
				nsc.RecruitUnit(1, n, "Leader");
			}
			if(space.name == "Moria"){
				nsc.RecruitUnit(2, n, "Regular");
			}
			if(space.name == "Mount Gundabad"){
				nsc.RecruitUnit(2, n, "Regular");
			}
			if(space.name == "Nurn"){
				nsc.RecruitUnit(2, n, "Regular");
			}
			if(space.name == "Morannon"){
				nsc.RecruitUnit(5, n, "Regular");
				nsc.RecruitUnit(1, n, "Leader");
			}
			if(space.name == "Far Harad"){
				nsc.RecruitUnit(3, n, "Regular");
				nsc.RecruitUnit(1, n, "Elite");
			}
			if(space.name == "Near Harad"){
				nsc.RecruitUnit(3, n, "Regular");
				nsc.RecruitUnit(1, n, "Elite");
			}
			if(space.name == "North Rhun"){
				nsc.RecruitUnit(2, n, "Regular");
			}
			if(space.name == "South Rhun"){
				nsc.RecruitUnit(3, n, "Regular");
				nsc.RecruitUnit(1, n, "Elite");
			}
			if(space.name == "Umbar"){
				nsc.RecruitUnit(3, n, "Regular");
			}
		}
	}

	public void UnhighlightAllSpaces(){
		Debug.Log("Current action ended.");
		foreach (GameObject space in allSpaces) {
			//Unhighlight all of the spaces
			if(space.GetComponent<New_SpaceClass>() != null){
				space.GetComponent<New_SpaceClass>().UnhighlightSpace();
			}
			if(space.GetComponent<SettlementSpace>() != null){
				space.GetComponent<SettlementSpace>().UnhighlightSpace();
			}
		}
	}

	//    Overloaded method that allows some spaces to stay highligted
	public void UnhighlightAllSpaces(GameObject exception){
		Debug.Log("Current action ended.");
		foreach (GameObject space in allSpaces) {
			if(exception.name == space.name){ //Skip over the specified space
				continue;
			} 
			//Unhighlight all of the spaces
			if(space.GetComponent<New_SpaceClass>() != null){
				space.GetComponent<New_SpaceClass>().UnhighlightSpace();
			}
			if(space.GetComponent<SettlementSpace>() != null){
				space.GetComponent<SettlementSpace>().UnhighlightSpace();
			}
		}
	}

}
