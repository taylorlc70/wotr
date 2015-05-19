using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour {
	//
	//Instance for the singleton
	private static TurnManager instance;
	public static TurnManager Instance{
		get{
			if (instance == null) {
				instance = GameObject.FindObjectOfType<TurnManager>();
			} 
			return instance;
		}
	}

	//GamePhase definition
	public enum GamePhase{
		RecoverDiceAndDrawCards,
		FellowshipPhase,
		HuntAllocation,
		RollingActionDice,
		ACTION_PHASE,
		VictoryCheck
	}
	private static GamePhase current_game_phase;
	public static GamePhase Current_game_phase{
		get{
			return current_game_phase;
		}
	}

	public enum TurnAction{
		FreePlayer,
		ShadowPlayer
	}
	private static TurnAction currentAction;
	public static TurnAction CurrentAction{
		get{
			return CurrentAction;
		}
	}

	private static int gameTurn = 0;

	//**************************************************************************************
	//                  Functions

	void Awake(){
		//Create Singleton
		instance = this;
	}

	//*************************************************************
	//Custom functions
	public void NextAction(){
		if (currentAction == TurnAction.FreePlayer) {
			currentAction = TurnAction.ShadowPlayer;
			Debug.Log("Awaiting action from " + currentAction.ToString());
		}
		else{
			currentAction = TurnAction.FreePlayer;
			Debug.Log("Awaiting action from  " + currentAction.ToString());
		}
	}
	
	public void StartTurn(){
		gameTurn ++;
		Debug.Log (string.Format("Game turn #" + "{0}" +" has started.", gameTurn));
		current_game_phase = GamePhase.RecoverDiceAndDrawCards;

		EventManager.PhaseEventTrigger (current_game_phase); // Call the PhaseEventTrigger event
	}

	public void AdvanceGamePhase(){
		//This changes phases AND sets off the PhaseWasSwitched event
		if (current_game_phase != GamePhase.VictoryCheck) {
			current_game_phase += 1;
		} else {
			current_game_phase = GamePhase.RecoverDiceAndDrawCards;
		}

		Debug.Log("Game phase is now " + current_game_phase.ToString());

		EventManager.PhaseEventTrigger(current_game_phase); // Call the PhaseEventTrigger event

	}

	public void RevertGamePhase(){

		Debug.Log("Game phase is now " + current_game_phase.ToString());
		
		EventManager.PhaseEventTrigger(current_game_phase); // Call the PhaseEventTrigger event
		
	}
}
