using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	//Set as singleton
	private static UIManager instance;
	public static UIManager Instance{
		get{
			if (instance == null) {
				instance = GameObject.FindObjectOfType<UIManager>();
			} 
			return instance;
		}
	}

	public GameObject confirmAction_Panel;
	public GameObject declareFellowship_Panel;
	public GameObject changeGuide_Panel;
	public GameObject confirmHuntDice_Panel;

	private GameObject[] allSpaces;

	//For keeping track of rolling - values changed in ActionDice script
	public int diceStarted = 0;
	public int diceDone = 0;

//------------------------------------------------------------------
	void Awake(){
		instance = this;
	}
	// Use this for initialization
	void Start () {
		allSpaces = GameObject.FindGameObjectsWithTag ("Space");
		//Get needed prefabs
		confirmAction_Panel = (Resources.Load ("UI_Prefabs/ScreenSpaceUI/ConfirmActionPanel") as GameObject);
		declareFellowship_Panel = (Resources.Load ("UI_Prefabs/ScreenSpaceUI/Fphase/DeclareFellowshipPanel") as GameObject);
		changeGuide_Panel = (Resources.Load ("UI_Prefabs/ScreenSpaceUI/Fphase/ChangeGuidePanel") as GameObject);
		confirmHuntDice_Panel = (Resources.Load ("UI_Prefabs/ScreenSpaceUI/HAphase/HuntAllocationPanel") as GameObject);
	}
//-----------------------------------------------------------------------------------------------------------
//                              Prompts
//-----------------------------------------------------------------------------------------------------------
	public void Prompt_DeclareFellowship(){
		FocusToPanel ();
		GameObject dfPanel = Instantiate(declareFellowship_Panel, transform.position, transform.rotation) as GameObject;
		dfPanel.transform.SetParent (GameObject.Find ("Main UI").transform, false);
	}

	public void Prompt_ChangeGuide(){
		FocusToPanel ();
		GameObject cgPanel = Instantiate(changeGuide_Panel, transform.position, transform.rotation) as GameObject;
		cgPanel.transform.SetParent (GameObject.Find ("Main UI").transform, false);
	}

	public void Prompt_AllocateHuntDice(){
		FocusToPanel ();
		GameObject chdPanel = Instantiate(confirmHuntDice_Panel, transform.position, transform.rotation) as GameObject;
		chdPanel.transform.SetParent (GameObject.Find ("Main UI").transform, false);
	}

	public void Prompt_ConfirmAction(){ //Call this function when player has taken his action for this turn
		FocusToPanel ();
		//Bring up confirmation popup
		GameObject caPanel = Instantiate(confirmAction_Panel, transform.position, transform.rotation) as GameObject;
		caPanel.transform.SetParent (GameObject.Find ("Main UI").transform, false);
	}

	public void DestroyPrompt(GameObject sender){
		Destroy (sender);
	}

	private void FocusToPanel(){
		foreach (GameObject space in allSpaces) { //Don't allow clicking on any of the board spaces or dice
			space.GetComponent<Button>().interactable = false;
		}
		//GameObject[] allDice = GameObject.FindGameObjectsWithTag (); //Also turn off dice?
	}

	private void ReturnNormalFocus(){
		GameObject[] allSpaces = GameObject.FindGameObjectsWithTag ("Space");
		foreach (GameObject space in allSpaces) {
			space.GetComponent<Button>().interactable = true;
		}
	}


	public void CheckIfDiceDone(){ //Check for rolling finished.
		if(diceStarted > 1 && diceStarted == diceDone){ //The dice are done rolling. Advance the game phase.
			TurnManager.Instance.AdvanceGamePhase();
			diceStarted = 0; diceDone = 0;
		}
	}

	public void ConfirmHuntDice(){
		EventManager.HuntDiceChosenFunction();
	}
//-------------------------------------------------------------------------------------------------------
//                          Subscribe to Events
//-------------------------------------------------------------------------------------------------------
	void OnEnable(){
		EventManager.ActionPhase += ReturnNormalFocus; //Both players should have access to the board during Action phase
		if(GameManager.userIsFP){
			EventManager.Fellowship += Prompt_DeclareFellowship;
			EventManager.GuideSwitched += Prompt_ConfirmAction;
		}
		else{
			EventManager.HuntAllocation += Prompt_AllocateHuntDice;
		}
	}
	
	void OnDisable(){
		EventManager.ActionPhase -= ReturnNormalFocus;
		if(GameManager.userIsFP){
			EventManager.Fellowship -= Prompt_DeclareFellowship;
			EventManager.GuideSwitched -= Prompt_ConfirmAction;
		}
		else{
			EventManager.HuntAllocation -= Prompt_AllocateHuntDice;
		}
	}
}
