using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpaceClass : MonoBehaviour{

	public Game thisGame;

	public string regionName;
	
	public GameManager.Nation partOf;

	public GameObject armyPrefab;

	//public bool isHighlighted = false;
	public enum HighlightState{
		None,
		Recruit,
		MoveStart,
		MoveDest
	}
	private HighlightState currentHighlight;

	public Color defaultColor;
	public Color highlightColor;

	public List<SpaceClass> adjacentSpaces;
	
	public GameObject myInfoPanel;

	//********************************************************************
	//MonoBehaviour Methods

	public virtual void Awake () {

		defaultColor = GetComponent<Renderer>().material.color;
		thisGame = GameObject.Find("Game").GetComponent<Game>();
		armyPrefab = Resources.Load("Prefabs/Army/ArmyPrefab") as GameObject;
		//Set Info Panel prefab for this space. Overridden in child classes.
		myInfoPanel = Resources.Load("UI_Prefabs/WorldSpaceUI/NormalSpaceInfo") as GameObject;
	}

	public virtual void Start(){
		//Populate list of adjacent spaces
		foreach (string x in GameManager.GetAdjacentSpaces(regionName)) {
			adjacentSpaces.Add(GameObject.Find(x).GetComponent<SpaceClass>());
		}
		//adjacentSpaces = GameManager.GetAdjacentSpaces(regionName);
	}

	//********************************************************
	//Highlight this space function
	public void HighlightSpace(HighlightState state){
		//isHighlighted = true;
		switch (state) {
			case HighlightState.Recruit:
				currentHighlight = HighlightState.Recruit;
				highlightColor = Color.green;
				break;
			case HighlightState.MoveStart:
				currentHighlight = HighlightState.MoveStart;
				highlightColor = Color.yellow;
				break;
			case HighlightState.MoveDest:
				currentHighlight = HighlightState.MoveDest;
				highlightColor = Color.magenta;
				break;
		}
		GetComponent<Renderer>().material.color = highlightColor;
	}

	//********************MOVEMENT FUNCTION *******************************************
	public void HighlightForMovement(int counter){
		if (counter == 0) {
			return;
		} else {
			counter -= 1;
			foreach(SpaceClass spc in adjacentSpaces){
				if(spc.currentHighlight != HighlightState.MoveDest){
					//If no highlight function exists
					//spc.currentHighlight = HighlightState.MoveDest;
					//If highlight function exists
					spc.HighlightSpace(HighlightState.MoveDest);
				}
				spc.HighlightForMovement(counter);
			}
		}
	}

	public void UnhighlightSpace(){
		//isHighlighted = false;
		currentHighlight = HighlightState.None;
		GetComponent<Renderer>().material.color = defaultColor;
	}

	public void RecruitUnit(uint amt, string type){
		Army army = null;
		if (GetComponentInChildren<Army>() != null) {
			army = GetComponentInChildren<Army>();
		}
		if (army != null) {
			GetComponentInChildren<Army>().AddUnits(amt, partOf, type);
		} else {
			GameObject newArmy = Instantiate(armyPrefab) as GameObject;
			newArmy.transform.parent = transform;
			//newArmy.GetComponent<Army>().InitArmy();
			newArmy.GetComponent<Army>().AddUnits(amt, partOf, type);
		}
	}

	//************************************************************
	// Region name popup
	// The popup that shows the name of the region is called RegionName
	void OnMouseOver(){
		if (transform.Find("RegionName(Clone)") == null) {
			GameObject namePopup = Instantiate(Resources.Load("UI_Prefabs/WorldspaceUI/RegionName")) as GameObject;
			namePopup.transform.position = gameObject.transform.position;
			namePopup.transform.SetParent(gameObject.transform);
			namePopup.GetComponentInChildren<Text>().text = regionName;
		}
	}

	void OnMouseExit(){
		//Destroy name popup
		try{
			GameObject namePopup = transform.Find("RegionName(Clone)").gameObject;
			Destroy(namePopup);
		}
		catch{
			Debug.Log("CATCH: did not find region name popup");
		}
	}
	//**********************************************************
	// OnMouseDown function calls what is needed when space is clicked on
	void OnMouseDown(){
		//For debugging
		foreach(SpaceClass space in adjacentSpaces){
			Debug.DrawRay(gameObject.transform.position, GameObject.Find(space.regionName).transform.position - gameObject.transform.position, Color.red, 2.0f);
		}

		//First action on click:
		//            Destory all other popup menus and info windows
		if(GameObject.FindGameObjectWithTag("PopupMenu") != null){
			Destroy(GameObject.FindGameObjectWithTag("PopupMenu"));
		}
		//            Also destroy all other infowindows
		if(GameObject.FindGameObjectsWithTag("InfoWindow") != null){
			foreach(GameObject infoWindow in GameObject.FindGameObjectsWithTag("InfoWindow")){
				Destroy(infoWindow);
			}
		}

		switch (currentHighlight) {
			case HighlightState.Recruit:
				thisGame.UnhighlightAllSpaces(gameObject);
				GameObject recruitMenu;
				//Choose the right recruiting popup menu based on whose turn it is
				if(TurnManager.CurrentAction == TurnManager.TurnAction.FreePlayer){
					recruitMenu = Instantiate(Resources.Load("UI_Prefabs/WorldSpaceUI/RecruitingMenu_FP")) as GameObject;
				}else{
					recruitMenu = Instantiate(Resources.Load("UI_Prefabs/WorldSpaceUI/RecruitingMenu_SH")) as GameObject;
				}

				recruitMenu.transform.position = gameObject.transform.position;
				recruitMenu.transform.SetParent(gameObject.transform);
				break;

			case HighlightState.MoveStart:
				//Only this space will be highlighted as start movement when clicked
				thisGame.UnhighlightAllSpaces(gameObject);
				foreach(SpaceClass spc in adjacentSpaces){
					spc.HighlightSpace(HighlightState.MoveDest);
				}
				break;

			case HighlightState.MoveDest:
				foreach(SpaceClass spc in adjacentSpaces){
					//find the adjacent space highlighted to start movement
					if(spc.currentHighlight == HighlightState.MoveStart){
						//make sure there is an army on that space
						if(spc.gameObject.GetComponentInChildren<Army>() != null){
							GameObject movePanel = Instantiate(Resources.Load("UI_Prefabs/WorldSpaceUI/ArmyMovePopup")) as GameObject;
							//movePanel.GetComponent<ArmyMovePopup>().Init_ArmyMovePopup(moveStartSpace, gameObject);
							//movePanel.transform.position = Vector3.Lerp(moveStartSpace.transform.position, gameObject.transform.position, 0.5f);
						}
						else{
							Debug.Log("Error! There is no army in the first space!");
						}
					}
				}
				thisGame.UnhighlightAllSpaces();
				break;

			case HighlightState.None:
				//Bring up info window about this space. **CHECK TO MAKE SURE this works correctly for other types of spaces
				BringUpInfoPanel();

				thisGame.UnhighlightAllSpaces();
				break;
		}
	}

	public virtual void BringUpInfoPanel(){
		//This function is called as is and has stuff added to it in the override child class functions
		GameObject infoPanel;
		if (GetComponentInChildren<Transform>().tag != "InfoWindow") {
			infoPanel = Instantiate(myInfoPanel) as GameObject;
			infoPanel.transform.SetParent(gameObject.transform);
			infoPanel.transform.position = gameObject.transform.position;
			infoPanel.transform.Find("RegionName").GetComponent<Text>().text = regionName;
			infoPanel.transform.Find("Nation").GetComponent<Text>().text = partOf.ToString();
			//Also set custom image and text here
		}
	}
	
}
