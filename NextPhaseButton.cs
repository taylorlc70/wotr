using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextPhaseButton : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		//Set the UpdateText function to run with every gamephase switch event
		EventManager.RecAndDraw += UpdateText;
		EventManager.Fellowship += UpdateText;
		EventManager.HuntAllocation += UpdateText;
		EventManager.RollingDice += UpdateText;
		EventManager.ActionPhase += UpdateText;
		EventManager.VictoryCheck += UpdateText;
	}

	void Start(){
		//Setup the look of the button for the players
		if (GameManager.userIsFP) {
			gameObject.GetComponent<Image>().sprite = Resources.Load("Artwork/Sprites/FP_Button_Sprite", typeof(Sprite)) as Sprite;
			gameObject.GetComponentInChildren<Text>().color = Color.white;
		} else {
			gameObject.GetComponent<Image>().sprite = Resources.Load("Artwork/Sprites/Dice/SHDiceBase", typeof(Sprite)) as Sprite;
			gameObject.GetComponentInChildren<Text>().color = Color.black;
		}
		gameObject.GetComponentInChildren<Text>().text = "Next Phase: Fellowship Phase";
	}
	void UpdateText () {
		switch (TurnManager.Current_game_phase){
			case TurnManager.GamePhase.RecoverDiceAndDrawCards:
				//gameObject.GetComponent<Button>().interactable = true;
				gameObject.GetComponentInChildren<Text>().text = "Next Phase: Fellowship Phase";
				break;
			case TurnManager.GamePhase.FellowshipPhase:
				gameObject.GetComponentInChildren<Text>().text = "Next Phase: Hunt Allocation";
				break;
			case TurnManager.GamePhase.HuntAllocation:
				gameObject.GetComponentInChildren<Text>().text = "Next Phase: Roll Action Dice";
				break;
			case TurnManager.GamePhase.RollingActionDice:
				gameObject.GetComponentInChildren<Text>().text = "Next Phase: Action Phase";
				break;
			case TurnManager.GamePhase.ACTION_PHASE:
				//gameObject.GetComponent<Button>().interactable = false;
				gameObject.GetComponentInChildren<Text>().text = "Next Phase: Victory Check";
				break;
			case TurnManager.GamePhase.VictoryCheck:
				gameObject.GetComponentInChildren<Text>().text = "Next Phase: Recover Dice and Draw Cards";
				break;
		}
	}

	void OnDisable(){
		EventManager.RecAndDraw -= UpdateText;
		EventManager.Fellowship -= UpdateText;
		EventManager.HuntAllocation -= UpdateText;
		EventManager.RollingDice -= UpdateText;
		EventManager.ActionPhase -= UpdateText;
		EventManager.VictoryCheck -= UpdateText;
	}
}
